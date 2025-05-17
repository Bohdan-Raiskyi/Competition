using System;
using System.Collections.Generic;

namespace CompetitionDomain.Model;

public partial class Result
{
    public int Id { get; set; }

    public int SwimId { get; set; }

    public int SwimmerId { get; set; }

    public TimeOnly? ArrivalTime { get; set; }

    public TimeOnly? ResultTime { get; set; }

    public int? Place { get; set; }

    public virtual Swim Swim { get; set; } = null!;

    public virtual Swimmer Swimmer { get; set; } = null!;
}
