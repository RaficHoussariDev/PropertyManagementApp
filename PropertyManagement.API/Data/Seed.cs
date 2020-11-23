using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using PropertyManagement.API.Models;
namespace PropertyManagement.API.Data
{
    public class Seed
    {
        public static void SeedApartments(DataContext context)
        {
            if(!context.Apartments.Any())
            {
                var apartmentData = System.IO.File.ReadAllText("Data/SeedApartments.json");
                var apartments = JsonConvert.DeserializeObject<List<Apartment>>(apartmentData);
                foreach (var apartment in apartments)
                {
                    context.Apartments.Add(apartment);
                }
            }
        }
        public static void SeedBuyers(DataContext context)
        {
            if(!context.Buyers.Any())
            {
                var buyerData = System.IO.File.ReadAllText("Data/SeedBuyers.json");
                var buyers = JsonConvert.DeserializeObject<List<Buyer>>(buyerData);
                foreach (var buyer in buyers)
                {
                    context.Buyers.Add(buyer);
                }
            }
            context.SaveChanges();
        }
    }
}