using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using WeatherAPI.Models;

namespace WeatherAPI.Controllers
{
    public class ApiController : Controller
    {
        
        [HttpGet] // GET api/weather
        public IActionResult Weather([FromQuery(Name = "q")] string q)
        {
            // Check if data is already stored in cache
            var cached = Data.ApplicationDbContext.RetrieveData(q);

            if (cached != null)
                return Json(new
                {
                    Local = cached.Local.Replace(",", ", "),
                    Temperatura = new
                    {
                        min = cached.Temp.Min,
                        max = cached.Temp.Max,
                        atual = cached.Temp.Now
                    }
                });
            
            // Retrieve data from external api if not found in cache
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string url = $"https://api.openweathermap.org/data/2.5/weather?q={q}&units=metric&appid=6aecf6cdad2a7af8eb7b7388b2e33c33";
            dynamic json;

            // Handle api error's
            try
            {
                json = JsonConvert.DeserializeObject((new WebClient()).DownloadString(url));
            }
            catch (Exception ex)
            {
                return Json(new { Query = q, Error = ex.Message });
            }

            // Store info in database
            Weather weather = new Weather(q, new double[] { json.main.temp_min.Value, json.main.temp_max.Value, json.main.temp.Value });

            return Json(new { 
                Local = weather.Local.Replace(",", ", "),
                Temperatura = new { 
                    min = weather.Temp.Min, 
                    max = weather.Temp.Max, 
                    atual = weather.Temp.Now 
                } 
            });
        }

        [HttpGet] // GET api/test
        public IActionResult Test()
        {
            return WeatherAPI.Test.Test.Run();
        }
    }
}
