using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Distance
{
    public int Id { get; set; }

    [Display(Name = "Стиль")]
    public string Style { get; set; } = null!;

    [Display(Name = "Дистанція")]
    public int Length { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
