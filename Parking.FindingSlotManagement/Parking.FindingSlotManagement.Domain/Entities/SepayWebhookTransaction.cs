using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parking.FindingSlotManagement.Domain.Entities
{
    /// <summary>
    /// Entity lưu thông tin webhook từ SePay khi có giao dịch ngân hàng
    /// </summary>
    [Table("SepayWebhookTransaction")]
    public class SepayWebhookTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }

        /// <summary>
        /// ID giao dịch từ SePay
        /// </summary>
        public int SepayTransactionId { get; set; }

        /// <summary>
        /// Brand name của ngân hàng (vd: Vietcombank, VPBank)
        /// </summary>
        [MaxLength(100)]
        public string Gateway { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian xảy ra giao dịch phía ngân hàng
        /// </summary>
        public DateTime TransactionDate { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng nhận tiền
        /// </summary>
        [MaxLength(100)]
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng phụ (tài khoản định danh)
        /// </summary>
        [MaxLength(250)]
        public string? SubAccount { get; set; }

        /// <summary>
        /// Loại giao dịch: "in" là tiền vào, "out" là tiền ra
        /// </summary>
        [MaxLength(10)]
        public string TransferType { get; set; } = "in";

        /// <summary>
        /// Số tiền giao dịch (chỉ lưu tiền vào)
        /// </summary>
        [Column(TypeName = "decimal(20,2)")]
        public decimal AmountIn { get; set; } = 0;

        /// <summary>
        /// Số tiền giao dịch (chỉ lưu tiền ra)
        /// </summary>
        [Column(TypeName = "decimal(20,2)")]
        public decimal AmountOut { get; set; } = 0;

        /// <summary>
        /// Số tiền giao dịch (gốc từ SePay)
        /// </summary>
        [Column(TypeName = "decimal(20,2)")]
        public decimal TransferAmount { get; set; }

        /// <summary>
        /// Số dư tài khoản (lũy kế)
        /// </summary>
        [Column(TypeName = "decimal(20,2)")]
        public decimal Accumulated { get; set; }

        /// <summary>
        /// Mã code thanh toán (SePay tự nhận diện từ nội dung CK)
        /// Có thể là BookingId hoặc mã đơn hàng
        /// </summary>
        [MaxLength(250)]
        public string? Code { get; set; }

        /// <summary>
        /// Nội dung chuyển khoản
        /// </summary>
        [MaxLength(1000)]
        public string? TransactionContent { get; set; }

        /// <summary>
        /// Mã tham chiếu của tin nhắn SMS (vd: MBVCB.3278907687)
        /// </summary>
        [MaxLength(255)]
        public string? ReferenceCode { get; set; }

        /// <summary>
        /// Toàn bộ nội dung tin nhắn SMS
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string? Description { get; set; }

        /// <summary>
        /// JSON gốc từ SePay (để debug hoặc tra cứu sau này)
        /// </summary>
        [Column(TypeName = "nvarchar(max)")]
        public string? RawJsonData { get; set; }

        /// <summary>
        /// Trạng thái xử lý: Pending, Processed, Failed
        /// </summary>
        [MaxLength(50)]
        public string ProcessStatus { get; set; } = "Pending";

        /// <summary>
        /// Ghi chú khi xử lý (lý do thất bại, thông tin booking đã cập nhật, etc.)
        /// </summary>
        [MaxLength(500)]
        public string? ProcessNote { get; set; }

        /// <summary>
        /// BookingId nếu code thanh toán được nhận diện
        /// </summary>
        public int? BookingId { get; set; }

        /// <summary>
        /// Thời gian nhận webhook
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Thời gian xử lý webhook
        /// </summary>
        public DateTime? ProcessedAt { get; set; }

        // Navigation properties
        [ForeignKey(nameof(BookingId))]
        public virtual Booking? Booking { get; set; }
    }
}
