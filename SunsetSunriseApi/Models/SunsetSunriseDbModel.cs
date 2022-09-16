namespace WeatherApi.Models {
    public class SunsetSunriseDbModel {
        public int Id { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
        public string solar_noon { get; set; }
        public string day_length { get; set; }
        public string civil_twilight_begin { get; set; }
        public string civil_twilight_end { get; set; }
        public string nautical_twilight_begin { get; set; }
        public string nautical_twilight_end { get; set; }
        public string astronomical_twilight_begin { get; set; }
        public string astronomical_twilight_end { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string date { get; set; }

        public static SunsetSunriseDbModel parse(SunsetSunriseResult result, double latitude, double longitude, string date) {
            return new SunsetSunriseDbModel() {
                Id = 0,
                sunrise = result.SunsetSunrise.sunrise,
                sunset = result.SunsetSunrise.sunset,
                solar_noon = result.SunsetSunrise.solar_noon,
                day_length = result.SunsetSunrise.day_length,
                civil_twilight_begin = result.SunsetSunrise.civil_twilight_begin,
                civil_twilight_end = result.SunsetSunrise.civil_twilight_end,
                nautical_twilight_begin = result.SunsetSunrise.nautical_twilight_begin,
                nautical_twilight_end = result.SunsetSunrise.nautical_twilight_end,
                astronomical_twilight_begin = result.SunsetSunrise.astronomical_twilight_begin,
                astronomical_twilight_end = result.SunsetSunrise.astronomical_twilight_end,
                latitude = latitude,
                longitude = longitude,
                date = date
            };
        }
    }
}
