using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Daily_Weather_Rest_Api;

namespace Daily_Weather_Rest_Api
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await IdojarasNapi();

            Console.WriteLine("Program vége...");
            Console.ReadKey();
        }

        private static async Task IdojarasNapi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&current=temperature_2m,wind_speed_10m&hourly=temperature_2m,relative_humidity_2m,wind_speed_10m");
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var dailyWeather = DailyWeather.FromJson(jsonString);

                        //Adatok kiírása
                        Console.WriteLine($"Időzóna: {dailyWeather.Timezone}");
                        Console.WriteLine($"Szélsebesség: {dailyWeather.Current.WindSpeed10M} km/s");
                        Console.WriteLine($"Hőmérséklet: {dailyWeather.Current.Temperature2M} °C");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
