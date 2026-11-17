using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Ssn { get; set; } = null!;

    public DateOnly HireDate { get; set; }

    public decimal Salary { get; set; }

    public int PositionId { get; set; }

    public int DepartmentId { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual EmployeePosition Position { get; set; } = null!;

    public virtual ICollection<SubjectAssignment> SubjectAssignments { get; set; } = new List<SubjectAssignment>();
}
