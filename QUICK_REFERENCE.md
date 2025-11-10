# .NET 9 Migration Quick Reference

## Common Namespace Changes

| .NET Framework 4.5 | .NET 9 |
|-------------------|---------|
| `System.Web.Mvc` | `Microsoft.AspNetCore.Mvc` |
| `System.Web.Http` | `Microsoft.AspNetCore.Mvc` |
| `System.Web.Routing` | `Microsoft.AspNetCore.Routing` |
| `System.Net.Mail` | Use `MailKit` |
| `Oracle.DataAccess.Client` | `Oracle.ManagedDataAccess.Client` |

## HttpContext Changes

```csharp
// OLD (.NET Framework)
var user = HttpContext.Current.User;
var path = Server.MapPath("~/Content");
var request = HttpContext.Current.Request;

// NEW (.NET 9) - Inject IHttpContextAccessor
public class YourController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IWebHostEnvironment _environment;

    public YourController(IHttpContextAccessor httpContextAccessor,
                         IWebHostEnvironment environment)
    {
        _httpContextAccessor = httpContextAccessor;
        _environment = environment;
    }

    public IActionResult YourAction()
    {
        var user = _httpContextAccessor.HttpContext.User;
        var path = Path.Combine(_environment.WebRootPath, "Content");
        var request = _httpContextAccessor.HttpContext.Request;
    }
}
```

## Configuration Access

```csharp
// OLD (.NET Framework)
var value = ConfigurationManager.AppSettings["KeyName"];
var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

// NEW (.NET 9) - Inject IConfiguration
public class YourService
{
    private readonly IConfiguration _configuration;

    public YourService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void YourMethod()
    {
        var value = _configuration["KeyName"];
        var connStr = _configuration.GetConnectionString("DefaultConnection");
    }
}
```

## Dependency Injection Setup

Add this to `Program.cs`:

```csharp
// Services
builder.Services.AddScoped<IChartOfAccountService, ChartOfAccountService>();
builder.Services.AddScoped<ICheckerMakerService, CheckerMakerService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICostCenterGroupService, CostCenterGroupService>();
builder.Services.AddScoped<ICostCenterService, CostCenterService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IPaymentModeService, PaymentModeService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IStockClosingService, StockClosingService>();
builder.Services.AddScoped<IUnilayerChartofAccountService, UnilayerChartofAccountService>();
builder.Services.AddScoped<IDraftVoucherService, DraftVoucherService>();
builder.Services.AddScoped<IVoucherMasterService, VoucherMasterService>();
builder.Services.AddScoped<IVoucherTypeService, VoucherTypeService>();

// Repositories
builder.Services.AddScoped<IChartOfAccountRepository, ChartOfAccountRepository>();
builder.Services.AddScoped<IAccountClassRepository, AccountClassRepository>();
builder.Services.AddScoped<ICheckerMakerRepository, CheckerMakerRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICostCenterRepository, CostCenterRepository>();
builder.Services.AddScoped<ICostCneterGroupRepository, CostCneterGroupRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IMinClassRepository, MainClassRepository>();
builder.Services.AddScoped<IParentClassRepository, ParentClassRepository>();
builder.Services.AddScoped<IPaymentModeRepository, PaymentModeRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IStockClosingRepository, StockClosingRepository>();
builder.Services.AddScoped<ISubClassRepositroy, SubClassRepositroy>();
builder.Services.AddScoped<IMapClassRepository, MapClassRepository>();
builder.Services.AddScoped<IDraftVoucherRepository, DraftVoucherRepository>();
builder.Services.AddScoped<IVoucherMasterRepository, VoucherMasterRepository>();
builder.Services.AddScoped<IVoucherTypeRepository, VoucherTypeRepository>();

// Add HttpContextAccessor if needed
builder.Services.AddHttpContextAccessor();
```

## Controller Base Classes

```csharp
// OLD (.NET Framework)
public class AccountingController : System.Web.Mvc.Controller
{
}

public class ApiController : System.Web.Http.ApiController
{
}

// NEW (.NET 9)
using Microsoft.AspNetCore.Mvc;

public class AccountingController : Controller
{
}

// For API controllers, use [ApiController] attribute
[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { message = "Hello" });
    }
}
```

## Action Results

```csharp
// OLD (.NET Framework)
return Json(data, JsonRequestBehavior.AllowGet);
return Content("text");
return RedirectToAction("Index");

// NEW (.NET 9)
return Json(data); // AllowGet not needed
return Content("text");
return RedirectToAction("Index"); // Same
```

## Routing

```csharp
// OLD (.NET Framework) - RouteConfig.cs
routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
);

// NEW (.NET 9) - Program.cs
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// For areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
```

## Filters

```csharp
// OLD (.NET Framework)
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (!filterContext.Controller.ViewData.ModelState.IsValid)
        {
            filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}

// NEW (.NET 9)
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}

// Register in Program.cs
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidateModelAttribute>();
});
```

## SignalR Hub Registration

Find your hub files (in `Hubs/` folder) and register them in `Program.cs`:

```csharp
// Example: If you have NotificationHub.cs
app.MapHub<NotificationHub>("/notificationHub");

// If you have multiple hubs
app.MapHub<ChatHub>("/chatHub");
app.MapHub<DashboardHub>("/dashboardHub");
```

## Oracle Database Code Changes

```csharp
// OLD (.NET Framework)
using Oracle.DataAccess.Client;

// NEW (.NET 9)
using Oracle.ManagedDataAccess.Client;

// Usage remains the same
using (var connection = new OracleConnection(connectionString))
{
    connection.Open();
    // Your code
}
```

## Important Files to Update

1. **Program.cs** - Add all DI registrations
2. **appsettings.json** - Add connection strings and configuration
3. **Controllers/** - Update namespaces and base classes
4. **Hubs/** - Register SignalR hubs in Program.cs
5. **Models/** - No major changes needed
6. **Views/** - Minimal changes, mostly layout updates

## Files to Delete

- `Global.asax` and `Global.asax.cs`
- `Web.config` (keep for reference until migration is complete)
- `App_Start/` folder contents (after migrating to Program.cs)
- `packages.config` files (NuGet uses PackageReference now)

## Build and Run

```bash
# Restore packages
dotnet restore

# Build
dotnet build

# Run
cd ERPSolution
dotnet run

# Or use watch for development
dotnet watch run
```

## Common Errors and Solutions

### Error: "The type or namespace name 'Mvc' does not exist"
**Solution:** Change `using System.Web.Mvc;` to `using Microsoft.AspNetCore.Mvc;`

### Error: "Oracle.DataAccess could not be found"
**Solution:** Update to `using Oracle.ManagedDataAccess.Client;`

### Error: "HttpContext does not contain a definition for 'Current'"
**Solution:** Inject `IHttpContextAccessor` in your constructor

### Error: "ConfigurationManager does not exist"
**Solution:** Inject `IConfiguration` in your constructor

### Error: "Server.MapPath does not exist"
**Solution:** Inject `IWebHostEnvironment` and use `_environment.WebRootPath`

---

**Quick Tip:** Use Find and Replace (Ctrl+H) in your IDE to quickly update common patterns across the solution.
