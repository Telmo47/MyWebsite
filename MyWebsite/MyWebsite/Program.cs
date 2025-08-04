using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using MyWebsite.Data.Seed;
using Website.Hubs; // Include your Hub namespace
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext using SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MVC support
builder.Services.AddControllersWithViews();

// Add support for Razor Pages if needed
builder.Services.AddRazorPages();

// Configure session state
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add Identity with default token providers
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// Add in-memory cache for session
builder.Services.AddDistributedMemoryCache();

// Add SignalR
builder.Services.AddSignalR();

// Add Swagger (optional, remove if you don't want it here)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyWebsite API",
        Version = "v1",
        Description = "API for managing projects, technologies, and contact messages"
    });
});

var app = builder.Build();

// Configure error handling and HSTS for production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Use standard middlewares
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Enable Swagger (optional)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebsite API v1");
    c.RoutePrefix = "swagger"; // Only show Swagger UI at /swagger
});

// Map controllers and Razor pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Map SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

// Seed the database
await app.Services.SeedDataAsync();

app.Run();
