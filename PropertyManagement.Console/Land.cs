namespace PropertyManagement.Console
{
    public class Land : Property
    {
        public int Area { get; set; }
        public bool CanBeFarmed { get; set; }

        public Land(string title, int area, string address, bool canBeFarmed)
            :base(title, address)
        {
            this.Type = "land";
            this.Area = area;
            this.CanBeFarmed = canBeFarmed;
            this.Price = this.Area * 3000;
        }

        public override string ToString()
        {
            string farmed = "";
            if(CanBeFarmed)
            {
                farmed += "Yes";
            }
            else
            {
                farmed += "No";
            }
            return "A land with an " + base.ToString() + ", area length: " + this.Area + ", can it be farmed? " + farmed;
        }
    }
}