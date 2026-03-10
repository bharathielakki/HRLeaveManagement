using FluentValidation.AspNetCore;
using HRLeaveManagement.Application.Services;
using HRLeaveManagement.Application.Validators;
using HRLeaveManagement.Domain.Interfaces;
using HRLeaveManagement.Infrastructure.Data;
using HRLeaveManagement.Infrastructure.Repositories;
using HRLeaveManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//add service by bharathi
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
builder.Services.AddScoped<LeaveRequestService>();


builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IManagerApprovalRepository, ManagerApprovalRepository>();
builder.Services.AddScoped<ManagerApprovalService>();


builder.Services.AddScoped<IPaymentService, StripePaymentService>();
builder.Services.AddScoped<ManagerApprovalRepository>();

builder.Services.AddScoped<ITeamCalendarRepository, TeamCalendarRepository>();
builder.Services.AddScoped<TeamCalendarService>();

builder.Services.AddScoped<ILeaveBalanceRepository, LeaveBalanceRepository>();
builder.Services.AddScoped<LeaveBalanceService>();

builder.Services.AddScoped<ILeaveReportRepository, LeaveReportRepository>();
builder.Services.AddScoped<LeaveReportService>();

builder.Services.AddScoped<ILeaveReportRepository, LeaveReportRepository>();
builder.Services.AddScoped<LeaveReportService>();

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<LeaveRequestValidator>());


builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();


var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var stripeSettings = builder.Configuration.GetSection("Stripe");

StripeConfiguration.ApiKey = stripeSettings["SecretKey"];


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Error() // Only log errors
    .WriteTo.File(
        path: "Logs/errorlog-.txt", // folder + filename pattern
        rollingInterval: RollingInterval.Day, // new file each day
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"
    )
    .CreateLogger();

builder.Host.UseSerilog(); // Use Serilog for logging
//end
var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(); 

