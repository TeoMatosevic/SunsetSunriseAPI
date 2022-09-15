using WeatherApi.Context;
using WeatherApi.Models;

namespace WeatherApi.Utils {
    public class Utils {
        public static SunsetSunriseDbModel Get(SunsetSunriseDbContext sunsetSunriseDbContext, double latitude, double longitude, string date) {
            int count = sunsetSunriseDbContext.SunsetSunrises.Where(ss => ss.latitude == latitude && ss.longitude == longitude && ss.date.Equals(date)).Count();
            if (count == 0) {
                return null;
            } else {
                SunsetSunriseDbModel sunsetSunriseDbModel = sunsetSunriseDbContext.SunsetSunrises.Where(ss => ss.latitude == latitude && ss.longitude == longitude && ss.date.Equals(date)).First();
                return sunsetSunriseDbModel;
            }
        }

        public static IQueryable<SunsetSunriseDbModel> GetList(SunsetSunriseDbContext sunsetSunriseDbContext, double latitude, double longitude) {
            IQueryable<SunsetSunriseDbModel> list;
            int count = sunsetSunriseDbContext.SunsetSunrises.Where(ss => ss.latitude == latitude && ss.longitude == longitude).Count();
            if (count == 0) {
                return null;
            } else {
                list = sunsetSunriseDbContext.SunsetSunrises.Where(ss => ss.latitude == latitude && ss.longitude == longitude);
                return list;
            }
        }
    }
}
