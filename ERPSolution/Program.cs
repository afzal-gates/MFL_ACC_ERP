using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ERPSolution.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Add SignalR
builder.Services.AddSignalR();

// Add Hangfire services
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT SecretKey not configured");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Set to true in production
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// Register Exception Handler
builder.Services.AddExceptionHandler<ERP.Core.GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Register JWT Token Service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

// TODO: Configure Dependency Injection
// Replace Ninject bindings with builder.Services.AddScoped/AddTransient/AddSingleton
// Example:
// builder.Services.AddScoped<IChartOfAccountService, ChartOfAccountService>();
// builder.Services.AddScoped<IVoucherMasterService, VoucherMasterService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Use custom exception handler for all environments
app.UseExceptionHandler(options => { });

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors();

// IMPORTANT: Authentication must come before Authorization
app.UseAuthentication();
app.UseAuthorization();

// Configure Hangfire Dashboard
app.UseHangfireDashboard();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Map SignalR hubs
// TODO: Map your SignalR hubs here
// Example: app.MapHub<YourHub>("/yourHubPath");

app.Run();
