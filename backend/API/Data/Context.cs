using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions <Context> options) : base(options)
        {
        }
         public DbSet<Item> Items{ get; set; }
         
    }
}