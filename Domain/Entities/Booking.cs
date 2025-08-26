using System.ComponentModel.DataAnnotations;

namespace RclubHook.Domain.Entities;

public class Booking
{
    public int Id { get; set; }


    [Required]
    [Display(Name = "Контактное имя")]
    public string VisitorName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [Display(Name = "Ваша Email")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [Phone]
    [Display(Name = "Номер для связи")]
    public string Phone { get; set; } = string.Empty;
    
    [Display(Name = "Желаемая дата")]
    public DateTime DesiredDate { get; set; }
    
    [Display(Name = "Количество людей")]
    public int VisitorCount { get; set; }
    
    [Display(Name = "Коментарии")]
    public string Comments { get; set; } = string.Empty;
    
    public DateTime BookingDate { get; set; }
}