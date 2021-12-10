using System;
namespace CLI
{
    public class LocationWeather
    {
        public string Name { get; set; }
        public float Temperature { get; set; }
        public float Humidity { get; set; }

        public override string ToString()
        {
            return String.Format("The weather for {0} is:\n- Temperature: {1}\n- Humidity:    {2}", Name, Temperature, Humidity);
        }
    }
}
