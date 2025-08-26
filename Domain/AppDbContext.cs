using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RclubHook.Domain.Entities;

namespace RclubHook.Domain.Repositories;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
    
    public DbSet<TextField> TextFields { get; set; }
    public DbSet<EventItem> EventItems { get; set; }
    public DbSet<ServiceItem> ServiceItems { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<PromoItem> PromoItems { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);

        var adminRoleId = "BE123BA3-1380-4DA9-9DF6-A5784FB1497B";
        var adminUserId = "08688E7E-4F94-4818-A822-0DC1CDFD3BE6";
        
        // Создаем экземпляр PasswordHasher
        var hasher = new PasswordHasher<IdentityUser>();

        // Seed данные для роли
        modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = adminRoleId,
            Name = "admin", // ← Исправлено на нижний регистр
            NormalizedName = "ADMIN",
            ConcurrencyStamp = Guid.NewGuid().ToString() // Добавлен ConcurrencyStamp
        });
        
        // Seed данные для пользователя
        var adminUser = new IdentityUser
        {
            Id = adminUserId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "ad@mail.ru",
            NormalizedEmail = "AD@MAIL.RU", // Добавлено
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "admin69"), // Исправлено
            SecurityStamp = Guid.NewGuid().ToString(), // Исправлено
            ConcurrencyStamp = Guid.NewGuid().ToString(), // Добавлено
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        };
        
        modelBuilder.Entity<IdentityUser>().HasData(adminUser);
        
        // Связь пользователя с ролью
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });
       
        
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("D9870D01-C967-4E2E-89DE-5B5E010410F3"),
            CodeWord = "PageIndex",
            Title = "Главная"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id =Guid.Parse("6825B379-2D9D-4B6B-8FE4-836A8F088E7D"),
            CodeWord = "PagePromo",
            Title = "Акции"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("C967053E-3CB5-49E2-B88C-199214532306"),
            CodeWord = "PageAbout",
            Title = "O нас"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("785D4C6D-C83D-43BD-9438-6C5135B8EEFB"),
            CodeWord = "PageEvents",
            Title = "Мероприятия"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("E5667212-16D5-4927-A403-506D0F0822C2"),
            CodeWord = "PageServices",
            Title = "Услуги"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("161852BA-BD7A-458D-86D4-4ADCB004417A"),
            CodeWord = "PageContacts",
            Title = "Контакты"
        });
        
        modelBuilder.Entity<TextField>().HasData(new TextField
        {
            Id = Guid.Parse("B22B4FE5-B826-4217-9FEC-CFFBEC724C8D"),
            CodeWord = "PageBooking",
            Title = "Бронь столика"
        });


    }
}