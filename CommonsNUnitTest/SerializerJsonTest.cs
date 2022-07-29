using System;
using System.Data;
using System.Collections.Generic;
using Commons.FileReader;
using Commons.FileReader.Model;
using NUnit.Framework;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CommonsNUnitTest
{
    public class SerializerJsonTest
    {
        [Test]
        public void SerializeTest()
        {
            SerializerJson sj = new SerializerJson("weatherForecast.json");
            
            var weatherForecast = new WeatherForecast
            {
                Date = DateTime.Parse("2019-08-01"),
                TemperatureCelsius = 25,
                Summary = "Hot",
                SummaryField = "Hot",
                DatesAvailable = new List<DateTimeOffset>()
                    { DateTime.Parse("2019-08-01"), DateTime.Parse("2019-08-02") },
                TemperatureRanges = new Dictionary<string, HighLowTemps>
                {
                    ["Cold"] = new HighLowTemps { High = 20, Low = -10 },
                    ["Hot"] = new HighLowTemps { High = 60, Low = 20 }
                },
                SummaryWords = new[] { "Cool", "Windy", "Humid" }
            };
            
            List<WeatherForecast> wf = new List<WeatherForecast>();
            wf.Add(weatherForecast);
            wf.Add(weatherForecast);

            sj.Serialize(wf);
        }

        [Test]
        public void DeserializeToObjextTest()
        {
            SerializerJson sj = new SerializerJson("appsettings.json");
            JObject jo = sj.DeserializeToObjext();
            var t = jo["Data"]["PlayConnectionString"].ToString();
        }
    }
}