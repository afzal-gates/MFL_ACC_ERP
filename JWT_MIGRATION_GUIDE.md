# JWT Authentication Migration Plan

## Overview
Replace Session-based authentication (HttpContext.Current.Session["multiScUserId"]) with JWT token-based authentication.

## Benefits of JWT over Session
- ✅ Stateless - No server-side session storage needed
- ✅ Scalable - Works across multiple servers
- ✅ Secure - Digitally signed tokens
- ✅ Modern - Standard for ASP.NET Core APIs
- ✅ Cross-platform - Works with mobile apps, SPAs, etc.

## Implementation Steps

### 1. Install JWT Package
Add to ERPSolution.csproj:
```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
```

### 2. Configure JWT in Program.cs
```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"]; // Store in appsettings.json or environment variable

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
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

// ... rest of your services

var app = builder.Build();

app.UseAuthentication();  // Must come before UseAuthorization
app.UseAuthorization();

// ... rest of your middleware

app.Run();
```

### 3. Add JWT Settings to appsettings.json
```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "https://your-app.com",
    "Audience": "https://your-app.com",
    "TokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }
}
```

### 4. Create JWT Token Service
Create `Services/JwtTokenService.cs`:
```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IJwtTokenService
{
    string GenerateToken(long userId, string userName, string email, List<string> roles);
    ClaimsPrincipal? ValidateToken(string token);
}

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = configuration["JwtSettings:SecretKey"]!;
        _issuer = configuration["JwtSettings:Issuer"]!;
        _audience = configuration["JwtSettings:Audience"]!;
        _expirationMinutes = int.Parse(configuration["JwtSettings:TokenExpirationMinutes"]!);
    }

    public string GenerateToken(long userId, string userName, string email, List<string> roles)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Add roles
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);

        try
        {
            var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return principal;
        }
        catch
        {
            return null;
        }
    }
}
```

### 5. Update BaseApiController to Use Claims
Replace the old session-based UserId property:

**OLD (Session-based):**
```csharp
public string UserId
{
    get
    {
        if (HttpContext?.Session != null)
        {
            var userId = HttpContext.Session.GetString("multiScUserId");
            if (!string.IsNullOrEmpty(userId))
                return userId;
        }
        return string.Empty;
    }
}
```

**NEW (JWT Claims-based):**
```csharp
public long UserId
{
    get
    {
        var userIdClaim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (long.TryParse(userIdClaim, out long userId))
            return userId;
        return 0;
    }
}

public string UserName => User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
public string UserEmail => User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
```

### 6. Update Login Controller
Create/Update login endpoint to return JWT token:

```csharp
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthController(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginModel model)
    {
        // Validate credentials (your existing logic)
        var user = ValidateUserCredentials(model.LOGIN_ID, model.PASSWORD_HASH);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        // Get user roles
        var roles = GetUserRoles(user.SC_USER_ID);

        // Generate JWT token
        var token = _jwtTokenService.GenerateToken(
            user.SC_USER_ID,
            user.USER_NAME_EN,
            user.USER_EMAIL,
            roles
        );

        return Ok(new
        {
            token = token,
            userId = user.SC_USER_ID,
            userName = user.USER_NAME_EN,
            email = user.USER_EMAIL,
            expiresIn = 3600 // seconds
        });
    }

    private ScUserModel? ValidateUserCredentials(string loginId, string password)
    {
        // Your existing validation logic
        // Return user if valid, null if invalid
        return null; // Placeholder
    }

    private List<string> GetUserRoles(long userId)
    {
        // Your existing role retrieval logic
        return new List<string> { "User" }; // Placeholder
    }
}
```

### 7. Update Model Methods to Accept UserId Parameter
Instead of accessing session/context, pass userId as parameter:

**BEFORE:**
```csharp
public string SaveUser()
{
    // ... code ...
    new CommandParameter() {
        ParameterName = "pCREATED_BY",
        Value = Convert.ToInt64(HttpContext.Current.Session["multiScUserId"])
    },
```

**AFTER:**
```csharp
public string SaveUser(long currentUserId)
{
    // ... code ...
    new CommandParameter() {
        ParameterName = "pCREATED_BY",
        Value = currentUserId
    },
```

**Controller calls it:**
```csharp
[HttpPost]
[Authorize]
public IActionResult SaveUser([FromBody] ScUserModel model)
{
    var result = model.SaveUser(this.UserId); // UserId from JWT claims
    return Ok(result);
}
```

### 8. Register JWT Service in Program.cs
```csharp
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
```

### 9. Frontend Changes
**Angular/JavaScript:**
```javascript
// Store token after login
localStorage.setItem('token', response.token);

// Add to HTTP headers
headers: {
  'Authorization': `Bearer ${localStorage.getItem('token')}`
}
```

### 10. Protect API Endpoints
```csharp
[Authorize] // Requires valid JWT token
[HttpGet]
public IActionResult GetData()
{
    var userId = this.UserId; // Automatically from JWT claims
    // ... your logic
}

[Authorize(Roles = "Admin")] // Requires specific role
[HttpDelete]
public IActionResult DeleteData(int id)
{
    // ...
}
```

## Migration Checklist

- [ ] Install JWT Bearer package
- [ ] Configure JWT in Program.cs
- [ ] Add JWT settings to appsettings.json
- [ ] Create JwtTokenService
- [ ] Update BaseApiController UserId property
- [ ] Update all Model methods to accept userId parameter
- [ ] Create/Update AuthController with login endpoint
- [ ] Update frontend to store and send JWT token
- [ ] Test authentication flow
- [ ] Update all API endpoints to use [Authorize] attribute
- [ ] Remove session configuration (if any)

## Security Best Practices

1. **Secret Key**: Store in environment variables or Azure Key Vault, not in source code
2. **HTTPS**: Always use HTTPS in production
3. **Token Expiration**: Keep tokens short-lived (15-60 minutes)
4. **Refresh Tokens**: Implement refresh token mechanism for better UX
5. **Token Revocation**: Consider implementing token blacklist for logout
6. **CORS**: Configure CORS properly for your frontend domains

## Benefits Achieved

✅ No more `HttpContext.Current.Session` issues
✅ Stateless authentication (scales horizontally)
✅ Works with mobile apps and SPAs
✅ Standard ASP.NET Core authentication
✅ Role-based authorization built-in
✅ Better security with token expiration

## Next Steps

1. Implement JwtTokenService
2. Update appsettings.json with JWT settings
3. Modify Program.cs for JWT authentication
4. Update BaseApiController
5. Refactor model methods to accept userId parameter
6. Update controllers to pass UserId from claims
7. Update frontend to handle JWT tokens
