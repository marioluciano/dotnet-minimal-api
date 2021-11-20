public class EmployeeDBContext : DbContext
{
    public EmployeeDBContext(DbContextOptions options) : base(options)
    {

    }

    // protected override void OnModelCreating(DbModelBuilder modelBuilder)
    // {
    //     modelBuilder.HasDefaultSchema("public");
    //     base.OnModelCreating(modelBuilder);
    // }

    public DbSet<Employee>? Employees => Set<Employee>();
}