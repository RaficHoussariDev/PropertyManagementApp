using System.Collections.Generic;
namespace PropertyManagement.Console
{
    using System;
    class Program
    {
        public static void Main(string[] args)
        {
            List<Property> totalProperties = new List<Property>();
            List<Buyer> totalBuyers = new List<Buyer>();

            //Create 5 apartments
            Property p1 = new Apartment("Apartment with 1 room", 1, "Beirut" );
            totalProperties.Add(p1);
            Property p2 = new Apartment("Apartment with 2 rooms", 2, "Jbeil" );
            totalProperties.Add(p2);
            Property p3 = new Apartment("Appartment with 3 rooms", 3, "Ashrafieh" );
            totalProperties.Add(p3);
            Property p4 = new Apartment("Appartment with 4 rooms", 4, "Tripoli" );
            totalProperties.Add(p4);
            Property p5 = new Apartment("Appartment with 5 rooms", 5, "Jounieh" );
            totalProperties.Add(p5);

            //Create 2 lands
            Property l1 = new Land("Land 1", 250, "Chouf", true);
            totalProperties.Add(l1);
            Property l2 = new Land("Land 2", 300, "Aley", false);
            totalProperties.Add(l2);

            //Create 3 shops
            Property s1 = new Shop("Coffee Shop", 60, "Beirut", "Food");
            totalProperties.Add(s1);
            Property s2 = new Shop("Repair Shop", 50, "Saida", "Repair");
            totalProperties.Add(s2);
            Property s3 = new Shop("Retail Shop", 20, "Tripoli", "Retail");
            totalProperties.Add(s3);
            
            //Create 3 buyers
            Buyer Rafic = new Buyer("Rafic Houssari", 60000);
            totalBuyers.Add(Rafic);
            Buyer Jimmy = new Buyer("Jimmy Yammine", 10000);
            totalBuyers.Add(Jimmy);
            Buyer Marc = new Buyer("Marc Daou", 400000);
            totalBuyers.Add(Marc);

            //Display all properties in the console
            Console.WriteLine("All the properties: ");
            foreach (var property in totalProperties)
            {
                Console.WriteLine(property);
            }

            //Display all lands in the console
            Console.WriteLine("==========================");
            Console.WriteLine("All the lands: ");
            foreach (var land in totalProperties)
            {
                if(land.Type == "land")
                {
                    Console.WriteLine(land);
                }
            }

            //Display all the properties whose prices are between 45000 and 100000
            Console.WriteLine("==========================");
            Console.WriteLine("All properties whose price is between 45000 and 100000");
            foreach (var property in totalProperties)
            {
                if(property.Price >= 45000 && property.Price <= 100000)
                {
                    Console.WriteLine(property);
                }
            }

            //Simulate the purchase of the properties by the buyer, I consider that a buyer cannot buy an already buyed property
            Console.WriteLine("==========================");
            Console.WriteLine("Simulate the buys");
            Rafic.Buy(p1);
            Rafic.Buy(p2);
            Rafic.Buy(s2);
            Jimmy.Buy(p3);
            Jimmy.Buy(s3);
            Marc.Buy(p4);
            Marc.Buy(s1);
            Marc.Buy(p5);

            //Display each buyer in the console
            Console.WriteLine("==========================");
            Console.WriteLine("Display the buyers");
            foreach (var buyer in totalBuyers)
            {
                Console.WriteLine(buyer);
            }

            //Update the title of the property whose ID is 2
            Console.WriteLine("==========================");
            Console.WriteLine("Update the property with the ID 2");
            foreach (var property in totalProperties)
            {
                if(property.Id == 2)
                {
                    property.Title = "New Property Title";
                    Console.WriteLine("The new property's title: " + property.Title);
                }
            }

            foreach (var property in totalProperties)
            {
                if(property.Id == 2)
                {
                    Console.WriteLine("Displaying updated property: " + property);
                }
            }

            //Remove 2 unpurchased properties
            Console.WriteLine("==========================");
            Console.WriteLine("Removing 2 unpurchased properties randomly:");
            List<Property> purchasedProperties = new List<Property>();
            foreach (var buyer in totalBuyers)
            {
                foreach (var property in buyer.OwnedProperties)
                {
                    purchasedProperties.Add(property);
                }
            }

            int i = 0;
            while(true)
            {
                if(i == 2) { break; }
                Random rnd = new Random();
                int RandomId = rnd.Next(1, totalProperties.Count - 1);
                bool t = false;
                foreach (var property in purchasedProperties)
                {
                    if(property.Id == RandomId)
                    {
                        t = true;
                        break;
                    }
                }
                if(!t)
                {
                    for(int j = 0; j < totalProperties.Count; j++)
                    {
                        if(totalProperties[j].Id == RandomId)
                        {
                            Console.WriteLine("This property will removed randomly: " + totalProperties[j]);
                            totalProperties.Remove(totalProperties[j]);
                            i++;
                        }
                    }
                }
            }

            //Remove 2 unpurchased properties
            Console.WriteLine("==========================");
            Console.WriteLine("Displaying all the remaining properties:");
            foreach (var property in totalProperties)
            {
                Console.WriteLine(property);
            }
        }
    }
}
