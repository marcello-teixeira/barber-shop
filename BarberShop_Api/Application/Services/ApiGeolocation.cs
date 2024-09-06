using dotenv.net;

namespace BarberShop_Api.Application.Services
{
    public class ApiGeolocation
    {
        /// <summary>
        /// Initialize two values to get geolocation reverse
        /// </summary>
        public ApiGeolocation(string latitude, string longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
            
            // Charge enviroment variables 
            DotEnv.Load();

            _apiKeyGeolocation = Environment.GetEnvironmentVariable("API_KEY_GEOLOCATION") ?? "";

            if (string.IsNullOrEmpty(_apiKeyGeolocation))
            {
                throw new InvalidOperationException(nameof(_apiKeyGeolocation));
            }
        }

        public object? Location { get; private set; }

        private readonly string _apiKeyGeolocation;
        private readonly string _latitude;
        private readonly string _longitude;

        /// <summary>
        /// Send latitude and longitude to API OpenCageData endpoint, then get the response.
        /// </summary>
        public async Task GetGeolocation()
        {
           using(HttpClient client = new())
            {
                try
                {
                    string url = $"https://api.opencagedata.com/geocode/v1/json?q={_latitude}+{_longitude}&key={_apiKeyGeolocation}";

                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    Location = await response.Content.ReadAsStringAsync();
                } 
                catch(HttpRequestException ex)
                {
                    throw new HttpRequestException(ex.Message); 
                }
            }
        }
    }
}
