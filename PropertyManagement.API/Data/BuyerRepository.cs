using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class BuyerRepository: IBuyerRepository
    {
        private readonly DataContext _context;
        private List<Apartment> purchasedApartments = new List<Apartment>();
        public BuyerRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Buyer> CreateBuyer(Buyer buyer, string fullName, int credits)
        {
            buyer.FullName = fullName;
            buyer.Credit = credits;

            await _context.Buyers.AddAsync(buyer);
            await _context.SaveChangesAsync();

            return buyer;
        }

        public async Task<bool> VerifyCredit(int buyerId, int apartmentId)
        {
            var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Id == buyerId);
            var apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == apartmentId);
            if(buyer.Credit < apartment.Price)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> VerifyPurchased(int id)
        {
            if(await _context.PurchasedApartments.AnyAsync(p => p.Id == id))
            {
                return true;
            }
            return false;
        }

        public async void Buy(int buyerId, int apartmentId)
        {
            var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.Id == buyerId);
            var apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == apartmentId);
    
            PurchasedApartment purchasedApartment = new PurchasedApartment
            {
                Id = apartment.Id,
                OwnerId = buyer.Id
            };
            await _context.PurchasedApartments.AddAsync(purchasedApartment);
            buyer.Credit -= apartment.Price;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BuyerExists(int id)
        {
            if(await _context.Buyers.AnyAsync(x => x.Id == id))
                return true;
            return false;
        }

        public async Task<bool> ApartmentExists(int id)
        {
            if(await _context.Apartments.AnyAsync(p => p.Id == id))
                return true;
            return false;
        }

        public async Task<bool> BuyerFullnameExists(string fullName)
        {
            if(await _context.Buyers.AnyAsync(p => p.FullName == fullName))
                return true;
            return false;
        }

        public async void AddBuyersApartments(Buyer buyer)
        {
            var purchasedApartments = await _context.PurchasedApartments.ToListAsync();
            foreach (var purchasedApartment in purchasedApartments)
            {
                if(purchasedApartment.OwnerId == buyer.Id)
                {
                    var apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == purchasedApartment.Id);
                    buyer.Apartments.Add(apartment);
                }
            }  
        }

        public async Task<List<Buyer>> GetBuyers()
        {
            var buyers = await _context.Buyers.ToListAsync();

            foreach (var buyer in buyers)
            {
                AddBuyersApartments(buyer);   
            }

            return buyers;
        }

        public async Task<List<Apartment>> GetBuyerApartments(int id)
        {
            var buyer = await _context.Buyers.FirstOrDefaultAsync(u => u.Id == id);

            AddBuyersApartments(buyer);

            return buyer.Apartments;
        }
    }
}