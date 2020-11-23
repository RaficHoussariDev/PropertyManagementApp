namespace PropertyManagement.API.Helpers
{
    public class ApartmentParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize: value; }
        }

        public int MinPrice { get; set; } = 0;

        public int MaxPrice { get; set; } = int.MaxValue;

        public int? NbOfRooms { get; set; }

        public string OrderBy { get; set; }

        public string Address { get; set; }
    }
}