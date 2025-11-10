# .NET Framework 4.5 to .NET 9 Migration Guide

## Overview

This document outlines the migration of the MFL_ACC_ERP solution from .NET Framework 4.5 to .NET 9.

## Migration Date
**Branch:** develop-migration
**Date:** November 10, 2025

## Backup Location
All original `.csproj` files have been backed up to `.migration-backup/` directory.

---

## Projects Migrated

### 1. ERP.Shared
**Type:** Class Library
**Dependencies:** None
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- Enabled implicit usings and nullable reference types

### 2. ERP.DAL (Data Access Layer)
**Type:** Class Library
**Dependencies:** Oracle Database
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- **IMPORTANT:** Replaced `Oracle.DataAccess.x86` with `Oracle.ManagedDataAccess.Core` version 23.6.1
- This is the .NET Core/.NET compatible Oracle driver

### 3. ERP.Model
**Type:** Class Library
**Dependencies:** ERP.DAL, ASP.NET Core MVC
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- Added `Microsoft.AspNetCore.App` framework reference
- Kept `Microsoft.Office.Interop.Excel` for Excel functionality

### 4. ERP.Data (Repository Layer)
**Type:** Class Library
**Dependencies:** ERP.DAL, ERP.Model, ERP.Shared
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- Simple class library with project references

### 5. ERP.Core
**Type:** Class Library
**Dependencies:** ERP.Shared, ASP.NET Core Web API
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- Added `Microsoft.AspNetCore.App` framework reference
- Upgraded `Newtonsoft.Json` from 6.0.4 to 13.0.3

### 6. ERP.BLL (Business Logic Layer)
**Type:** Class Library
**Dependencies:** ERP.DAL, ERP.Data, ERP.Model, ERP.Shared
**Changes:**
- Migrated to SDK-style project format
- Target framework: `net9.0`
- Simple class library with project references

### 7. ERPSolution (Main Web Application)
**Type:** ASP.NET Core Web Application
**Dependencies:** All other projects
**Changes:**
- Migrated from ASP.NET MVC 5 to ASP.NET Core 9 MVC
- Changed project SDK to `Microsoft.NET.Sdk.Web`
- Created new `Program.cs` (replaces Global.asax)
- Created `appsettings.json` and `appsettings.Development.json` (replaces Web.config)

---

## Key Package Upgrades

| Old Package | New Package | Notes |
|-------------|-------------|-------|
| `Oracle.DataAccess.x86 2.112.1.0` | `Oracle.ManagedDataAccess.Core 23.6.1` | .NET Core compatible Oracle driver |
| `Newtonsoft.Json 6.0.x` | `Newtonsoft.Json 13.0.3` | Updated for .NET 9 |
| `Microsoft.AspNet.Mvc 5.2.6` | Built-in ASP.NET Core MVC | Part of `Microsoft.AspNetCore.App` |
| `Microsoft.AspNet.WebApi` | Built-in ASP.NET Core Web API | Part of `Microsoft.AspNetCore.App` |
| `Microsoft.AspNet.SignalR 2.2.0` | `Microsoft.AspNetCore.SignalR 1.1.0` | ASP.NET Core version |
| `Hangfire 1.5.1` | `Hangfire.AspNetCore 1.8.14` | Updated for ASP.NET Core |
| `Codaxy.WkHtmlToPdf` | `DinkToPdf 1.0.8` | .NET Core compatible PDF generation |
| `Postal.Mvc5` | `MailKit 4.7.1` | Modern email library for .NET |
| `Ninject` | `Ninject 4.0.0` OR Built-in DI | Can use ASP.NET Core's built-in DI |
| `FluentScheduler 5.0.0` | `FluentScheduler 5.5.1` | Updated version |
| `RazorEngine` | `RazorEngine.NetCore 3.1.0` | .NET Core compatible version |

---

## Critical Code Changes Required

### 1. Oracle Database Access
**File Pattern:** `**/OraDatabase.cs` and any files using `Oracle.DataAccess`

**Old Code:**
```csharp
using Oracle.DataAccess.Client;
```

**New Code:**
```csharp
using Oracle.ManagedDataAccess.Client;
```

### 2. Global.asax → Program.cs
The `Global.asax` file has been replaced with `Program.cs`. You need to:
- Move route configuration from `RouteConfig.cs` to `Program.cs`
- Move bundle configuration from `BundleConfig.cs` to use ASP.NET Core's bundling
- Move filter configuration from `FilterConfig.cs` to `Program.cs`

### 3. Web.config → appsettings.json
Configuration has moved from `Web.config` to `appsettings.json`:
- Connection strings moved to `ConnectionStrings` section
- App settings moved to root or custom sections
- Update connection strings for Oracle and Hangfire

### 4. Dependency Injection (Ninject → ASP.NET Core DI)
**Recommended:** Replace Ninject with ASP.NET Core's built-in DI

**Old Code (NinjectWebCommon.cs):**
```csharp
kernel.Bind<IChartOfAccountService>().To<ChartOfAccountService>();
```

**New Code (Program.cs):**
```csharp
builder.Services.AddScoped<IChartOfAccountService, ChartOfAccountService>();
```

### 5. OWIN → ASP.NET Core Middleware
Remove all OWIN-related code:
- `Startup.cs` (OWIN version)
- OWIN middleware configuration

Replace with ASP.NET Core middleware in `Program.cs` (already done)

### 6. SignalR Hubs
Update SignalR hub registration:

**Old Code (Startup.cs):**
```csharp
app.MapSignalR();
```

**New Code (Program.cs):**
```csharp
app.MapHub<YourHubName>("/hubPath");
```

