namespace PropertyManagement.API.Dtos
{
    public class ApartmentForUpdateDto
    {
        public string Title { get; set; }
        public int? NbOfRooms { get; set; }
        public int Price { get; set; }
        public string Address { get; set; }
    }
}