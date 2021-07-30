using Dynamitey;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherAPI.Controllers;
using Xunit;

namespace WeatherAPI.Test
{
    public static class Test
    {
        private const string Query = "London,UK";

        public static JsonResult Run()
        {
            dynamic json = new ApiController().Weather(Query) as dynamic;

            var local = Dynamic.InvokeGet(json.Value, "Local");
            var temp = Dynamic.InvokeGet(json.Value, "Temperatura");
            var min = Dynamic.InvokeGet(temp, "min");
            var max = Dynamic.InvokeGet(temp, "max");
            var now  = Dynamic.InvokeGet(temp, "atual");

            //Assert
            Assert.NotNull(local);
            Assert.NotNull(min);
            Assert.NotNull(max);
            Assert.NotNull(now);

            return json;
        }
    }
}
