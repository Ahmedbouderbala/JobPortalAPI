using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sebnjobs.Models;

public partial class Job
{
    public Job()
    {
        Job1s = new HashSet<Job1>();
    }
    public int JobId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<Job1> Job1s { get; set; } = new List<Job1>();
}