### 7. Controllers
ASP.NET Core controllers need updates:

**Old Base Class:**
```csharp
public class HomeController : Controller
```

**New Base Class:**
```csharp
public class HomeController : Microsoft.AspNetCore.Mvc.Controller
```

**Namespace Changes:**
- `System.Web.Mvc` → `Microsoft.AspNetCore.Mvc`
- `System.Web.Http` → `Microsoft.AspNetCore.Mvc` (for API controllers)

### 8. Email (Postal → MailKit)
Replace Postal email functionality with MailKit:

**Old Code:**
```csharp
var email = new YourEmail();
email.Send();
```

**New Code:**
```csharp
using MailKit.Net.Smtp;
using MimeKit;

var message = new MimeMessage();
// Configure message
using var client = new SmtpClient();
await client.SendAsync(message);
```

### 9. PDF Generation (WkHtmlToPdf → DinkToPdf)
Update PDF generation code:

**Setup (Program.cs):**
```csharp
// Add DinkToPdf context
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
```

### 10. Area Registration
**Old Code (AreaRegistration.cs):**
```csharp
AreaRegistration.RegisterAllAreas();
```

**New Code (Program.cs):**
```csharp
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
```

---

## Build and Run Steps

### Prerequisites
1. Install .NET 9 SDK from https://dotnet.microsoft.com/download/dotnet/9.0
2. Install Oracle client libraries (if not using managed driver)
3. Update connection strings in `appsettings.json`

### Build the Solution
```bash
cd /home/user/MFL_ACC_ERP
dotnet restore
dotnet build
```

### Run the Application
```bash
cd ERPSolution
dotnet run
```

The application will start on:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000

---

## Testing Checklist

### Phase 1: Build and Compilation
- [ ] All projects build without errors
- [ ] No namespace resolution issues
- [ ] All NuGet packages restore correctly

### Phase 2: Database Connectivity
- [ ] Oracle connection works with `Oracle.ManagedDataAccess.Core`
- [ ] SQL Server connection works (for Hangfire)
- [ ] All stored procedures execute correctly
- [ ] Data reads and writes work as expected

### Phase 3: Web Application
- [ ] Application starts without errors
- [ ] Home page loads
- [ ] Static files (CSS, JS, images) load correctly
- [ ] AngularJS application works
- [ ] Routes work correctly
- [ ] Areas load properly

### Phase 4: Features
- [ ] SignalR hubs connect and communicate
- [ ] Hangfire dashboard accessible
- [ ] Background jobs execute
- [ ] PDF generation works
- [ ] Excel export/import works
- [ ] Email sending works
- [ ] Authentication/Authorization works
- [ ] API endpoints respond correctly

### Phase 5: Performance
- [ ] Application startup time is acceptable
- [ ] Page load times are similar or better
- [ ] Memory usage is acceptable
- [ ] No memory leaks detected

---

## Known Issues and Workarounds

### 1. System.Web Dependencies
If you encounter `System.Web` dependencies:
- Replace with ASP.NET Core equivalents
- `HttpContext.Current` → Inject `IHttpContextAccessor`
- `Server.MapPath` → Use `IWebHostEnvironment.WebRootPath`

### 2. App_Start Folder
The `App_Start` folder configuration files need to be migrated:
- `RouteConfig.cs` → Move to `Program.cs`
- `BundleConfig.cs` → Update to use ASP.NET Core bundling
- `FilterConfig.cs` → Move to `Program.cs`
- `WebApiConfig.cs` → Update in `Program.cs`

### 3. Web.config Transforms
Replace with:
- `appsettings.Development.json`
- `appsettings.Staging.json`
- `appsettings.Production.json`

---

## Rollback Instructions

If you need to rollback to .NET Framework 4.5:

1. Restore original `.csproj` files:
```bash
cp .migration-backup/*.csproj .
cp .migration-backup/ERP.Shared.csproj ERP.Shared/
cp .migration-backup/ERP.DAL.csproj ERP.DAL/
cp .migration-backup/ERP.Model.csproj ERP.Model/
cp .migration-backup/ERP.Data.csproj ERP.Data/
cp .migration-backup/ERP.Core.csproj ERP.Core/
cp .migration-backup/ERP.BLL.csproj ERP.BLL/
cp .migration-backup/ERPSolution.csproj ERPSolution/
```

2. Delete new files:
```bash
rm ERPSolution/Program.cs
rm ERPSolution/appsettings.json
rm ERPSolution/appsettings.Development.json
```

3. Commit the rollback:
```bash
git add .
git commit -m "Rollback to .NET Framework 4.5"
```

---

## Additional Resources

- [Migrate from ASP.NET to ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/migration/proper-to-2x/)
- [.NET 9 Documentation](https://docs.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- [Oracle.ManagedDataAccess.Core Documentation](https://www.oracle.com/database/technologies/appdev/dotnet/odp.html)
- [ASP.NET Core Migration Guide](https://docs.microsoft.com/en-us/aspnet/core/migration/)

---

## Support

For issues or questions about this migration:
1. Check the Known Issues section above
2. Review the official Microsoft migration guides
3. Consult the team lead or senior developers

---

## Next Steps

1. **Review and Test**: Build the solution and run all tests
2. **Update Code**: Apply the critical code changes listed above
3. **Configure Services**: Set up dependency injection in `Program.cs`
4. **Update Connection Strings**: Configure database connections in `appsettings.json`
5. **Test Thoroughly**: Go through the testing checklist
6. **Deploy**: Once testing is complete, deploy to staging environment

---

**Migration completed by:** Claude
**Date:** November 10, 2025
**Branch:** develop-migration
