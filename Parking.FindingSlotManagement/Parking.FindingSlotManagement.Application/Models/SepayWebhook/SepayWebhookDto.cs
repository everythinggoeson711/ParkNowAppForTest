using System;

namespace Parking.FindingSlotManagement.Application.Models.SepayWebhook
{
    /// <summary>
    /// DTO nhận webhook từ SePay
    /// </summary>
    public class SepayWebhookDto
    {
        /// <summary>
        /// ID giao dịch trên SePay
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Brand name của ngân hàng (vd: Vietcombank, VPBank, etc.)
        /// </summary>
        public string Gateway { get; set; } = string.Empty;

        /// <summary>
        /// Thời gian xảy ra giao dịch phía ngân hàng
        /// Format: "2023-03-25 14:02:37"
        /// </summary>
        public string TransactionDate { get; set; } = string.Empty;

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? AccountNumber { get; set; }

        /// <summary>
        /// Mã code thanh toán (SePay tự nhận diện dựa vào cấu hình)
        /// VD: "BOOK123", "ORDER456"
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Nội dung chuyển khoản
        /// VD: "chuyen tien mua iphone"
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// Loại giao dịch. "in" là tiền vào, "out" là tiền ra
        /// </summary>
        public string TransferType { get; set; } = "in";

        /// <summary>
        /// Số tiền giao dịch
        /// </summary>
        public decimal TransferAmount { get; set; }

        /// <summary>
        /// Số dư tài khoản (lũy kế)
        /// </summary>
        public decimal Accumulated { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng phụ (tài khoản định danh)
        /// </summary>
        public string? SubAccount { get; set; }

        /// <summary>
        /// Mã tham chiếu của tin nhắn SMS
        /// VD: "MBVCB.3278907687"
        /// </summary>
        public string? ReferenceCode { get; set; }

        /// <summary>
        /// Toàn bộ nội dung tin nhắn SMS
        /// </summary>
        public string? Description { get; set; }
    }
}
