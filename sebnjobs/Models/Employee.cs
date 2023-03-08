using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sebnjobs.Models;

public partial class Employee
{
    public Employee()
    {
        Job1s = new HashSet<Job1>();
    }
    public int EmployeeId { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }


    [JsonIgnore]
    public virtual ICollection<Job1>? Job1s { get; set; } 
}
