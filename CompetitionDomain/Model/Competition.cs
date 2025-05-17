using System;
using System.Collections.Generic;

namespace CompetitionDomain.Model;

public partial class Competition
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string Venue { get; set; } = null!;

    public string? TimedTo { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
