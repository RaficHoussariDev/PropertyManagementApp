using System;
namespace PropertyManagement.Console
{
    public abstract class Property
    {
        static int NbrOfInstances = 1;
        public int Id { get; set; }

        public string Type { get; set; }
        public string Title { get; set; }

        public int Price { get; set; }
        public string Address { get; set; }

        public Property(string title, string address)
        {
            this.Id = NbrOfInstances;
            NbrOfInstances++;
            this.Title = title;
            this.Address = address;
        }

        public override string ToString()
        {
            return "Id: " + this.Id + ", Title: " + this.Title + ", Price: " + this.Price + ", Address: " + this.Address;
        }
    }
}