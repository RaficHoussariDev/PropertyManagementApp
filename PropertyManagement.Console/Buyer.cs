using System.Collections.Generic;

namespace PropertyManagement.Console
{
    public class Buyer
    {
        static int NbrOfBuyers = 1;
        public int BuyerId { get; set; }
        public string FullName { get; set; }
        public int Credit { get; set; }
        public List<Property> OwnedProperties;

        public Buyer(string fullname, int credit)
        {
            BuyerId = NbrOfBuyers;
            NbrOfBuyers++;
            this.FullName = fullname;
            this.Credit = credit;
            this.OwnedProperties = new List<Property>();
        }

        public void Buy(Property property)
        {
            if(property.Price <= this.Credit)
            {
                this.Credit -= property.Price;
                this.OwnedProperties.Add(property);
                System.Console.WriteLine(property.Type + " with Id " + property.Id + " was purchased by " + this.FullName + " for " + property.Price);
            }
            else
            {
                System.Console.WriteLine("Buyer with ID " + this.BuyerId + " cannot buy this property because he does not have the required price in his credit");
            }
        }

        public override string ToString()
        {
            return "Buyer: " + this.FullName + "\n" + "Nb of Owned Properties: " + this.OwnedProperties.Count + "\n" + "Remaining Credit: " + this.Credit + "\n";
        }
    }
}