using Microsoft.EntityFrameworkCore;
using web_app.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register DbContext with SQL Server
builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Register JwtHandler
builder.Services.AddTransient<JwtHandler>();

// Register HttpClient services
builder.Services.AddHttpClient<IUserService, UserService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
})
.AddHttpMessageHandler<JwtHandler>();

builder.Services.AddHttpClient<IPrivilegeService, PrivilegeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
})
.AddHttpMessageHandler<JwtHandler>();

// Register BlogService
builder.Services.AddScoped<IBlogService, BlogService>();

// Configure authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Configure authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewerOrAdmin", policy =>
    policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == ClaimTypes.Role && (claim.Value == "VIEWER" || claim.Value == "ADMIN"))));

    options.AddPolicy("AdminOrEditor", policy =>
    policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == ClaimTypes.Role && (claim.Value == "EDITOR" || claim.Value == "ADMIN"))));

    options.AddPolicy("ViewerOrAdminOrEditor", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(claim => claim.Type == ClaimTypes.Role &&
                                           (claim.Value == "VIEWER" || claim.Value == "ADMIN" || claim.Value == "EDITOR"))));

    options.AddPolicy("Admin", policy =>
        policy.RequireRole("ADMIN"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
