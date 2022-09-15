namespace WeatherApi.Models {
    public class SunsetSunriseGetModel {
        public List<SunsetSunrise> SunsetSunrise { get; set; }
        public int count { get; set; }
        public double LatitudeInput { get; set; }
        public double LongitudeInput { get; set; }
        public bool Found { get; set; }
        public bool Error { get; set; }
    }
}
