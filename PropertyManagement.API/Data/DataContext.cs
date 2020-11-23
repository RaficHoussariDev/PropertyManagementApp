using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {}

        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<PurchasedApartment> PurchasedApartments { get; set; }
    }
}