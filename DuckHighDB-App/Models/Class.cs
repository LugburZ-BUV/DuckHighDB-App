using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public string SchoolYear { get; set; } = null!;

    public int? MentorId { get; set; }

    public virtual Employee? Mentor { get; set; }

    public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; } = new List<SubjectAssignment>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
