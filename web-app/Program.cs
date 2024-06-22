using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddDbContext<web_app.Models.BlogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddTransient<web_app.Models.JwtHandler>();

builder.Services.AddHttpClient<web_app.Models.IUserService, web_app.Models.UserService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
})
.AddHttpMessageHandler<web_app.Models.JwtHandler>();

builder.Services.AddHttpClient<web_app.Models.IPrivilegeService, web_app.Models.PrivilegeService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/");
})
.AddHttpMessageHandler<web_app.Models.JwtHandler>();

builder.Services.AddScoped<web_app.Models.IBlogService, web_app.Models.BlogService>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();