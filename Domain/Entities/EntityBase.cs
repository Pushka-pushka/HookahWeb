using System.ComponentModel.DataAnnotations;

namespace RclubHook.Domain.Entities;

public abstract class EntityBase
{
    [Required]
    public Guid Id { get; set; }

    [Display(Name = "Название (заголовок)")]
    public virtual string Title { get; set; } = string.Empty;
    
    [Display(Name="Краткое описание")]
    public virtual string Subtitle { get; set; } = string.Empty;

    
    [Display(Name ="Полное описание")]
    public virtual string Text { get; set; } = string.Empty;

    
    [Display(Name = "Tитульная картинка")]
    public virtual string TitleImagePath { get; set; } = string.Empty;

    
    [Display(Name = "SEO метатег Title")]
    public string MetaTitle { get; set; } = string.Empty;

    
    [Display(Name = "SEO метатег Description")]
    public string  MetaDescription { get; set; } =string.Empty;

    
    [Display(Name = "SEO метатег Keywords")]
    public string MetaKeywords { get; set; } = string.Empty;

    
    [DataType(DataType.Time)]
    public DateTime DateAdded { get; set; }
}