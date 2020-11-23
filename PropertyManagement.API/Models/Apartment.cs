namespace PropertyManagement.API.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NbOfRooms { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
    }
}