namespace WeatherApi.Models {
    public class SunsetSunriseResult {
        public SunsetSunrise SunsetSunrise { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }

        public SunsetSunriseResult(SunsetSunrise? sunsetSunrise, bool error, string? errorMessage) {
            SunsetSunrise = sunsetSunrise;
            Error = error;
            ErrorMessage = errorMessage;
        }
        public SunsetSunriseResult() { }
    }
}
