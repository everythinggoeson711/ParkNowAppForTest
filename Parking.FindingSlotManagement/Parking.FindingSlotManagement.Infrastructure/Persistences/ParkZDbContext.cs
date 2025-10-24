using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Parking.FindingSlotManagement.Infrastructure.Persistences
{
    public class ParkZDbContext : DbContext
    {
        public ParkZDbContext(DbContextOptions<ParkZDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<BusinessProfile> BusinessProfiles { get; set; } = null!;
        public DbSet<FavoriteAddress> FavoriteAddresses { get; set; } = null!;
        public DbSet<Floor> Floors { get; set; } = null!;
        public DbSet<TimeLine> TimeLines { get; set; } = null!;
        public DbSet<Domain.Entities.Parking> Parkings { get; set; } = null!;
        public DbSet<ParkingHasPrice> ParkingHasPrices { get; set; } = null!;
        public DbSet<ParkingPrice> ParkingPrices { get; set; } = null!;
        public DbSet<ParkingSlot> ParkingSlots { get; set; } = null!;
        public DbSet<ParkingSpotImage> ParkingSpotImages { get; set; } = null!;
        public DbSet<PayPal> PayPals { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Traffic> Traffics { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<VehicleInfor> VehicleInfors { get; set; } = null!;
        public DbSet<Fee> Fees { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<FieldWorkParkingImg> FieldWorkParkingImgs { get; set; } = null!;
        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<ApproveParking> ApproveParkings { get; set; } = null!;
        public DbSet<Domain.Entities.VnPay> VnPays { get; set; } = null!;
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<ConflictRequest> ConflictRequests { get; set; }
        public DbSet<OTP> OTPs { get; set; } = null!;
        public DbSet<SepayWebhookTransaction> SepayWebhookTransactions { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data sẽ được thêm tự động khi migration chạy
            SeedData(modelBuilder);
            
            //OnModelCreatingPartial(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Traffic (Moto=1, Car=2, Bike=3)
            modelBuilder.Entity<Traffic>().HasData(
                new Traffic { TrafficId = 1, Name = "Xe máy", IsActive = true },
                new Traffic { TrafficId = 2, Name = "Ô tô", IsActive = true },
                new Traffic { TrafficId = 3, Name = "Xe đạp", IsActive = true }
            );

            // Seed Fees đầy đủ
            modelBuilder.Entity<Fee>().HasData(
                new Fee { FeeId = 1, BusinessType = "Cá nhân", Price = 50000, Name = "Phí đăng ký cá nhân", NumberOfParking = "1-5" },
                new Fee { FeeId = 2, BusinessType = "Doanh nghiệp nhỏ", Price = 100000, Name = "Phí đăng ký doanh nghiệp nhỏ", NumberOfParking = "6-20" },
                new Fee { FeeId = 3, BusinessType = "Doanh nghiệp lớn", Price = 200000, Name = "Phí đăng ký doanh nghiệp lớn", NumberOfParking = "21-50" },
                new Fee { FeeId = 4, BusinessType = "Tập đoàn", Price = 500000, Name = "Phí đăng ký tập đoàn", NumberOfParking = "50+" }
            );

            // Tạo password hash cho "123456"
            var passwordHash = CreatePasswordHash("123456");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Admin",
                    Email = "admin@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456789",
                    RoleId = 1,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Nam",
                    Address = "Hà Nội, Việt Nam"
                },
                new User
                {
                    UserId = 2,
                    Name = "Nguyễn Văn A",
                    Email = "manager1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456788",
                    RoleId = 1,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = "Nam",
                    Address = "TP. Hồ Chí Minh, Việt Nam"
                },
                new User
                {
                    UserId = 3,
                    Name = "Trần Thị B",
                    Email = "staff1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456787",
                    RoleId = 4,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Gender = "Nữ",
                    Address = "Đà Nẵng, Việt Nam",
                    ManagerId = 2
                },
                new User
                {
                    UserId = 4,
                    Name = "Lê Văn C",
                    Email = "keeper1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456786",
                    RoleId = 2,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1988, 3, 10),
                    Gender = "Nam",
                    Address = "Hải Phòng, Việt Nam",
                    ManagerId = 2
                },
                new User
                {
                    UserId = 5,
                    Name = "Phạm Thị D",
                    Email = "customer1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456785",
                    RoleId = 3,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1995, 12, 5),
                    Gender = "Nữ",
                    Address = "Cần Thơ, Việt Nam"
                }
            );

            // Seed BusinessProfiles
            modelBuilder.Entity<BusinessProfile>().HasData(
                new BusinessProfile
                {
                    BusinessProfileId = 1,
                    Name = "Công ty TNHH Bãi đỗ xe ABC",
                    Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                    FrontIdentification = "https://via.placeholder.com/300x200",
                    BackIdentification = "https://via.placeholder.com/300x200",
                    BusinessLicense = "https://via.placeholder.com/300x200",
                    UserId = 2,
                    Type = "Doanh nghiệp",
                    FeeId = 2
                },
                new BusinessProfile
                {
                    BusinessProfileId = 2,
                    Name = "Bãi đỗ xe XYZ",
                    Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                    FrontIdentification = "https://via.placeholder.com/300x200",
                    BackIdentification = "https://via.placeholder.com/300x200",
                    BusinessLicense = "https://via.placeholder.com/300x200",
                    UserId = 1,
                    Type = "Cá nhân",
                    FeeId = 1
                }
            );

            // Seed Parkings
            modelBuilder.Entity<Domain.Entities.Parking>().HasData(
                new Domain.Entities.Parking
                {
                    ParkingId = 1,
                    Code = "PARK001",
                    Name = "Bãi đỗ xe ABC - Tầng hầm",
                    Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                    Latitude = 10.7769m,
                    Longitude = 106.7009m,
                    Description = "Bãi đỗ xe hiện đại với hệ thống camera giám sát",
                    IsActive = true,
                    IsAvailable = true,
                    MotoSpot = 50,
                    CarSpot = 30,
                    IsFull = false,
                    IsPrepayment = true,
                    IsOvernight = true,
                    Stars = 4.5f,
                    TotalStars = 45f,
                    StarsCount = 10,
                    BusinessId = 1
                },
                new Domain.Entities.Parking
                {
                    ParkingId = 2,
                    Code = "PARK002",
                    Name = "Bãi đỗ xe XYZ - Tầng trệt",
                    Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                    Latitude = 10.7879m,
                    Longitude = 106.7119m,
                    Description = "Bãi đỗ xe ngoài trời với giá cả hợp lý",
                    IsActive = true,
                    IsAvailable = true,
                    MotoSpot = 30,
                    CarSpot = 20,
                    IsFull = false,
                    IsPrepayment = false,
                    IsOvernight = false,
                    Stars = 4.0f,
                    TotalStars = 32f,
                    StarsCount = 8,
                    BusinessId = 2
                }
            );

            // Seed Floors
            modelBuilder.Entity<Floor>().HasData(
                new Floor { FloorId = 1, FloorName = "Tầng B1", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 2, FloorName = "Tầng B2", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 3, FloorName = "Tầng B3", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 4, FloorName = "Khu A", IsActive = true, ParkingId = 2 },
                new Floor { FloorId = 5, FloorName = "Khu B", IsActive = true, ParkingId = 2 }
            );

            // Seed Wallets
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet { WalletId = 1, Balance = 1000000, Debt = 0, UserId = 1 },
                new Wallet { WalletId = 2, Balance = 500000, Debt = 0, UserId = 2 },
                new Wallet { WalletId = 3, Balance = 200000, Debt = 0, UserId = 3 },
                new Wallet { WalletId = 4, Balance = 150000, Debt = 0, UserId = 4 },
                new Wallet { WalletId = 5, Balance = 300000, Debt = 0, UserId = 5 }
            );

            // Seed ParkingSlots
            var parkingSlots = new List<ParkingSlot>();
            
            // Generate parking slots for Floor 1 (B1) - 20 car slots
            for (int i = 1; i <= 20; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"A{i:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 1) / 5 + 1,
                    ColumnIndex = (i - 1) % 5 + 1,
                    TrafficId = 2, // Car
                    FloorId = 1,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 2 (B2) - 20 car slots
            for (int i = 21; i <= 40; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"B{i - 20:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 21) / 5 + 1,
                    ColumnIndex = (i - 21) % 5 + 1,
                    TrafficId = 2, // Car
                    FloorId = 2,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 3 (B3) - 30 moto slots
            for (int i = 41; i <= 70; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"M{i - 40:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 41) / 10 + 1,
                    ColumnIndex = (i - 41) % 10 + 1,
                    TrafficId = 1, // Moto
                    FloorId = 3,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 4 (Khu A) - 15 car slots
            for (int i = 71; i <= 85; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"C{i - 70:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 71) / 5 + 1,
                    ColumnIndex = (i - 71) % 5 + 1,
                    TrafficId = 2, // Car
                    FloorId = 4,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 5 (Khu B) - 15 moto slots
            for (int i = 86; i <= 100; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"D{i - 85:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 86) / 5 + 1,
                    ColumnIndex = (i - 86) % 5 + 1,
                    TrafficId = 1, // Moto
                    FloorId = 5,
                    IsBackup = false
                });
            }

            modelBuilder.Entity<ParkingSlot>().HasData(parkingSlots.ToArray());

            // Seed ParkingPrices
            modelBuilder.Entity<ParkingPrice>().HasData(
                // Car prices for Business 1 (TrafficId = 2)
                new ParkingPrice
                {
                    ParkingPriceId = 1,
                    ParkingPriceName = "Giá xe ô tô - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 5000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 1,
                    TrafficId = 2
                },
                new ParkingPrice
                {
                    ParkingPriceId = 2,
                    ParkingPriceName = "Giá xe ô tô - Ngày",
                    IsActive = true,
                    IsWholeDay = true,
                    StartingTime = 0,
                    HasPenaltyPrice = false,
                    PenaltyPrice = null,
                    PenaltyPriceStepTime = null,
                    IsExtrafee = false,
                    ExtraTimeStep = null,
                    BusinessId = 1,
                    TrafficId = 2
                },
                // Moto prices for Business 1 (TrafficId = 1)
                new ParkingPrice
                {
                    ParkingPriceId = 3,
                    ParkingPriceName = "Giá xe máy - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 2000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 1,
                    TrafficId = 1
                },
                new ParkingPrice
                {
                    ParkingPriceId = 4,
                    ParkingPriceName = "Giá xe máy - Ngày",
                    IsActive = true,
                    IsWholeDay = true,
                    StartingTime = 0,
                    HasPenaltyPrice = false,
                    PenaltyPrice = null,
                    PenaltyPriceStepTime = null,
                    IsExtrafee = false,
                    ExtraTimeStep = null,
                    BusinessId = 1,
                    TrafficId = 1
                },
                // Car prices for Business 2 (TrafficId = 2)
                new ParkingPrice
                {
                    ParkingPriceId = 5,
                    ParkingPriceName = "Giá xe ô tô - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 3000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 2,
                    TrafficId = 2
                },
                // Moto prices for Business 2 (TrafficId = 1)
                new ParkingPrice
                {
                    ParkingPriceId = 6,
                    ParkingPriceName = "Giá xe máy - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 1000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 2,
                    TrafficId = 1
                }
            );

            // Seed ParkingHasPrices
            modelBuilder.Entity<ParkingHasPrice>().HasData(
                // Parking 1 has all prices
                new ParkingHasPrice { ParkingHasPriceId = 1, ParkingId = 1, ParkingPriceId = 1 },
                new ParkingHasPrice { ParkingHasPriceId = 2, ParkingId = 1, ParkingPriceId = 2 },
                new ParkingHasPrice { ParkingHasPriceId = 3, ParkingId = 1, ParkingPriceId = 3 },
                new ParkingHasPrice { ParkingHasPriceId = 4, ParkingId = 1, ParkingPriceId = 4 },
                
                // Parking 2 has hourly prices only
                new ParkingHasPrice { ParkingHasPriceId = 5, ParkingId = 2, ParkingPriceId = 5 },
                new ParkingHasPrice { ParkingHasPriceId = 6, ParkingId = 2, ParkingPriceId = 6 }
            );

            // Seed TimeLines (sample tiers for hourly prices)
            modelBuilder.Entity<TimeLine>().HasData(
                new TimeLine
                {
                    TimeLineId = 1,
                    Name = "Ô tô - Giờ đầu",
                    Price = 15000m,
                    Description = "Giá giờ đầu cho ô tô",
                    IsActive = true,
                    StartTime = new TimeSpan(0, 0, 0),
                    EndTime = new TimeSpan(1, 0, 0),
                    ExtraFee = 0m,
                    ParkingPriceId = 1
                },
                new TimeLine
                {
                    TimeLineId = 2,
                    Name = "Ô tô - Mỗi 30 phút tiếp theo",
                    Price = 5000m,
                    Description = "Phụ phí mỗi 30 phút sau giờ đầu",
                    IsActive = true,
                    StartTime = new TimeSpan(1, 0, 0),
                    EndTime = new TimeSpan(23, 59, 59),
                    ExtraFee = 0m,
                    ParkingPriceId = 1
                },
                new TimeLine
                {
                    TimeLineId = 3,
                    Name = "Xe máy - Giờ đầu",
                    Price = 5000m,
                    Description = "Giá giờ đầu cho xe máy",
                    IsActive = true,
                    StartTime = new TimeSpan(0, 0, 0),
                    EndTime = new TimeSpan(1, 0, 0),
                    ExtraFee = 0m,
                    ParkingPriceId = 3
                },
                new TimeLine
                {
                    TimeLineId = 4,
                    Name = "Xe máy - Mỗi 30 phút tiếp theo",
                    Price = 2000m,
                    Description = "Phụ phí mỗi 30 phút sau giờ đầu",
                    IsActive = true,
                    StartTime = new TimeSpan(1, 0, 0),
                    EndTime = new TimeSpan(23, 59, 59),
                    ExtraFee = 0m,
                    ParkingPriceId = 3
                }
            );

            // Seed VehicleInfors
            modelBuilder.Entity<VehicleInfor>().HasData(
                new VehicleInfor
                {
                    VehicleInforId = 1,
                    LicensePlate = "29A1-12345",
                    VehicleName = "Honda Wave RSX",
                    Color = "Đỏ",
                    UserId = 5,
                    TrafficId = 1
                },
                new VehicleInfor
                {
                    VehicleInforId = 2,
                    LicensePlate = "30A1-67890",
                    VehicleName = "Toyota Vios",
                    Color = "Trắng",
                    UserId = 5,
                    TrafficId = 2
                }
            );

            // Seed TimeSlots
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot
                {
                    TimeSlotId = 1,
                    StartTime = new DateTime(2024, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2024, 1, 1, 18, 0, 0),
                    Status = "Active",
                    CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ParkingSlotId = 1
                },
                new TimeSlot
                {
                    TimeSlotId = 2,
                    StartTime = new DateTime(2024, 1, 1, 18, 0, 0),
                    EndTime = new DateTime(2024, 1, 2, 6, 0, 0),
                    Status = "Active",
                    CreatedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    ParkingSlotId = 1
                }
            );

            // Seed Booking (sample)
            modelBuilder.Entity<Booking>().HasData(
                new Booking
                {
                    BookingId = 1,
                    StartTime = new DateTime(2024, 1, 10, 8, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2024, 1, 10, 10, 0, 0, DateTimeKind.Utc),
                    CheckinTime = new DateTime(2024, 1, 10, 8, 5, 0, DateTimeKind.Utc),
                    CheckoutTime = new DateTime(2024, 1, 10, 10, 2, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2024, 1, 9, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Completed",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 30000m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = true,
                    UserId = 5,
                    VehicleInforId = 1
                },
                // Seed data for testing GET /api/customer-booking/upcomming
                new Booking
                {
                    BookingId = 2,
                    StartTime = new DateTime(2025, 11, 15, 9, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 11, 15, 11, 0, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2025, 11, 14, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Initial",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 20000m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = false,
                    UserId = 5,
                    VehicleInforId = 1
                },
                new Booking
                {
                    BookingId = 3,
                    StartTime = new DateTime(2025, 11, 16, 14, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 11, 16, 16, 0, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2025, 11, 15, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Success",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 25000m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = false,
                    UserId = 5,
                    VehicleInforId = 2
                },
                new Booking
                {
                    BookingId = 4,
                    StartTime = new DateTime(2025, 11, 17, 10, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 11, 17, 12, 0, 0, DateTimeKind.Utc),
                    CheckinTime = new DateTime(2025, 11, 17, 10, 5, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2025, 11, 16, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Check_In",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 15000m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = false,
                    UserId = 5,
                    VehicleInforId = 1
                },
                // Seed data for testing GET /api/customer-booking/activities
                new Booking
                {
                    BookingId = 5,
                    StartTime = new DateTime(2025, 10, 10, 8, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 10, 10, 10, 0, 0, DateTimeKind.Utc),
                    CheckinTime = new DateTime(2025, 10, 10, 8, 5, 0, DateTimeKind.Utc),
                    CheckoutTime = new DateTime(2025, 10, 10, 10, 2, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2025, 10, 9, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Done",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 30000m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = true,
                    UserId = 5,
                    VehicleInforId = 1
                },
                new Booking
                {
                    BookingId = 6,
                    StartTime = new DateTime(2025, 10, 12, 14, 0, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 10, 12, 16, 0, 0, DateTimeKind.Utc),
                    DateBook = new DateTime(2025, 10, 11, 12, 0, 0, DateTimeKind.Utc),
                    Status = "Cancel",
                    GuestName = "Pham Thi D",
                    GuestPhone = "0123456785",
                    TotalPrice = 0m,
                    QRImage = "https://via.placeholder.com/150",
                    UnPaidMoney = 0m,
                    IsRating = false,
                    UserId = 5,
                    VehicleInforId = 2
                }
            );

            // Seed BookingDetails (link Booking 1 with TimeSlot 1)
            modelBuilder.Entity<BookingDetails>().HasData(
                new BookingDetails
                {
                    BookingDetailsId = 1,
                    TimeSlotId = 1,
                    BookingId = 1
                },
                // Link new bookings with existing TimeSlot 2
                new BookingDetails
                {
                    BookingDetailsId = 2,
                    TimeSlotId = 2,
                    BookingId = 2
                },
                new BookingDetails
                {
                    BookingDetailsId = 3,
                    TimeSlotId = 2,
                    BookingId = 3
                },
                new BookingDetails
                {
                    BookingDetailsId = 4,
                    TimeSlotId = 2,
                    BookingId = 4
                },
                new BookingDetails
                {
                    BookingDetailsId = 5,
                    TimeSlotId = 1,
                    BookingId = 5
                },
                new BookingDetails
                {
                    BookingDetailsId = 6,
                    TimeSlotId = 1,
                    BookingId = 6
                }
            );

            // Seed Transaction (for Booking 1, Wallet 5)
            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    TransactionId = 1,
                    Price = 30000m,
                    Status = "Success",
                    PaymentMethod = "Cash",
                    Description = "Payment on checkout",
                    CreatedDate = new DateTime(2024, 1, 10, 10, 2, 0, DateTimeKind.Utc),
                    WalletId = 5,
                    BookingId = 1
                }
            );

            // Seed Bill (for Business 1, Wallet 5)
            modelBuilder.Entity<Bill>().HasData(
                new Bill
                {
                    BillId = 1,
                    Time = new DateTime(2024, 1, 10, 10, 5, 0, DateTimeKind.Utc),
                    Status = "Paid",
                    Price = 30000m,
                    BusinessId = 1,
                    WalletId = 5
                }
            );

            // Seed ApproveParking
            modelBuilder.Entity<ApproveParking>().HasData(
                new ApproveParking
                {
                    ApproveParkingId = 1,
                    Note = "Giám sát hiện trường",
                    NoteForAdmin = "Hồ sơ đạt yêu cầu",
                    CreatedDate = new DateTime(2024, 1, 5, 9, 0, 0, DateTimeKind.Utc),
                    Status = "Approved",
                    StaffId = 3, // Staff user
                    ParkingId = 1
                }
            );

            // Seed FieldWorkParkingImg (for ApproveParking 1)
            modelBuilder.Entity<FieldWorkParkingImg>().HasData(
                new FieldWorkParkingImg
                {
                    FieldWorkParkingImgId = 1,
                    Url = "https://via.placeholder.com/400x300",
                    ApproveParkingId = 1
                }
            );

            // Seed FavoriteAddress (for User 5)
            modelBuilder.Entity<FavoriteAddress>().HasData(
                new FavoriteAddress
                {
                    FavoriteAddressId = 1,
                    TagName = "Nhà",
                    Address = "01 Lê Lợi, Đà Nẵng",
                    UserId = 5
                }
            );

            // Seed ParkingSpotImage
            modelBuilder.Entity<ParkingSpotImage>().HasData(
                new ParkingSpotImage { ParkingSpotImageId = 1, ImgPath = "https://via.placeholder.com/600x400", ParkingId = 1 },
                new ParkingSpotImage { ParkingSpotImageId = 2, ImgPath = "https://via.placeholder.com/600x400", ParkingId = 2 }
            );

            // Seed PayPal (for Manager 2)
            modelBuilder.Entity<PayPal>().HasData(
                new PayPal
                {
                    PayPalId = 1,
                    ClientId = "sample-client-id",
                    SecretKey = "sample-secret",
                    ManagerId = 2
                }
            );

            // Seed VnPay (for User 2)
            modelBuilder.Entity<Domain.Entities.VnPay>().HasData(
                new Domain.Entities.VnPay
                {
                    VnPayId = 1,
                    TmnCode = "TMNCODE123",
                    HashSecret = "HASHSECRET456",
                    UserId = 2
                }
            );

            // Seed ConflictRequest (link to Parking 1 & Booking 1)
            modelBuilder.Entity<ConflictRequest>().HasData(
                new ConflictRequest
                {
                    ConflictRequestId = 1,
                    ParkingId = 1,
                    BookingId = 1,
                    Message = "Tranh chấp về thời gian giữ chỗ",
                    Status = "Resolved"
                }
            );
        }

        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        public async Task SeedDataAsync()
        {
            // Ensure database is created
            await Database.EnsureCreatedAsync();

            // Seed data in order of dependencies
            await SeedTrafficAsync();
            await SeedFeeAsync();
            await SeedUsersAsync();
            await SeedBusinessProfilesAsync();
            await SeedParkingsAsync();
            await SeedFloorsAsync();
            await SeedParkingSlotsAsync();
            await SeedParkingPricesAsync();
            await SeedParkingHasPricesAsync();
            await SeedWalletsAsync();

            await SaveChangesAsync();
        }

        private async Task SeedTrafficAsync()
        {
            if (!await Traffics.AnyAsync())
            {
                var traffics = new List<Traffic>
                {
                    new Traffic { TrafficId = 1, Name = "Xe máy", IsActive = true },
                    new Traffic { TrafficId = 2, Name = "Ô tô", IsActive = true },
                    new Traffic { TrafficId = 3, Name = "Xe đạp", IsActive = true }
                };

                await Traffics.AddRangeAsync(traffics);
            }
        }

        private async Task SeedFeeAsync()
        {
            if (!await Fees.AnyAsync())
            {
                var fees = new List<Fee>
                {
                    new Fee { FeeId = 1, BusinessType = "Cá nhân", Price = 50000, Name = "Phí đăng ký cá nhân", NumberOfParking = "1-5" },
                    new Fee { FeeId = 2, BusinessType = "Doanh nghiệp nhỏ", Price = 100000, Name = "Phí đăng ký doanh nghiệp nhỏ", NumberOfParking = "6-20" },
                    new Fee { FeeId = 3, BusinessType = "Doanh nghiệp lớn", Price = 200000, Name = "Phí đăng ký doanh nghiệp lớn", NumberOfParking = "21-50" },
                    new Fee { FeeId = 4, BusinessType = "Tập đoàn", Price = 500000, Name = "Phí đăng ký tập đoàn", NumberOfParking = "50+" }
                };

                await Fees.AddRangeAsync(fees);
            }
        }

        private async Task SeedUsersAsync()
        {
            if (!await Users.AnyAsync())
            {
                var passwordHash = CreatePasswordHash("123456");

                var users = new List<User>
                {
                    new User
                    {
                        UserId = 1,
                        Name = "Admin",
                        Email = "admin@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456789",
                        RoleId = 1,
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Nam",
                        Address = "Hà Nội, Việt Nam"
                    },
                    new User
                    {
                        UserId = 2,
                        Name = "Nguyễn Văn A",
                        Email = "manager1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456788",
                        RoleId = 1,
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1985, 5, 15),
                        Gender = "Nam",
                        Address = "TP. Hồ Chí Minh, Việt Nam"
                    },
                    new User
                    {
                        UserId = 3,
                        Name = "Trần Thị B",
                        Email = "staff1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456787",
                        RoleId = 4,
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1992, 8, 20),
                        Gender = "Nữ",
                        Address = "Đà Nẵng, Việt Nam",
                        ManagerId = 2
                    },
                    new User
                    {
                        UserId = 4,
                        Name = "Lê Văn C",
                        Email = "keeper1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456786",
                        RoleId = 2,
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1988, 3, 10),
                        Gender = "Nam",
                        Address = "Hải Phòng, Việt Nam",
                        ManagerId = 2
                    },
                    new User
                    {
                        UserId = 5,
                        Name = "Phạm Thị D",
                        Email = "customer1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456785",
                        RoleId = 3,
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1995, 12, 5),
                        Gender = "Nữ",
                        Address = "Cần Thơ, Việt Nam"
                    }
                };

                await Users.AddRangeAsync(users);
            }
        }

        private async Task SeedBusinessProfilesAsync()
        {
            if (!await BusinessProfiles.AnyAsync())
            {
                var businessProfiles = new List<BusinessProfile>
                {
                    new BusinessProfile
                    {
                        BusinessProfileId = 1,
                        Name = "Công ty TNHH Bãi đỗ xe ABC",
                        Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                        FrontIdentification = "https://via.placeholder.com/300x200",
                        BackIdentification = "https://via.placeholder.com/300x200",
                        BusinessLicense = "https://via.placeholder.com/300x200",
                        UserId = 2,
                        Type = "Doanh nghiệp",
                        FeeId = 2
                    },
                    new BusinessProfile
                    {
                        BusinessProfileId = 2,
                        Name = "Bãi đỗ xe XYZ",
                        Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                        FrontIdentification = "https://via.placeholder.com/300x200",
                        BackIdentification = "https://via.placeholder.com/300x200",
                        BusinessLicense = "https://via.placeholder.com/300x200",
                        UserId = 1,
                        Type = "Cá nhân",
                        FeeId = 1
                    }
                };

                await BusinessProfiles.AddRangeAsync(businessProfiles);
            }
        }

        private async Task SeedParkingsAsync()
        {
            if (!await Parkings.AnyAsync())
            {
                var parkings = new List<Domain.Entities.Parking>
                {
                    new Domain.Entities.Parking
                    {
                        ParkingId = 1,
                        Code = "PARK001",
                        Name = "Bãi đỗ xe ABC - Tầng hầm",
                        Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                        Latitude = 10.7769m,
                        Longitude = 106.7009m,
                        Description = "Bãi đỗ xe hiện đại với hệ thống camera giám sát",
                        IsActive = true,
                        IsAvailable = true,
                        MotoSpot = 50,
                        CarSpot = 30,
                        IsFull = false,
                        IsPrepayment = true,
                        IsOvernight = true,
                        Stars = 4.5f,
                        TotalStars = 45f,
                        StarsCount = 10,
                        BusinessId = 1
                    },
                    new Domain.Entities.Parking
                    {
                        ParkingId = 2,
                        Code = "PARK002",
                        Name = "Bãi đỗ xe XYZ - Tầng trệt",
                        Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                        Latitude = 10.7879m,
                        Longitude = 106.7119m,
                        Description = "Bãi đỗ xe ngoài trời với giá cả hợp lý",
                        IsActive = true,
                        IsAvailable = true,
                        MotoSpot = 30,
                        CarSpot = 20,
                        IsFull = false,
                        IsPrepayment = false,
                        IsOvernight = false,
                        Stars = 4.0f,
                        TotalStars = 32f,
                        StarsCount = 8,
                        BusinessId = 2
                    }
                };

                await Parkings.AddRangeAsync(parkings);
            }
        }

        private async Task SeedFloorsAsync()
        {
            if (!await Floors.AnyAsync())
            {
                var floors = new List<Floor>
                {
                    new Floor { FloorId = 1, FloorName = "Tầng B1", IsActive = true, ParkingId = 1 },
                    new Floor { FloorId = 2, FloorName = "Tầng B2", IsActive = true, ParkingId = 1 },
                    new Floor { FloorId = 3, FloorName = "Tầng B3", IsActive = true, ParkingId = 1 },
                    new Floor { FloorId = 4, FloorName = "Khu A", IsActive = true, ParkingId = 2 },
                    new Floor { FloorId = 5, FloorName = "Khu B", IsActive = true, ParkingId = 2 }
                };

                await Floors.AddRangeAsync(floors);
            }
        }

        private async Task SeedParkingSlotsAsync()
        {
            if (!await ParkingSlots.AnyAsync())
            {
                var parkingSlots = new List<ParkingSlot>();

                for (int i = 1; i <= 20; i++)
                {
                    parkingSlots.Add(new ParkingSlot
                    {
                        ParkingSlotId = i,
                        Name = $"A{i:D2}",
                        IsAvailable = true,
                        RowIndex = (i - 1) / 5 + 1,
                        ColumnIndex = (i - 1) % 5 + 1,
                        TrafficId = 2,
                        FloorId = 1,
                        IsBackup = false
                    });
                }

                for (int i = 21; i <= 40; i++)
                {
                    parkingSlots.Add(new ParkingSlot
                    {
                        ParkingSlotId = i,
                        Name = $"B{i - 20:D2}",
                        IsAvailable = true,
                        RowIndex = (i - 21) / 5 + 1,
                        ColumnIndex = (i - 21) % 5 + 1,
                        TrafficId = 2,
                        FloorId = 2,
                        IsBackup = false
                    });
                }

                for (int i = 41; i <= 70; i++)
                {
                    parkingSlots.Add(new ParkingSlot
                    {
                        ParkingSlotId = i,
                        Name = $"M{i - 40:D2}",
                        IsAvailable = true,
                        RowIndex = (i - 41) / 10 + 1,
                        ColumnIndex = (i - 41) % 10 + 1,
                        TrafficId = 1,
                        FloorId = 3,
                        IsBackup = false
                    });
                }

                for (int i = 71; i <= 85; i++)
                {
                    parkingSlots.Add(new ParkingSlot
                    {
                        ParkingSlotId = i,
                        Name = $"C{i - 70:D2}",
                        IsAvailable = true,
                        RowIndex = (i - 71) / 5 + 1,
                        ColumnIndex = (i - 71) % 5 + 1,
                        TrafficId = 2,
                        FloorId = 4,
                        IsBackup = false
                    });
                }

                for (int i = 86; i <= 100; i++)
                {
                    parkingSlots.Add(new ParkingSlot
                    {
                        ParkingSlotId = i,
                        Name = $"D{i - 85:D2}",
                        IsAvailable = true,
                        RowIndex = (i - 86) / 5 + 1,
                        ColumnIndex = (i - 86) % 5 + 1,
                        TrafficId = 1,
                        FloorId = 5,
                        IsBackup = false
                    });
                }

                await ParkingSlots.AddRangeAsync(parkingSlots);
            }
        }

        private async Task SeedParkingPricesAsync()
        {
            if (!await ParkingPrices.AnyAsync())
            {
                var parkingPrices = new List<ParkingPrice>
                {
                    new ParkingPrice
                    {
                        ParkingPriceId = 1,
                        ParkingPriceName = "Giá xe ô tô - Giờ",
                        IsActive = true,
                        IsWholeDay = false,
                        StartingTime = 0,
                        HasPenaltyPrice = true,
                        PenaltyPrice = 5000,
                        PenaltyPriceStepTime = 15,
                        IsExtrafee = true,
                        ExtraTimeStep = 15,
                        BusinessId = 1,
                        TrafficId = 2
                    },
                    new ParkingPrice
                    {
                        ParkingPriceId = 2,
                        ParkingPriceName = "Giá xe ô tô - Ngày",
                        IsActive = true,
                        IsWholeDay = true,
                        StartingTime = 0,
                        HasPenaltyPrice = false,
                        PenaltyPrice = null,
                        PenaltyPriceStepTime = null,
                        IsExtrafee = false,
                        ExtraTimeStep = null,
                        BusinessId = 1,
                        TrafficId = 2
                    },
                    new ParkingPrice
                    {
                        ParkingPriceId = 3,
                        ParkingPriceName = "Giá xe máy - Giờ",
                        IsActive = true,
                        IsWholeDay = false,
                        StartingTime = 0,
                        HasPenaltyPrice = true,
                        PenaltyPrice = 2000,
                        PenaltyPriceStepTime = 15,
                        IsExtrafee = true,
                        ExtraTimeStep = 15,
                        BusinessId = 1,
                        TrafficId = 1
                    },
                    new ParkingPrice
                    {
                        ParkingPriceId = 4,
                        ParkingPriceName = "Giá xe máy - Ngày",
                        IsActive = true,
                        IsWholeDay = true,
                        StartingTime = 0,
                        HasPenaltyPrice = false,
                        PenaltyPrice = null,
                        PenaltyPriceStepTime = null,
                        IsExtrafee = false,
                        ExtraTimeStep = null,
                        BusinessId = 1,
                        TrafficId = 1
                    },
                    new ParkingPrice
                    {
                        ParkingPriceId = 5,
                        ParkingPriceName = "Giá xe ô tô - Giờ",
                        IsActive = true,
                        IsWholeDay = false,
                        StartingTime = 0,
                        HasPenaltyPrice = true,
                        PenaltyPrice = 3000,
                        PenaltyPriceStepTime = 15,
                        IsExtrafee = true,
                        ExtraTimeStep = 15,
                        BusinessId = 2,
                        TrafficId = 2
                    },
                    new ParkingPrice
                    {
                        ParkingPriceId = 6,
                        ParkingPriceName = "Giá xe máy - Giờ",
                        IsActive = true,
                        IsWholeDay = false,
                        StartingTime = 0,
                        HasPenaltyPrice = true,
                        PenaltyPrice = 1000,
                        PenaltyPriceStepTime = 15,
                        IsExtrafee = true,
                        ExtraTimeStep = 15,
                        BusinessId = 2,
                        TrafficId = 1
                    }
                };

                await ParkingPrices.AddRangeAsync(parkingPrices);
            }
        }

        private async Task SeedParkingHasPricesAsync()
        {
            if (!await ParkingHasPrices.AnyAsync())
            {
                var parkingHasPrices = new List<ParkingHasPrice>
                {
                    new ParkingHasPrice { ParkingHasPriceId = 1, ParkingId = 1, ParkingPriceId = 1 },
                    new ParkingHasPrice { ParkingHasPriceId = 2, ParkingId = 1, ParkingPriceId = 2 },
                    new ParkingHasPrice { ParkingHasPriceId = 3, ParkingId = 1, ParkingPriceId = 3 },
                    new ParkingHasPrice { ParkingHasPriceId = 4, ParkingId = 1, ParkingPriceId = 4 },
                    new ParkingHasPrice { ParkingHasPriceId = 5, ParkingId = 2, ParkingPriceId = 5 },
                    new ParkingHasPrice { ParkingHasPriceId = 6, ParkingId = 2, ParkingPriceId = 6 }
                };

                await ParkingHasPrices.AddRangeAsync(parkingHasPrices);
            }
        }

        private async Task SeedWalletsAsync()
        {
            if (!await Wallets.AnyAsync())
            {
                var wallets = new List<Wallet>
                {
                    new Wallet { WalletId = 1, Balance = 1000000, Debt = 0, UserId = 1 },
                    new Wallet { WalletId = 2, Balance = 500000, Debt = 0, UserId = 2 },
                    new Wallet { WalletId = 3, Balance = 200000, Debt = 0, UserId = 3 },
                    new Wallet { WalletId = 4, Balance = 150000, Debt = 0, UserId = 4 },
                    new Wallet { WalletId = 5, Balance = 300000, Debt = 0, UserId = 5 }
                };

                await Wallets.AddRangeAsync(wallets);
            }
        }
    }
}
