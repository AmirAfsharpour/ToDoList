using Microsoft.EntityFrameworkCore;
namespace MyToDoList.Data
{
    public class MyConnection : DbContext
    {
        public DbSet<Models.task> tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial Catalog=Task; Integrated Security=True; Trust Server Certificate=True;");
        }
    }
}
