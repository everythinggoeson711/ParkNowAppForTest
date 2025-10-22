using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Infrastructure.Cloudinary;
using Parking.FindingSlotManagement.Infrastructure.Firebase.PushService;
using Parking.FindingSlotManagement.Infrastructure.Mail;
using Parking.FindingSlotManagement.Infrastructure.Persistences;
using Parking.FindingSlotManagement.Infrastructure.Repositories;
using Parking.FindingSlotManagement.Infrastructure.Repositories.AuthenticationRepositories;
using Parking.FindingSlotManagement.Infrastructure.VnPay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ParkZDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBusinessManagerAuthenticationRepository, BusinessManagerAuthenticationRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITrafficRepository, TrafficRepository>();
            services.AddScoped<IParkingRepository, ParkingRepository>();
            //services.AddScoped<IStaffParkingRepository, StaffParkingRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IFavoriteAddressRepository, FavoriteAddressRepository>();
            services.AddScoped<IVehicleInfoRepository, VehicleInfoRepository>();
            services.AddScoped<IBusinessProfileRepository, BusinessProfileRepository>();
            services.AddScoped<IAdminAuthenticationRepository, AdminAuthenticationRepository>();
            services.AddScoped<IStaffAuthenticationRepository, StaffAuthenticationRepository>();
            services.AddScoped<IPackagePriceRepository, PackagePriceRepository>();
            services.AddScoped<IVnPayRepository, VnPayRepository>();
            services.AddScoped<IParkingHasPriceRepository, ParkingHasPriceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IOTPRepository, OTPRepository>();
            services.AddScoped<IPaypalRepository, PaypalRepository>();
            services.AddScoped<IParkingSpotImageRepository, ParkingSpotImageRepository>();
            services.AddScoped<IParkingPriceRepository, ParkingPriceRepository>();
            services.AddScoped<ITimelineRepository, TimelineRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IApproveParkingRepository, ApproveParkingRepository>();
            services.AddScoped<IFieldWorkParkingImgRepository, FieldWorkParkingImgRepository>();
            services.AddScoped<IBookingDetailsRepository, BookingDetailsRepository>();
            services.AddScoped<IFeeRepository, FeeRepository>();
            services.AddScoped<IBookingDetailsRepository, BookingDetailsRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IConflictRequestRepository, ConflictRequestRepository>();
            services.AddScoped<IHangfireRepository, HangfireRepository>();
            
            // Firebase configuration - đường dẫn tương đối từ working directory
            var firebaseCredentialPath = Path.Combine(AppContext.BaseDirectory, "Firebase", "parknowapp-6cefc-firebase-adminsdk-fbsvc-feeb7ffc25.json");
            
            // Fallback: thử đường dẫn khác nếu file không tồn tại
            if (!File.Exists(firebaseCredentialPath))
            {
                // Thử đường dẫn từ Infrastructure project (development)
                firebaseCredentialPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Parking.FindingSlotManagement.Infrastructure", "Firebase", "parknowapp-6cefc-firebase-adminsdk-fbsvc-feeb7ffc25.json");
            }
            
            if (File.Exists(firebaseCredentialPath))
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(firebaseCredentialPath)
                });
            }
            else
            {
                // Log warning nhưng không crash app
                Console.WriteLine($"WARNING: Firebase credential file not found at: {firebaseCredentialPath}");
                Console.WriteLine("Firebase Push Notification service will not be available.");
            }

            services.AddScoped<IFireBaseMessageServices, FireBaseMessageServices>();
            services.AddScoped<IVnPayService, VnPayService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
            return services;
        }
    }
}
