using System;
using System.Collections.Generic;

namespace DuckHighDB_App.Models;

public partial class EmployeePosition
{
    public int PositionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
