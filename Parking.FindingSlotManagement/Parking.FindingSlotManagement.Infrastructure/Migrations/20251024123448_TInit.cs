using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.FindingSlotManagement.Infrastructure.Migrations
{
    public partial class TInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SepayWebhookTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SepayTransactionId = table.Column<int>(type: "int", nullable: false),
                    Gateway = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubAccount = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TransferType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AmountIn = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    AmountOut = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    TransferAmount = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    Accumulated = table.Column<decimal>(type: "decimal(20,2)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TransactionContent = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ReferenceCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RawJsonData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProcessNote = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SepayWebhookTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_SepayWebhookTransaction_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                });

            migrationBuilder.UpdateData(
                table: "ApproveParkings",
                keyColumn: "ApproveParkingId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 20, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Bills",
                keyColumn: "BillId",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2025, 10, 25, 10, 5, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 1,
                columns: new[] { "CheckinTime", "CheckoutTime", "DateBook", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2025, 10, 25, 8, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 25, 10, 2, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 24, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 25, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 25, 8, 0, 0, 0, DateTimeKind.Utc), "Done" });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 2,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 29, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 30, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 30, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 3,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 30, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 31, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 31, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 4,
                columns: new[] { "CheckinTime", "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 28, 10, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 27, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 28, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 28, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 5,
                columns: new[] { "CheckinTime", "CheckoutTime", "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 20, 8, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 20, 10, 2, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 19, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 20, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 20, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 6,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 17, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 18, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 18, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "TimeSlot",
                keyColumn: "TimeSlotId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 25, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 25, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeSlot",
                keyColumn: "TimeSlotId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 26, 6, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 25, 18, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 10, 25, 10, 2, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 216, 119, 149, 251, 157, 115, 62, 6, 11, 76, 178, 159, 62, 23, 154, 195, 90, 230, 138, 75, 108, 198, 192, 99, 23, 40, 216, 74, 249, 230, 190, 203, 122, 96, 134, 217, 70, 52, 192, 180, 239, 161, 126, 175, 205, 56, 115, 130, 64, 249, 93, 120, 132, 177, 116, 124, 166, 8, 133, 22, 144, 21, 16, 155 }, new byte[] { 205, 25, 187, 218, 9, 51, 162, 4, 96, 133, 121, 239, 242, 165, 141, 236, 113, 8, 186, 72, 185, 65, 180, 117, 45, 229, 186, 87, 140, 241, 29, 241, 35, 72, 239, 114, 74, 147, 43, 210, 18, 176, 121, 119, 208, 1, 212, 117, 218, 111, 218, 200, 174, 122, 51, 5, 121, 55, 146, 53, 61, 201, 205, 18, 125, 90, 159, 159, 118, 221, 48, 162, 159, 206, 15, 0, 154, 181, 243, 61, 151, 197, 108, 234, 187, 218, 224, 93, 155, 12, 206, 199, 228, 206, 20, 106, 98, 248, 207, 5, 230, 189, 153, 49, 225, 71, 105, 26, 231, 210, 70, 184, 60, 169, 80, 229, 49, 124, 88, 57, 144, 230, 242, 86, 92, 237, 93, 125 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 216, 119, 149, 251, 157, 115, 62, 6, 11, 76, 178, 159, 62, 23, 154, 195, 90, 230, 138, 75, 108, 198, 192, 99, 23, 40, 216, 74, 249, 230, 190, 203, 122, 96, 134, 217, 70, 52, 192, 180, 239, 161, 126, 175, 205, 56, 115, 130, 64, 249, 93, 120, 132, 177, 116, 124, 166, 8, 133, 22, 144, 21, 16, 155 }, new byte[] { 205, 25, 187, 218, 9, 51, 162, 4, 96, 133, 121, 239, 242, 165, 141, 236, 113, 8, 186, 72, 185, 65, 180, 117, 45, 229, 186, 87, 140, 241, 29, 241, 35, 72, 239, 114, 74, 147, 43, 210, 18, 176, 121, 119, 208, 1, 212, 117, 218, 111, 218, 200, 174, 122, 51, 5, 121, 55, 146, 53, 61, 201, 205, 18, 125, 90, 159, 159, 118, 221, 48, 162, 159, 206, 15, 0, 154, 181, 243, 61, 151, 197, 108, 234, 187, 218, 224, 93, 155, 12, 206, 199, 228, 206, 20, 106, 98, 248, 207, 5, 230, 189, 153, 49, 225, 71, 105, 26, 231, 210, 70, 184, 60, 169, 80, 229, 49, 124, 88, 57, 144, 230, 242, 86, 92, 237, 93, 125 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 216, 119, 149, 251, 157, 115, 62, 6, 11, 76, 178, 159, 62, 23, 154, 195, 90, 230, 138, 75, 108, 198, 192, 99, 23, 40, 216, 74, 249, 230, 190, 203, 122, 96, 134, 217, 70, 52, 192, 180, 239, 161, 126, 175, 205, 56, 115, 130, 64, 249, 93, 120, 132, 177, 116, 124, 166, 8, 133, 22, 144, 21, 16, 155 }, new byte[] { 205, 25, 187, 218, 9, 51, 162, 4, 96, 133, 121, 239, 242, 165, 141, 236, 113, 8, 186, 72, 185, 65, 180, 117, 45, 229, 186, 87, 140, 241, 29, 241, 35, 72, 239, 114, 74, 147, 43, 210, 18, 176, 121, 119, 208, 1, 212, 117, 218, 111, 218, 200, 174, 122, 51, 5, 121, 55, 146, 53, 61, 201, 205, 18, 125, 90, 159, 159, 118, 221, 48, 162, 159, 206, 15, 0, 154, 181, 243, 61, 151, 197, 108, 234, 187, 218, 224, 93, 155, 12, 206, 199, 228, 206, 20, 106, 98, 248, 207, 5, 230, 189, 153, 49, 225, 71, 105, 26, 231, 210, 70, 184, 60, 169, 80, 229, 49, 124, 88, 57, 144, 230, 242, 86, 92, 237, 93, 125 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 216, 119, 149, 251, 157, 115, 62, 6, 11, 76, 178, 159, 62, 23, 154, 195, 90, 230, 138, 75, 108, 198, 192, 99, 23, 40, 216, 74, 249, 230, 190, 203, 122, 96, 134, 217, 70, 52, 192, 180, 239, 161, 126, 175, 205, 56, 115, 130, 64, 249, 93, 120, 132, 177, 116, 124, 166, 8, 133, 22, 144, 21, 16, 155 }, new byte[] { 205, 25, 187, 218, 9, 51, 162, 4, 96, 133, 121, 239, 242, 165, 141, 236, 113, 8, 186, 72, 185, 65, 180, 117, 45, 229, 186, 87, 140, 241, 29, 241, 35, 72, 239, 114, 74, 147, 43, 210, 18, 176, 121, 119, 208, 1, 212, 117, 218, 111, 218, 200, 174, 122, 51, 5, 121, 55, 146, 53, 61, 201, 205, 18, 125, 90, 159, 159, 118, 221, 48, 162, 159, 206, 15, 0, 154, 181, 243, 61, 151, 197, 108, 234, 187, 218, 224, 93, 155, 12, 206, 199, 228, 206, 20, 106, 98, 248, 207, 5, 230, 189, 153, 49, 225, 71, 105, 26, 231, 210, 70, 184, 60, 169, 80, 229, 49, 124, 88, 57, 144, 230, 242, 86, 92, 237, 93, 125 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 216, 119, 149, 251, 157, 115, 62, 6, 11, 76, 178, 159, 62, 23, 154, 195, 90, 230, 138, 75, 108, 198, 192, 99, 23, 40, 216, 74, 249, 230, 190, 203, 122, 96, 134, 217, 70, 52, 192, 180, 239, 161, 126, 175, 205, 56, 115, 130, 64, 249, 93, 120, 132, 177, 116, 124, 166, 8, 133, 22, 144, 21, 16, 155 }, new byte[] { 205, 25, 187, 218, 9, 51, 162, 4, 96, 133, 121, 239, 242, 165, 141, 236, 113, 8, 186, 72, 185, 65, 180, 117, 45, 229, 186, 87, 140, 241, 29, 241, 35, 72, 239, 114, 74, 147, 43, 210, 18, 176, 121, 119, 208, 1, 212, 117, 218, 111, 218, 200, 174, 122, 51, 5, 121, 55, 146, 53, 61, 201, 205, 18, 125, 90, 159, 159, 118, 221, 48, 162, 159, 206, 15, 0, 154, 181, 243, 61, 151, 197, 108, 234, 187, 218, 224, 93, 155, 12, 206, 199, 228, 206, 20, 106, 98, 248, 207, 5, 230, 189, 153, 49, 225, 71, 105, 26, 231, 210, 70, 184, 60, 169, 80, 229, 49, 124, 88, 57, 144, 230, 242, 86, 92, 237, 93, 125 } });

            migrationBuilder.CreateIndex(
                name: "IX_SepayWebhookTransaction_BookingId",
                table: "SepayWebhookTransaction",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SepayWebhookTransaction");

            migrationBuilder.UpdateData(
                table: "ApproveParkings",
                keyColumn: "ApproveParkingId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 5, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Bills",
                keyColumn: "BillId",
                keyValue: 1,
                column: "Time",
                value: new DateTime(2024, 1, 10, 10, 5, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 1,
                columns: new[] { "CheckinTime", "CheckoutTime", "DateBook", "EndTime", "StartTime", "Status" },
                values: new object[] { new DateTime(2024, 1, 10, 8, 5, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 10, 2, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 9, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "Completed" });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 2,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 11, 14, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 15, 11, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 15, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 3,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 11, 15, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 16, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 16, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 4,
                columns: new[] { "CheckinTime", "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 11, 17, 10, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 16, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 17, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 11, 17, 10, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 5,
                columns: new[] { "CheckinTime", "CheckoutTime", "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 10, 8, 5, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 10, 10, 2, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 9, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 10, 10, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 10, 8, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Booking",
                keyColumn: "BookingID",
                keyValue: 6,
                columns: new[] { "DateBook", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2025, 10, 11, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 12, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 10, 12, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "TimeSlot",
                keyColumn: "TimeSlotId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "TimeSlot",
                keyColumn: "TimeSlotId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "EndTime", "StartTime" },
                values: new object[] { new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "TransactionId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 1, 10, 10, 2, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 90, 210, 239, 23, 45, 103, 55, 96, 128, 154, 8, 88, 76, 143, 139, 228, 6, 140, 85, 222, 231, 26, 178, 221, 177, 178, 153, 153, 17, 17, 6, 8, 96, 66, 124, 14, 46, 151, 20, 46, 145, 187, 202, 62, 102, 87, 207, 66, 36, 18, 19, 47, 212, 81, 21, 94, 203, 38, 200, 29, 97, 91, 205, 246 }, new byte[] { 133, 49, 135, 4, 205, 207, 173, 89, 21, 130, 248, 153, 23, 6, 203, 142, 253, 139, 172, 160, 118, 141, 254, 28, 123, 120, 171, 248, 130, 196, 68, 200, 190, 143, 211, 90, 239, 59, 19, 232, 118, 58, 64, 93, 101, 93, 9, 183, 156, 179, 139, 219, 189, 0, 15, 150, 99, 121, 203, 248, 119, 133, 60, 243, 24, 21, 154, 33, 11, 218, 80, 22, 193, 188, 134, 111, 229, 102, 26, 11, 187, 21, 6, 178, 142, 38, 20, 110, 55, 9, 210, 136, 91, 248, 96, 193, 156, 43, 121, 94, 206, 15, 225, 185, 49, 154, 51, 94, 190, 204, 163, 74, 175, 4, 171, 139, 20, 67, 83, 109, 142, 213, 61, 223, 225, 91, 110, 181 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 90, 210, 239, 23, 45, 103, 55, 96, 128, 154, 8, 88, 76, 143, 139, 228, 6, 140, 85, 222, 231, 26, 178, 221, 177, 178, 153, 153, 17, 17, 6, 8, 96, 66, 124, 14, 46, 151, 20, 46, 145, 187, 202, 62, 102, 87, 207, 66, 36, 18, 19, 47, 212, 81, 21, 94, 203, 38, 200, 29, 97, 91, 205, 246 }, new byte[] { 133, 49, 135, 4, 205, 207, 173, 89, 21, 130, 248, 153, 23, 6, 203, 142, 253, 139, 172, 160, 118, 141, 254, 28, 123, 120, 171, 248, 130, 196, 68, 200, 190, 143, 211, 90, 239, 59, 19, 232, 118, 58, 64, 93, 101, 93, 9, 183, 156, 179, 139, 219, 189, 0, 15, 150, 99, 121, 203, 248, 119, 133, 60, 243, 24, 21, 154, 33, 11, 218, 80, 22, 193, 188, 134, 111, 229, 102, 26, 11, 187, 21, 6, 178, 142, 38, 20, 110, 55, 9, 210, 136, 91, 248, 96, 193, 156, 43, 121, 94, 206, 15, 225, 185, 49, 154, 51, 94, 190, 204, 163, 74, 175, 4, 171, 139, 20, 67, 83, 109, 142, 213, 61, 223, 225, 91, 110, 181 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 90, 210, 239, 23, 45, 103, 55, 96, 128, 154, 8, 88, 76, 143, 139, 228, 6, 140, 85, 222, 231, 26, 178, 221, 177, 178, 153, 153, 17, 17, 6, 8, 96, 66, 124, 14, 46, 151, 20, 46, 145, 187, 202, 62, 102, 87, 207, 66, 36, 18, 19, 47, 212, 81, 21, 94, 203, 38, 200, 29, 97, 91, 205, 246 }, new byte[] { 133, 49, 135, 4, 205, 207, 173, 89, 21, 130, 248, 153, 23, 6, 203, 142, 253, 139, 172, 160, 118, 141, 254, 28, 123, 120, 171, 248, 130, 196, 68, 200, 190, 143, 211, 90, 239, 59, 19, 232, 118, 58, 64, 93, 101, 93, 9, 183, 156, 179, 139, 219, 189, 0, 15, 150, 99, 121, 203, 248, 119, 133, 60, 243, 24, 21, 154, 33, 11, 218, 80, 22, 193, 188, 134, 111, 229, 102, 26, 11, 187, 21, 6, 178, 142, 38, 20, 110, 55, 9, 210, 136, 91, 248, 96, 193, 156, 43, 121, 94, 206, 15, 225, 185, 49, 154, 51, 94, 190, 204, 163, 74, 175, 4, 171, 139, 20, 67, 83, 109, 142, 213, 61, 223, 225, 91, 110, 181 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 90, 210, 239, 23, 45, 103, 55, 96, 128, 154, 8, 88, 76, 143, 139, 228, 6, 140, 85, 222, 231, 26, 178, 221, 177, 178, 153, 153, 17, 17, 6, 8, 96, 66, 124, 14, 46, 151, 20, 46, 145, 187, 202, 62, 102, 87, 207, 66, 36, 18, 19, 47, 212, 81, 21, 94, 203, 38, 200, 29, 97, 91, 205, 246 }, new byte[] { 133, 49, 135, 4, 205, 207, 173, 89, 21, 130, 248, 153, 23, 6, 203, 142, 253, 139, 172, 160, 118, 141, 254, 28, 123, 120, 171, 248, 130, 196, 68, 200, 190, 143, 211, 90, 239, 59, 19, 232, 118, 58, 64, 93, 101, 93, 9, 183, 156, 179, 139, 219, 189, 0, 15, 150, 99, 121, 203, 248, 119, 133, 60, 243, 24, 21, 154, 33, 11, 218, 80, 22, 193, 188, 134, 111, 229, 102, 26, 11, 187, 21, 6, 178, 142, 38, 20, 110, 55, 9, 210, 136, 91, 248, 96, 193, 156, 43, 121, 94, 206, 15, 225, 185, 49, 154, 51, 94, 190, 204, 163, 74, 175, 4, 171, 139, 20, 67, 83, 109, 142, 213, 61, 223, 225, 91, 110, 181 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 90, 210, 239, 23, 45, 103, 55, 96, 128, 154, 8, 88, 76, 143, 139, 228, 6, 140, 85, 222, 231, 26, 178, 221, 177, 178, 153, 153, 17, 17, 6, 8, 96, 66, 124, 14, 46, 151, 20, 46, 145, 187, 202, 62, 102, 87, 207, 66, 36, 18, 19, 47, 212, 81, 21, 94, 203, 38, 200, 29, 97, 91, 205, 246 }, new byte[] { 133, 49, 135, 4, 205, 207, 173, 89, 21, 130, 248, 153, 23, 6, 203, 142, 253, 139, 172, 160, 118, 141, 254, 28, 123, 120, 171, 248, 130, 196, 68, 200, 190, 143, 211, 90, 239, 59, 19, 232, 118, 58, 64, 93, 101, 93, 9, 183, 156, 179, 139, 219, 189, 0, 15, 150, 99, 121, 203, 248, 119, 133, 60, 243, 24, 21, 154, 33, 11, 218, 80, 22, 193, 188, 134, 111, 229, 102, 26, 11, 187, 21, 6, 178, 142, 38, 20, 110, 55, 9, 210, 136, 91, 248, 96, 193, 156, 43, 121, 94, 206, 15, 225, 185, 49, 154, 51, 94, 190, 204, 163, 74, 175, 4, 171, 139, 20, 67, 83, 109, 142, 213, 61, 223, 225, 91, 110, 181 } });
        }
    }
}
