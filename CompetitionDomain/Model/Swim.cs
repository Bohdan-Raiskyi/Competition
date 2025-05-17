using System;
using System.Collections.Generic;

namespace CompetitionDomain.Model;

public partial class Swim
{
    public int Id { get; set; }

    public int DistanceId { get; set; }

    public int CompetitionId { get; set; }

    public int SwimNumber { get; set; }

    public TimeOnly? StartTime { get; set; }

    public virtual Competition Competition { get; set; } = null!;

    public virtual Distance Distance { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
