namespace WeatherApi.Models {
    public class SunsetSunrise {
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
        public string date { get; set; }

        public static SunsetSunrise parse(SunsetSunriseDbModel sunsetSunriseDbModel) {
            return new SunsetSunrise() {
                sunrise = sunsetSunriseDbModel.sunrise,
                sunset = sunsetSunriseDbModel.sunset,
                solar_noon = sunsetSunriseDbModel.solar_noon,
                day_length = sunsetSunriseDbModel.day_length,
                civil_twilight_begin = sunsetSunriseDbModel.civil_twilight_begin,
                civil_twilight_end = sunsetSunriseDbModel.civil_twilight_end,
                nautical_twilight_begin = sunsetSunriseDbModel.nautical_twilight_begin,
                nautical_twilight_end = sunsetSunriseDbModel.nautical_twilight_end,
                astronomical_twilight_begin = sunsetSunriseDbModel.astronomical_twilight_begin,
                astronomical_twilight_end = sunsetSunriseDbModel.astronomical_twilight_end,
                date = sunsetSunriseDbModel.date
            };
        }
        public static SunsetSunrise parse(SunsetSunriseJsonModel sunsetSunriseJsonModel, string date, int hours) {

            return new SunsetSunrise() {
                sunrise = DateTime.Parse(sunsetSunriseJsonModel.results.sunrise).AddHours(hours).ToShortTimeString(),
                sunset = DateTime.Parse(sunsetSunriseJsonModel.results.sunset).AddHours(hours).ToShortTimeString(),
                solar_noon = DateTime.Parse(sunsetSunriseJsonModel.results.solar_noon).AddHours(hours).ToShortTimeString(),
                day_length = sunsetSunriseJsonModel.results.day_length,
                civil_twilight_begin = DateTime.Parse(sunsetSunriseJsonModel.results.civil_twilight_begin).AddHours(hours).ToShortTimeString(),
                civil_twilight_end = DateTime.Parse(sunsetSunriseJsonModel.results.civil_twilight_end).AddHours(hours).ToShortTimeString(),
                nautical_twilight_begin = DateTime.Parse(sunsetSunriseJsonModel.results.nautical_twilight_begin).AddHours(hours).ToShortTimeString(),
                nautical_twilight_end = DateTime.Parse(sunsetSunriseJsonModel.results.nautical_twilight_end).AddHours(hours).ToShortTimeString(),
                astronomical_twilight_begin = DateTime.Parse(sunsetSunriseJsonModel.results.astronomical_twilight_begin).AddHours(hours).ToShortTimeString(),
                astronomical_twilight_end = DateTime.Parse(sunsetSunriseJsonModel.results.astronomical_twilight_end).AddHours(hours).ToShortTimeString(),
                date = date
            };
        }
        public static List<SunsetSunrise> parseList(List<SunsetSunriseDbModel> sunsetSunriseDbModels) {
            List<SunsetSunrise> result = new List<SunsetSunrise>();
            for (int i = 0; i < sunsetSunriseDbModels.Count; i++) {
                result.Add(parse(sunsetSunriseDbModels.ElementAt(i)));
            }
            return result;
        }
    }
}

