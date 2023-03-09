﻿using DALBookProject.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Se‌​rvices.AddDbContext<BookDbContext>(options =>
//{
//    options.UseSqlServer(
//            @"Server=DESKTOP-AJ10D55\\SQLEXPRESS;Database=BookStoreDB;Trusted_Connection=True",
//            options => options.EnableRetryOnFailure());

//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        option.LoginPath = "/Account/Login";
        option.AccessDeniedPath = "/Account/Login"; 
    });

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(5);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "checkuser",
//        pattern: "checkuser/{Email}",
//        defaults: new { controller = "Account", action = "IsUserExist" });
//});



app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
