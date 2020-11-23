using System.Threading.Tasks;
using PropertyManagement.API.Helpers;
using PropertyManagement.API.Models;

namespace PropertyManagement.API.Data
{
    public interface IApartmentRepository
    {
        Task<Apartment> CreateApartment(Apartment apartment, string title, int nbOfRooms, string address);
        Task<PagedList<Apartment>> GetApartments(ApartmentParams apartmentParams);
        Task<Apartment> GetApartment(int id);

        Task<Apartment> UpdateApartment(int id, string title, int? nbOfRooms, string address);
        Task<Apartment> DeleteApartment(int id);
    }
}