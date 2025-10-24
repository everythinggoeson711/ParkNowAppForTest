using System;

namespace Parking.FindingSlotManagement.Application.Models.SepayWebhook
{
    /// <summary>
    /// Response trả về cho SePay sau khi nhận webhook
    /// </summary>
    public class SepayWebhookResponse
    {
        /// <summary>
        /// true nếu xử lý thành công, false nếu thất bại
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Thông báo cho SePay biết kết quả
        /// </summary>
        public string? Message { get; set; }

        /// <summary>
        /// ID giao dịch đã lưu (nếu có)
        /// </summary>
        public int? TransactionId { get; set; }

        /// <summary>
        /// BookingId đã được cập nhật (nếu có)
        /// </summary>
        public int? BookingId { get; set; }
    }
}
