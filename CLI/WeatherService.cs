using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CLI
{
    public class WeatherService
    {
        public List<string> GetLocations()
        {
            List<string> locations;
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetStringAsync("https://airbrake.github.io/weatherapi/locations").Result;
                locations = JsonConvert.DeserializeObject<List<string>>(response);
                locations.AddRange(this.GetUnsupportedLocations());
            }
            return locations;
        }

        public LocationWeather GetWeather(string locationName)
        {
            var lw = new LocationWeather();
            lw.Name = locationName;

            using (HttpClient client = new HttpClient())
            {
                var resp = client.GetAsync("https://airbrake.github.io/weatherapi/weather/" + locationName);
                var result = resp.Result;
                if (result.IsSuccessStatusCode)
                {
                    var json = result.Content.ReadAsStringAsync().Result;
                    JObject jo = JObject.Parse(json);

                    lw.Temperature = (float)jo["current"]["temp"];
                    lw.Humidity = (float)jo["current"]["humidity"];
                }
                else
                {
                    throw new Exception(String.Format("Error getting weather for {0}! {1}", locationName, result.ReasonPhrase));
                }
            }
            return lw;
        }

        private List<string> GetUnsupportedLocations()
        {
            return new List<string>(){"boston", "seattle"};
        }
    }
}
