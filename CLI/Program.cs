using System;
using System.Collections.Generic;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            WeatherService w = new WeatherService();
            List<string> locations = w.GetLocations();
            foreach (var location in locations)
            {
                try
                {
                    var lw = w.GetWeather(location);
                    Console.WriteLine(lw);
                }
                catch (Exception ex)
                {
                    // TODO: Log the error to Airbrake
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
