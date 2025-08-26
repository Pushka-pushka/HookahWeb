using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RclubHook.Domain.Repositories;
using RclubHook.Domain.Repositories.Abstract;
using RclubHook.Domain.Repositories.Repositories.EF;
using RclubHook.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
        .ConfigureWarnings(warnings=>warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

var projectConfig = new Config();
builder.Configuration.GetSection("Project").Bind(projectConfig);
builder.Services.AddSingleton(projectConfig);

builder.Services.AddTransient<ITextFieldsRepository, TextFieldsRepository>();
builder.Services.AddTransient<IServiceItemRepository, ServiceItemRepository>();
builder.Services.AddTransient<IEventItemRepository, EventItemRepository>();
builder.Services.AddTransient<IPromoItemRepository, PromoItemRepository>();
builder.Services.AddTransient<DataManager>();


builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = "Rclub";
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login";
    options.AccessDeniedPath = "/Account/AccessDenied";;
    options.SlidingExpiration = true;

});

builder.Services.AddAuthorization(a =>
{
    a.AddPolicy("AdminArea",policy=>policy.RequireRole("admin"));
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("admin");
        policy.RequireAuthenticatedUser();
        policy.AuthenticationSchemes.Add("Identity.Application");
    });
});


var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();


app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();