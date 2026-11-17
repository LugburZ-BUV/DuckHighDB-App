using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Gender { get; set; }

    public string Ssn { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
