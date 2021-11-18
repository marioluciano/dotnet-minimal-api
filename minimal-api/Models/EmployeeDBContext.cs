using Microsoft.EntityFrameworkCore;

public class EmployeeDBContext : DbContext
{
    public EmployeeDBContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Employee>? Employees { get; set; }
}