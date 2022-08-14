using Cards.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Cards.Api.Data
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions options) : base(options)
        {
        }

        //Dbset
        public DbSet<Card> Cards { get; set; } 
    }
}
