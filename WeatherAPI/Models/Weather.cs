using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Data;

namespace WeatherAPI.Models
{
    public class Weather
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }

        public string Local { get; set; }
        public Temperature Temp = new Temperature();

        public Weather() { }

        public Weather(string local, double[] temp)
        {
            Local = local;
            Temp.Min = temp[0];
            Temp.Max = temp[1];
            Temp.Now = temp[2];

            // store request in database for 20 minutes
            ApplicationDbContext.StoreData(this);

        }
    }

    public class Temperature
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Now { get; set; }
    }
}