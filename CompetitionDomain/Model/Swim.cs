﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Swim
{
    public int Id { get; set; }

    [Display(Name = "Дистанція")]
    public int DistanceId { get; set; }

    [Display(Name = "Змагання")]
    public int CompetitionId { get; set; }

    [Display(Name = "Номер запливу")]
    [Required(ErrorMessage = "Поле 'Номер запливу' є обов'язковим.")]
    [Range(1, int.MaxValue, ErrorMessage = "'Номер запливу' - натуральне число")]
    public int SwimNumber { get; set; }

    [Display(Name = "Час старту")]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss.ff}", ApplyFormatInEditMode = true)]
    public TimeOnly? StartTime { get; set; }

    [Display(Name = "Змагання")]
    public virtual Competition? Competition { get; set; } = null!;

    [Display(Name = "Дистанція")]
    public virtual Distance? Distance { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
