using Newtonsoft.Json;

namespace Sama.Services.Shared.Commands.Models
{
    public class Location
    {
        public string Address { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public Location(string address) : this(address, 0, 0)
        {
        }
        
        public Location(double latitude, double longitude) : this(string.Empty, latitude, longitude)
        {
        }
        
        [JsonConstructor]
        private Location(string address, double latitude, double longitude)
        {
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}