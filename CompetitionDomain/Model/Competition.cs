using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompetitionDomain.Model;

public partial class Competition
{
    public int Id { get; set; }

    [Display(Name = "Дата проведення")]
    public DateOnly Date { get; set; }

    [Display(Name = "Місце проведення")]
    public string Venue { get; set; } = null!;

    [Display(Name = "Подія")]
    public string? TimedTo { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
