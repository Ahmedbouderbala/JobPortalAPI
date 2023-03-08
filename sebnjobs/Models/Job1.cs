using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace sebnjobs.Models;

public partial class Job1
{
    public int EmployeeId { get; set; }

    public int JobId { get; set; }

    public string? Description { get; set; }

    public string? FilePdfName { get; set; }

   

    [JsonIgnore]
    public virtual Employee? Employee { get; set; }

    [JsonIgnore]
    public virtual Job? Job { get; set; }



}
