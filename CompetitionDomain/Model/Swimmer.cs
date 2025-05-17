using System;
using System.Collections.Generic;

namespace CompetitionDomain.Model;

public partial class Swimmer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? TeamName { get; set; }

    public string AgeCategory { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();
}
