using Microsoft.EntityFrameworkCore;
namespace RestWithAspNetUdemy.Model.Context
{
    public class MySqlContext : DbContext
    {

        public MySqlContext()
        {

        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) { }


        public DbSet<Person> Persons { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
