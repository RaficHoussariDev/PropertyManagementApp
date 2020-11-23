using System;
namespace PropertyManagement.Console
{
    public class Shop : Property
    {
        public int Area { get; set; }
        public string[] BusinessOpportunities = new string[] { "Food", "Repair", "Retail" };
        public string Business { get; set; }

        public Shop(string title, int area, string address, string business)
            :base(title, address)
        {
            if(canSetBusiness(business)) 
            {
                this.Type = "shop";
                this.Area = area;
                this.Business = business;
                if(this.Area > 50)
                {
                    this.Price = 120000;
                }
                else
                {
                    this.Price = 80000;
                }
            }
            else 
            {
                throw new Exception("Cannot create this shop because its business is invalid");
            }
        }
        private bool canSetBusiness(string business)
        {
            foreach (var element in BusinessOpportunities)
            {
                if(business == element) 
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return "A shop with an " + base.ToString() + ", area length: " + this.Area + ", business: " + this.Business;
        }
    }
}