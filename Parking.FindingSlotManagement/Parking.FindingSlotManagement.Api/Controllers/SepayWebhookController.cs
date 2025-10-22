using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking.FindingSlotManagement.Application.Models.SepayWebhook;
using Parking.FindingSlotManagement.Domain.Entities;
using Parking.FindingSlotManagement.Infrastructure.Persistences;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Api.Controllers
{
    /// <summary>
    /// Controller nhận webhook từ SePay khi có giao dịch ngân hàng
    /// </summary>
    [Route("api/sepay-webhook")]
    [ApiController]
    public class SepayWebhookController : ControllerBase
    {
        private readonly ParkZDbContext _context;
        private readonly ILogger<SepayWebhookController> _logger;

        public SepayWebhookController(ParkZDbContext context, ILogger<SepayWebhookController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Endpoint nhận webhook từ SePay
        /// URL công khai: https://your-domain.com/api/sepay-webhook/receive
        /// </summary>
        /// <param name="webhookData">Dữ liệu từ SePay</param>
        /// <returns>Response theo format SePay yêu cầu</returns>
        [HttpPost("receive")]
        [ProducesResponseType(typeof(SepayWebhookResponse), 200)]
        [ProducesResponseType(typeof(SepayWebhookResponse), 201)]
        [ProducesResponseType(typeof(SepayWebhookResponse), 400)]
        public async Task<IActionResult> ReceiveWebhook([FromBody] SepayWebhookDto webhookData)
        {
            try
            {
                _logger.LogInformation($"[SePay Webhook] Received webhook ID: {webhookData.Id}, Amount: {webhookData.TransferAmount}, Code: {webhookData.Code}");

                // Validate dữ liệu cơ bản
                if (webhookData == null || webhookData.Id <= 0)
                {
                    return BadRequest(new SepayWebhookResponse
                    {
                        Success = false,
                        Message = "Invalid webhook data"
                    });
                }

                // Kiểm tra trùng lặp giao dịch dựa vào SepayTransactionId
                var existingTransaction = await _context.SepayWebhookTransactions
                    .FirstOrDefaultAsync(x => x.SepayTransactionId == webhookData.Id);

                if (existingTransaction != null)
                {
                    _logger.LogWarning($"[SePay Webhook] Duplicate transaction ID: {webhookData.Id}");
                    return Ok(new SepayWebhookResponse
                    {
                        Success = true,
                        Message = "Transaction already processed",
                        TransactionId = existingTransaction.TransactionId,
                        BookingId = existingTransaction.BookingId
                    });
                }

                // Parse transaction date
                DateTime transactionDate;
                if (!DateTime.TryParse(webhookData.TransactionDate, out transactionDate))
                {
                    transactionDate = DateTime.Now;
                }

                // Tạo entity mới
                var transaction = new SepayWebhookTransaction
                {
                    SepayTransactionId = webhookData.Id,
                    Gateway = webhookData.Gateway,
                    TransactionDate = transactionDate,
                    AccountNumber = webhookData.AccountNumber,
                    SubAccount = webhookData.SubAccount,
                    TransferType = webhookData.TransferType ?? "in",
                    TransferAmount = webhookData.TransferAmount,
                    AmountIn = webhookData.TransferType == "in" ? webhookData.TransferAmount : 0,
                    AmountOut = webhookData.TransferType == "out" ? webhookData.TransferAmount : 0,
                    Accumulated = webhookData.Accumulated,
                    Code = webhookData.Code,
                    TransactionContent = webhookData.Content,
                    ReferenceCode = webhookData.ReferenceCode,
                    Description = webhookData.Description,
                    RawJsonData = JsonSerializer.Serialize(webhookData),
                    ProcessStatus = "Pending",
                    CreatedAt = DateTime.Now
                };

                // Lưu vào database
                _context.SepayWebhookTransactions.Add(transaction);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"[SePay Webhook] Saved transaction ID: {transaction.TransactionId}");

                // Xử lý logic nghiệp vụ (cập nhật booking status nếu có code)
                bool bookingUpdated = false;
                int? bookingId = null;

                if (!string.IsNullOrEmpty(webhookData.Code) && webhookData.TransferType == "in")
                {
                    bookingUpdated = await ProcessPaymentForBooking(transaction, webhookData.Code);
                    bookingId = transaction.BookingId;
                }

                // Cập nhật trạng thái xử lý
                transaction.ProcessStatus = bookingUpdated ? "Processed" : "Pending";
                transaction.ProcessedAt = bookingUpdated ? DateTime.Now : null;
                transaction.ProcessNote = bookingUpdated 
                    ? $"Booking {bookingId} paid successfully" 
                    : "No booking found or payment not applicable";
                
                await _context.SaveChangesAsync();

                // Trả về response theo format SePay yêu cầu
                var response = new SepayWebhookResponse
                {
                    Success = true,
                    Message = bookingUpdated 
                        ? $"Payment processed for booking {bookingId}" 
                        : "Webhook received and saved",
                    TransactionId = transaction.TransactionId,
                    BookingId = bookingId
                };

                // SePay yêu cầu status code 200 hoặc 201 với success: true
                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[SePay Webhook] Error processing webhook");
                
                return StatusCode(500, new SepayWebhookResponse
                {
                    Success = false,
                    Message = $"Internal server error: {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Xử lý thanh toán cho booking khi nhận được webhook
        /// </summary>
        /// <param name="transaction">Transaction entity</param>
        /// <param name="paymentCode">Mã thanh toán (có thể là BookingId)</param>
        /// <returns>True nếu cập nhật thành công</returns>
        private async Task<bool> ProcessPaymentForBooking(SepayWebhookTransaction transaction, string paymentCode)
        {
            try
            {
                // Thử parse code thành BookingId
                if (!int.TryParse(paymentCode, out int bookingId))
                {
                    // Nếu code không phải số, thử tìm theo mã khác (vd: mã đơn hàng tùy chỉnh)
                    _logger.LogWarning($"[SePay Webhook] Cannot parse payment code to BookingId: {paymentCode}");
                    return false;
                }

                // Tìm booking
                var booking = await _context.Bookings
                    .Include(b => b.User)
                    .FirstOrDefaultAsync(b => b.BookingId == bookingId);

                if (booking == null)
                {
                    _logger.LogWarning($"[SePay Webhook] Booking not found: {bookingId}");
                    return false;
                }

                // Kiểm tra số tiền thanh toán có khớp không
                var expectedAmount = booking.TotalPrice ?? 0;
                if (Math.Abs(transaction.TransferAmount - expectedAmount) > 1000) // Cho phép sai lệch 1000 VND
                {
                    _logger.LogWarning($"[SePay Webhook] Amount mismatch. Expected: {expectedAmount}, Received: {transaction.TransferAmount}");
                    transaction.ProcessNote = $"Amount mismatch. Expected: {expectedAmount}, Received: {transaction.TransferAmount}";
                    return false;
                }

                // Cập nhật trạng thái booking
                booking.Status = "Success"; // Hoặc "Paid" tùy theo enum của bạn
                
                // Cập nhật BookingId vào transaction
                transaction.BookingId = bookingId;

                // Có thể tạo thêm Transaction record cho hệ thống nội bộ
                var internalTransaction = new Transaction
                {
                    Price = transaction.TransferAmount,
                    Status = "Success",
                    PaymentMethod = $"SePay - {transaction.Gateway}",
                    Description = $"Payment via SePay. Ref: {transaction.ReferenceCode}",
                    CreatedDate = transaction.TransactionDate,
                    BookingId = bookingId,
                    WalletId = booking.User?.Wallet?.WalletId
                };

                _context.Transactions.Add(internalTransaction);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"[SePay Webhook] Successfully updated booking {bookingId} status to Success");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[SePay Webhook] Error processing payment for code: {paymentCode}");
                return false;
            }
        }

        /// <summary>
        /// API test để kiểm tra webhook có hoạt động không
        /// </summary>
        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "Healthy",
                message = "SePay Webhook endpoint is ready to receive webhooks",
                timestamp = DateTime.Now
            });
        }

        /// <summary>
        /// API xem lịch sử webhook đã nhận
        /// </summary>
        [HttpGet("history")]
        public async Task<IActionResult> GetWebhookHistory(
            [FromQuery] int pageNo = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? status = null)
        {
            var query = _context.SepayWebhookTransactions
                .OrderByDescending(x => x.CreatedAt)
                .AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.ProcessStatus == status);
            }

            var total = await query.CountAsync();
            var transactions = await query
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new
                {
                    x.TransactionId,
                    x.SepayTransactionId,
                    x.Gateway,
                    x.TransactionDate,
                    x.TransferType,
                    x.TransferAmount,
                    x.Code,
                    x.TransactionContent,
                    x.ProcessStatus,
                    x.ProcessNote,
                    x.BookingId,
                    x.CreatedAt,
                    x.ProcessedAt
                })
                .ToListAsync();

            return Ok(new
            {
                success = true,
                total = total,
                pageNo = pageNo,
                pageSize = pageSize,
                data = transactions
            });
        }
    }
}
