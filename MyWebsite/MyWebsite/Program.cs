using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebsite.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add support for Razor Pages if needed
builder.Services.AddRazorPages();


// Configure cookie policy
builder.Services.AddSession(options =>
{
    // Set the session timeout to 30 minutes
    options.IdleTimeout = TimeSpan.FromMinutes(60   );
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Make the session cookie essential
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => // it configures identity options
{
    options.SignIn.RequireConfirmedAccount = false; // Disable email confirmation for simplicity
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddDistributedMemoryCache(); // Add distributed memory cache for session state

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MyWebsite API",
        Version = "v1",
        Description = "API para gestão de projetos, tecnologias e mensagens de contacto"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware do Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebsite API v1");
    c.RoutePrefix = "swagger"; // Apenas acessível em /swagger
});


app.UseRouting();

app.UseSession(); // Enable session middleware

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Enable Razor Pages if needed


app.Run();
