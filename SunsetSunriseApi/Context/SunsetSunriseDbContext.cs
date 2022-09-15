using Microsoft.EntityFrameworkCore;
using SunsetSunriseApi.Data;
using WeatherApi.Models;

namespace WeatherApi.Context {
    public class SunsetSunriseDbContext : DbContext {
        public SunsetSunriseDbContext(DbContextOptions<SunsetSunriseDbContext> options) : base(options) {
        }
        public DbSet<SunsetSunriseDbModel> SunsetSunrises { get; set; }
        public DbSet<TimeZonesMap> TimeZones { get; set; }
        public DbSet<TimeAdjustment> TimeAdjustments { get; set; }
        public DbSet<CurrentTimeZone> CurrentTimeZones { get; set; }
    }
}
