using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using PropertyManagement.API.Helpers;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public class ApartmentRepository: IApartmentRepository
    {
        private readonly DataContext _context;
        public ApartmentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Apartment> CreateApartment(Apartment apartment, string title, int nbOfRooms, string address)
        {
            apartment.Title = title;
            apartment.NbOfRooms = nbOfRooms;
            apartment.Address = address;
            apartment.Price = nbOfRooms * 15000;

            await _context.Apartments.AddAsync(apartment);
            await _context.SaveChangesAsync();

            return apartment;
        }

        public async Task<PagedList<Apartment>> GetApartments(ApartmentParams apartmentParams)
        {
            var apartments = _context.Apartments.OrderByDescending(p => p.Price).AsQueryable();

            if(apartmentParams.MinPrice != 0 || apartmentParams.MaxPrice != int.MaxValue)
            {
                apartments = apartments.Where(p => p.Price >= apartmentParams.MinPrice && p.Price <= apartmentParams.MaxPrice);
            }

            if(apartmentParams.NbOfRooms != null)
            {
                apartments = apartments.Where(p => p.NbOfRooms == apartmentParams.NbOfRooms);
            }

            if(!string.IsNullOrEmpty(apartmentParams.Address))
            {
                apartmentParams.Address = apartmentParams.Address.ToLower();
                apartments = apartments.Where(p => p.Address.ToLower().Contains(apartmentParams.Address));
            }

            return await PagedList<Apartment>.CreateAsync(apartments, apartmentParams.PageNumber, apartmentParams.PageSize);
        }
        
        public async Task<Apartment> GetApartment(int id)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == id);

            return apartment;
        }

        public async Task<Apartment> UpdateApartment(int id, string title, int? nbOfRooms, string address)
        {
            var apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == id);

            if(!string.IsNullOrEmpty(title))
            {
                apartment.Title = title;
            }
            if(nbOfRooms.HasValue)
            {
                apartment.NbOfRooms = nbOfRooms.Value;
                apartment.Price = nbOfRooms.Value * 15000;
            }
            if(!string.IsNullOrEmpty(address))
            {
                apartment.Address = address;
            }

            await _context.SaveChangesAsync();
            return apartment;
        }

        public async Task<Apartment> DeleteApartment(int id)
        {
            var apartment = new Apartment();

            if(await _context.PurchasedApartments.AnyAsync(p => p.Id == id))
            {
                var apartmentToRemove = await _context.PurchasedApartments.FirstOrDefaultAsync(p => p.Id == id);
                var buyerId = apartmentToRemove.OwnerId;
                var owner = await _context.Buyers.FirstOrDefaultAsync(o => o.Id == buyerId);
                apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == apartmentToRemove.Id);
                _context.PurchasedApartments.Remove(apartmentToRemove);
                owner.Apartments.Remove(apartment);
            }
            else
            {
                apartment = await _context.Apartments.FirstOrDefaultAsync(p => p.Id == id);
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();
            return apartment;
        }
    }
}