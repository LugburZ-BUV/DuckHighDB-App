using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; } = new List<SubjectAssignment>();
}
