using System.Collections.Generic;

namespace PropertyManagement.API.Models
{
    public class Buyer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Credit { get; set; }
        public List<Apartment> Apartments = new List<Apartment>();
    }
}