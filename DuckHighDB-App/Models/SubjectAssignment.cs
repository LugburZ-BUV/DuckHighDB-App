using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class SubjectAssignment
{
    public int AssignmentId { get; set; }

    public int ClassId { get; set; }

    public int SubjectId { get; set; }

    public int TeacherId { get; set; }

    public string SchoolYear { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;

    public virtual Employee Teacher { get; set; } = null!;
}
