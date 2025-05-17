using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Result
{
    public int Id { get; set; }

    [Display(Name = "Заплив")]
    public int SwimId { get; set; }

    [Display(Name = "Плавець")]
    public int SwimmerId { get; set; }

    [Display(Name = "Час фінішу")]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss.ff}", ApplyFormatInEditMode = true)]
    public TimeOnly? ArrivalTime { get; set; }

    [Display(Name = "Загальний час")]
    [DisplayFormat(DataFormatString = "{0:HH:mm:ss.ff}", ApplyFormatInEditMode = true)]
    public TimeOnly? ResultTime { get; set; }

    [Display(Name = "Призове місце")]
    [Range(1, int.MaxValue, ErrorMessage = "Призове місце - 1, 2 або 3")]
    public int? Place { get; set; }

    [Display(Name = "Заплив")]
    public virtual Swim? Swim { get; set; } = null!;

    [Display(Name = "Плавець")]
    public virtual Swimmer? Swimmer { get; set; } = null!;
}
