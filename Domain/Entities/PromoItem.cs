using System.ComponentModel.DataAnnotations;

namespace RclubHook.Domain.Entities;

public class PromoItem: EntityBase
{
    [Required(ErrorMessage = "Заполните название акции")]
    [Display(Name = "Название акции")]
    public override string Title { get; set; }

    [Display(Name ="Краткое описание акции")]
    public override string Subtitle { get; set; }

    [Display(Name = "Полное описние акции")]
    public override string Text { get; set; }

    [Display(Name="Титульная картинка")]
    public override string TitleImagePath { get; set; }
}