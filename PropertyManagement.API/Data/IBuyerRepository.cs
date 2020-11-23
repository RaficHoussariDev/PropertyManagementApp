using System.Threading.Tasks;
using System.Collections.Generic;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public interface IBuyerRepository
    {
        Task<Buyer> CreateBuyer(Buyer buyer, string fullName, int credits);
        Task<bool> VerifyCredit(int buyerId, int apartmentId);
        Task<bool> VerifyPurchased(int id);
        void Buy(int buyerId, int apartmentId);
        Task<bool> BuyerExists(int id);
        Task<bool> ApartmentExists(int id);
        Task<bool> BuyerFullnameExists(string fullName);

        void AddBuyersApartments(Buyer buyer);
        Task<List<Buyer>> GetBuyers();
        Task<List<Apartment>> GetBuyerApartments(int id);
    }
}