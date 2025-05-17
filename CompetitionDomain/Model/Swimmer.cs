using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Swimmer
{
    public int Id { get; set; }

    [Display(Name = "ПІБ плавця")]
    [Required(ErrorMessage = "Поле 'ПІБ плавця' є обов'язковим.")]
    [StringLength(100, ErrorMessage = "Поле 'ПІБ плавця' не може перевищувати 100 символів.")]
    public string Name { get; set; } = null!;

    [Display(Name = "Назва команди")]
    [StringLength(100, ErrorMessage = "Поле 'Назва команди' не може перевищувати 100 символів.")]
    public string? TeamName { get; set; }

    [Display(Name = "Вікова категорія")]
    [Required(ErrorMessage = "Поле 'Вікова категорія' є обов'язковим.")]
    [StringLength(50, ErrorMessage = "Поле 'Вікова категорія' не може перевищувати 50 символів.")]
    [RegularExpression(
        @"^\d{4}(-\d{4})?$",
        ErrorMessage = "Формат: YYYY або YYYY-YYYY (наприклад, 1990 або 1990-1991)."
    )]
    public string AgeCategory { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
