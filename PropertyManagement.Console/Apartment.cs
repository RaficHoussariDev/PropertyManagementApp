namespace PropertyManagement.Console
{
    public class Apartment : Property
    {
        public int NbrOfRooms { get; set; }

        public Apartment(string title, int nbrOfRooms, string address)
            : base(title, address)
        {
            this.Type = "apartment";
            this.NbrOfRooms = nbrOfRooms;
            this.Price = this.NbrOfRooms * 15000;
        }

        public override string ToString()
        {
            return "An apartment with an " + base.ToString() + ", number of rooms: " + this.NbrOfRooms;
        }
    }
}