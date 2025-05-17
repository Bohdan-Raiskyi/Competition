using System;
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
    public int SwimNumber { get; set; }

    [Display(Name = "Час старту")]
    public TimeOnly? StartTime { get; set; }

    [Display(Name = "Змагання")]
    public virtual Competition? Competition { get; set; } = null!;

    [Display(Name = "Дистанція")]
    public virtual Distance? Distance { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
