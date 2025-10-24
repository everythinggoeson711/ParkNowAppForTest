using Hangfire;
using Hangfire.SqlServer;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Parking.FindingSlotManagement.Application;
using Parking.FindingSlotManagement.Application.Behaviours;
using Parking.FindingSlotManagement.Application.Contracts.Infrastructure;
using Parking.FindingSlotManagement.Application.Models;
using Parking.FindingSlotManagement.Infrastructure;
using Parking.FindingSlotManagement.Infrastructure.Extensions;
using Parking.FindingSlotManagement.Infrastructure.HangFire;
using Parking.FindingSlotManagement.Infrastructure.Hubs;
using Parking.FindingSlotManagement.Infrastructure.Repositories;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

//Register the IServiceCollection
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHangfire(hangfire =>
{
    hangfire.SetDataCompatibilityLevel(CompatibilityLevel.Version_170);
    hangfire.UseSimpleAssemblyNameTypeSerializer();
    hangfire.UseRecommendedSerializerSettings();
    hangfire.UseColouredConsoleLogProvider();
    hangfire.UseSqlServerStorage(
                 builder.Configuration.GetConnectionString("DefaultConnection"));

});
builder.Services.AddHangfireServer();
builder.Services.AddTransient<IServiceManagement, ServiceManagement>();
//For Register MiddleWare
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareHandlerService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache(); // Add MemoryCache for OTP registration

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

//for appear summary
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ParkZ Parking Management API",
        Version = "v1",
        Description = "API for Parking Finding and Slot Management System",
        Contact = new OpenApiContact
        {
            Name = "ParkZ Team",
            Email = "support@parkz.com"
        }
    });

    // Add server URL for proper API testing in Swagger
    if (builder.Environment.IsDevelopment())
    {
        c.AddServer(new OpenApiServer
        {
            Url = "https://localhost:7071",
            Description = "Development Server"
        });
    }
    else
    {
        c.AddServer(new OpenApiServer
        {
            Url = "http://103.56.161.75",
            Description = "Production Server"
        });
    }

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer schame. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddAuthorization(op =>
{
    op.AddPolicy("RequireAdminRole", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});


builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
    //etc
});
/*builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});*/


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
string[] urlCors = new string[]
{
    "http://localhost:3000",
    "http://localhost:3001",
    "http://localhost:5178",
    "https://localhost:7071",
    "https://deploy-react-app-to-azure.vercel.app", 
    "https://park-z-manager-web.vercel.app", 
    "https://parkz-admin-web-five.vercel.app",
    "https://parkz-admin-website-eight.vercel.app",
    "https://parkzserver-001-site1.btempurl.com"
};
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(urlCors)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddSignalR();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seeding is handled via EF Core migrations (HasData). Do not run runtime seeding here.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
	var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
	if (!string.IsNullOrEmpty(authHeader) && !authHeader.StartsWith("Bearer "))
	{
		// Thêm tiền tố Bearer nếu chưa có
		context.Request.Headers["Authorization"] = $"Bearer {authHeader}";
	}
	await next();
});
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ParkZ API V1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "ParkZ API Documentation";
    c.DefaultModelsExpandDepth(-1); // Hide schemas section by default
});

// Comment out HTTPS redirection for HTTP-only deployment
// app.UseHttpsRedirection();

app.UseCors();
app.UseRouting();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
	DashboardTitle = "My Website",
	Authorization = new[]
		{
				new HangfireCustomBasicAuthenticationFilter{
					User = "admin",
					Pass = "123456"
				}
			}
});

app.UseMiddleware<LogMiddleware>();


app.UseAuthentication();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/parkz");
    endpoints.MapHangfireDashboard();
});
app.Run();
