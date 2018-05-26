namespace Sama.Core.Domain.Shared
{
    public class Location
    {
        public string Address { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }

        protected Location()
        {
        }
        
        public Location(string address, double latitude, double longitude)
        {
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}