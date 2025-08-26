using System.ComponentModel.DataAnnotations;

namespace RclubHook.Domain.Entities;

public class ServiceItem :EntityBase
{
    [Required(ErrorMessage = "Заполните название услуги")]
    [Display(Name = "Название услуги")]
    public override string Title { get; set; }

    [Display(Name ="Краткое описание услуги")]
    public override string Subtitle { get; set; }

    [Display(Name = "Полное описние услуги")]
    public override string Text { get; set; }

    [Display(Name="Титульная картинка")]
    public override string TitleImagePath { get; set; }
}