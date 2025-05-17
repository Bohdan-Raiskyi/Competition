using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Swimmer
{
    public int Id { get; set; }

    [Display(Name = "ПІБ плавця")]
    public string Name { get; set; } = null!;

    [Display(Name = "Назва команди")]
    public string? TeamName { get; set; }

    [Display(Name = "Вікова категорія")]
    public string AgeCategory { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
