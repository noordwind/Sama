using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;

namespace Sama.Infrastructure.Maps
{
    public class LocationProvider : ILocationProvider
    {
        private readonly HttpClient _client;
        private readonly LocationOptions _options;
        
        public LocationProvider(LocationOptions options)
        {
            _options = options;
            _client = new HttpClient
            {
                BaseAddress = new Uri(_options.ApiUrl)
            };
        }

        public async Task<Location> GetAsync(string address)
            => Map(await RetryGetAsync(address, 0, 0));

        public async Task<Location> GetAsync(double latitude, double longitude)
            => Map(await RetryGetAsync(string.Empty, latitude, longitude));
        
        private async Task<LocationResponse> RetryGetAsync(string address, double latitude, double longitude)
        {
            const int retries = 3;
            const int retriesDelay = 1000;
            var retryPolicy = Policy
                .HandleResult<LocationResponse>(r => r.Results == null || !r.Results.Any())
                .WaitAndRetryAsync<LocationResponse>(retries, 
                    retryAttempt => TimeSpan.FromMilliseconds(retriesDelay));

            return await retryPolicy.ExecuteAsync(async () => await GetAsync(address, latitude, longitude));
        }
        
        private async Task<LocationResponse> GetAsync(string address, double latitude, double longitude)
        {
            var retryPolicy = Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .WaitAndRetryAsync<HttpResponseMessage>(2, 
                    retryAttempt => TimeSpan.FromMilliseconds(500));
            var response = await retryPolicy.ExecuteAsync(async () => 
            {
                var query = string.IsNullOrWhiteSpace(address) ? $"latlng={latitude},{longitude}" : $"address={address}";
                var queryWithKey = string.IsNullOrWhiteSpace(_options.ApiKey) ? query : $"{query}&key={_options.ApiKey}";

                return await _client.GetAsync($"?{queryWithKey}");
            });
            var content = await response.Content.ReadAsStringAsync();
            if(!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<LocationResponse>(content));
        }

        private Location Map(LocationResponse response)
        {
            if (response?.Results == null || !response.Results.Any())
            {
                return null;
            }

            var result = response.Results.FirstOrDefault();
            if (result == null)
            {
                return null;
            }

            return new Location
            {
                Address = result.FormattedAddress,
                Latitude = result.Geometry.Location.Lat,
                Longitude = result.Geometry.Location.Lng
            };
        }


        private class LocationResponse
        {
            public IEnumerable<LocationResult> Results { get; set; }
        }

        private class LocationResult
        {
            [JsonProperty(PropertyName = "address_components")]
            public IEnumerable<AddressComponent> AddressComponents { get; set; }
            
            [JsonProperty(PropertyName = "formatted_address")]
            public string FormattedAddress { get; set; }
            
            public Geometry Geometry { get; set; }
        }

        private class AddressComponent
        {
            [JsonProperty(PropertyName = "long_name")]
            public string LongName { get; set; }

            [JsonProperty(PropertyName = "short_name")]
            public string ShortName { get; set; }

            public IEnumerable<string> Types { get; set; }
        }

        private class Geometry
        {
            public Coordinates Location { get; set; }
            
            public class Coordinates
            {
                public double Lat { get; set; }
                public double Lng { get; set; }
            }
        }
    }
}