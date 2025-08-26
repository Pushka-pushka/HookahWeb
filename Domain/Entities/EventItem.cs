using System.ComponentModel.DataAnnotations;

namespace RclubHook.Domain.Entities;

public class EventItem: EntityBase
{
    [Required(ErrorMessage = "Заполните название мероприятия")]
    [Display(Name = "Название мероприятия")]
    public override string Title { get; set; }

    [Display(Name ="Краткое описание описание")]
    public override string Subtitle { get; set; }

    [Display(Name = "Полное описние описание")]
    public override string Text { get; set; }

    [Display(Name="Титульная картинка")]
    public override string TitleImagePath { get; set; }
}