using CISConnect.Data;
using CISConnect.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Threading.RateLimiting;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Warning()
    .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/cisconnect-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 14,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

var sentryDsn = builder.Configuration["Sentry:Dsn"];
if (!string.IsNullOrWhiteSpace(sentryDsn))
{
    builder.WebHost.UseSentry(o =>
    {
        o.Dsn = sentryDsn;
        o.Debug = builder.Configuration.GetValue<bool>("Sentry:Debug");
        o.TracesSampleRate = builder.Configuration.GetValue<double>("Sentry:TracesSampleRate", 0.2);
        o.MinimumBreadcrumbLevel = Microsoft.Extensions.Logging.LogLevel.Warning;
        o.MinimumEventLevel = Microsoft.Extensions.Logging.LogLevel.Error;
    });
}

builder.Services.AddControllersWithViews(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

// Connection string resolved from User Secrets (development) or environment variable (production).
// Never commit real credentials to appsettings.json — use `dotnet user-secrets set` locally.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("DefaultConnection string not found. Set it via User Secrets or an environment variable.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 36))));

// Admin-only panel: relaxed password policy, no email confirmation needed.
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/admin/auth/login";
    options.AccessDeniedPath = "/admin/auth/login";
    options.Cookie.Name = "CISConnect.Admin";
    options.ExpireTimeSpan = TimeSpan.FromDays(7);
});

// Rate limiting: cap the live-search endpoint to 40 requests per 10 seconds per client.
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddOutputCache(options =>
{
    options.AddPolicy("public-lists", p => p.Expire(TimeSpan.FromSeconds(60)).Tag("public"));
});

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("search-live", opt =>
    {
        opt.Window           = TimeSpan.FromSeconds(10);
        opt.PermitLimit      = 40;
        opt.QueueLimit       = 0;
        opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

var app = builder.Build();

// Run EF Core migrations and seed reference data on startup.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    await DbSeeder.SeedAsync(dbContext);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    const string adminEmail = "admin@cisconnect.local";
    if (await userManager.FindByEmailAsync(adminEmail) is null)
    {
        var admin = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        await userManager.CreateAsync(admin, "Admin1234!");
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error", "?statusCode={0}");
app.UseHttpsRedirection();

// Basic security headers on every response (OWASP recommendations).
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
    ctx.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
    ctx.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    ctx.Response.Headers["X-XSS-Protection"] = "0";
    ctx.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    await next();
});

// Serve .mov as video/mp4 so Chrome/Firefox will attempt playback (they skip video/quicktime).
var contentTypeProvider = new FileExtensionContentTypeProvider();
contentTypeProvider.Mappings[".mov"] = "video/mp4";
app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = contentTypeProvider });

app.UseRouting();
app.UseOutputCache();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Updates}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
