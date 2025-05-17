using System;
using System.Collections.Generic;

namespace CompetitionDomain.Model;

public partial class Distance
{
    public int Id { get; set; }

    public string Style { get; set; } = null!;

    public int Length { get; set; }

    public virtual ICollection<Swim> Swims { get; set; } = new List<Swim>();
}
