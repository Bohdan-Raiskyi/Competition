using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Competition
{
    public int Id { get; set; }

    [Display(Name = "Дата проведення")]
    [Required(ErrorMessage = "Поле 'Дата проведення' є обов'язковим.")]
    public DateOnly Date { get; set; }

    [Display(Name = "Місце проведення")]
    [Required(ErrorMessage = "Поле 'Місце проведення' є обов'язковим.")]
    [StringLength(100, ErrorMessage = "Поле 'Місце проведення' не може перевищувати 100 символів.")]
    public string Venue { get; set; } = null!;

    [Display(Name = "Подія")]
    [StringLength(100, ErrorMessage = "Поле 'Подія' не може перевищувати 100 символів.")]
    public string? TimedTo { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
