using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Commons.FileReader.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class WeatherForecast
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }
        
        [JsonProperty("TemperatureCelsius")]
        public int TemperatureCelsius { get; set; }
        
        [JsonProperty("Summary")]
        public string? Summary { get; set; }
        
        [JsonProperty("SummaryField")]
        public string? SummaryField;
        
        [JsonProperty("DatesAvailable")]
        public IList<DateTimeOffset>? DatesAvailable { get; set; }
        
        [JsonProperty("TemperatureRanges")]
        public Dictionary<string, HighLowTemps>? TemperatureRanges { get; set; }
        
        [JsonProperty("SummaryWords")]
        public string[]? SummaryWords { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HighLowTemps
    {
        [JsonProperty("High")]
        public int High { get; set; }

        [JsonProperty("Low")]
        public int Low { get; set; }
    }
}
