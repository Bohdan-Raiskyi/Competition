using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Distance
{
    public int Id { get; set; }

    [Display(Name = "Стиль")]
    [Required(ErrorMessage = "Поле 'Стиль' є обов'язковим.")]
    [StringLength(50, ErrorMessage = "Поле 'Стиль' не може перевищувати 50 символів.")]
    public string Style { get; set; } = null!;

    [Display(Name = "Дистанція")]
    [Required(ErrorMessage = "Поле 'Дистанція' є обов'язковим.")]
    [Range(1, int.MaxValue, ErrorMessage = "Дистація - натуральне число в метрах")]
    public int Length { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
