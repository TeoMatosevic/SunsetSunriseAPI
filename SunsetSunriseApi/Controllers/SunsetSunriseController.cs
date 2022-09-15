using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunsetSunriseApi.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WeatherApi.Context;
using WeatherApi.Models;
using WeatherApi.Properties;

namespace WeatherApi.Controllers {

    /// <summary>
    /// WeatherAPI Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SunsetSunriseController : ControllerBase {

        //https://api.sunrise-sunset.org/json?lat=36.7201600&lng=-4.4203400&date=today

        private readonly SunsetSunriseDbContext _sunsetSunriseDbContext;
        private static string urlRoute = "https://api.sunrise-sunset.org/json?";

        /// <summary>
        /// WeatherAPI Controller
        /// </summary>
        public SunsetSunriseController(SunsetSunriseDbContext sunsetSunriseDbContext) {
            _sunsetSunriseDbContext = sunsetSunriseDbContext;
            ApiHelper.InitializeClient();
        }
        /// <summary>
        /// Checks for informations about today
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SunsetSunriseResult>> CheckSunsetSunrise([DefaultValue(0)][Required] double latitude, [DefaultValue(0)][Required] double longitude) {
            string date = DateTime.Today.ToShortDateString();
            if (Math.Abs(latitude) > 90 || Math.Abs(longitude) > 180) {
                return BadRequest(new SunsetSunriseResult() {
                    SunsetSunrise = null,
                    Error = true,
                    ErrorMessage = "Incorrect values given for latitude, longitude and timezone."
                });
            }
            int hours = Int32.Parse(_sunsetSunriseDbContext.CurrentTimeZones.First().zoneDif.Substring(1));
            if (_sunsetSunriseDbContext.CurrentTimeZones.First().zoneDif.ElementAt(0) == '-') hours = hours * -1;
            if (_sunsetSunriseDbContext.TimeAdjustments.Where(t => t.Id == 1).First().season.Equals("summer") && _sunsetSunriseDbContext.TimeAdjustments.Where(t => t.Id == 1).First().adjusstment.Equals("false")) {
                hours = hours - 1;
            }
            if (Utils.Utils.Get(_sunsetSunriseDbContext, latitude, longitude, date) != null) {
                return Ok(new SunsetSunriseResult() {
                    SunsetSunrise = SunsetSunrise.parse(Utils.Utils.Get(_sunsetSunriseDbContext, latitude, longitude, date)),
                    Error = false,
                    ErrorMessage = "null"
                });
            }
            string url = $"{urlRoute}lat={latitude}&lng={longitude}&date=today";
            SunsetSunriseResult result;
            using (HttpResponseMessage response = await ApiHelper.ApiCLient.GetAsync(url)) {
                if (response.IsSuccessStatusCode) {
                    SunsetSunriseJsonModel sunsetSunriseJsonModel = await response.Content.ReadFromJsonAsync<SunsetSunriseJsonModel>();
                    result = new SunsetSunriseResult() {
                        SunsetSunrise = SunsetSunrise.parse(sunsetSunriseJsonModel, date, hours),
                        Error = false,
                        ErrorMessage = "null"
                    };
                } else {
                    result = new SunsetSunriseResult() {
                        SunsetSunrise = null,
                        Error = true,
                        ErrorMessage = $"Exception: {response.ReasonPhrase}"
                    };
                }
            }
            await _sunsetSunriseDbContext.SunsetSunrises.AddAsync(SunsetSunriseDbModel.parse(result, latitude, longitude, date));
            await _sunsetSunriseDbContext.SaveChangesAsync();
            return Ok(result);
        }
        /// <summary>
        /// Checks for information about the given day
        /// </summary>
        [HttpPost("date")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SunsetSunriseResult>> CheckSunsetSunriseByDate([DefaultValue(0)][Required] double latitude, [DefaultValue(0)][Required] double longitude, [Required] string date) {
            DateTime dateTime;
            try {
                dateTime = DateTime.Parse(date);
            } catch (FormatException e) {
                return BadRequest(new SunsetSunriseResult() {
                    SunsetSunrise = null,
                    Error = true,
                    ErrorMessage = $"Exception: {e.Message}"
                });
            }
            if (Math.Abs(latitude) > 90 || Math.Abs(longitude) > 180) {
                return BadRequest(new SunsetSunriseResult() {
                    SunsetSunrise = null,
                    Error = true,
                    ErrorMessage = "Incorrect values given for latitude and longitude."
                });
            }
            int hours = Int32.Parse(_sunsetSunriseDbContext.CurrentTimeZones.First().zoneDif.Substring(1));
            if (_sunsetSunriseDbContext.CurrentTimeZones.First().zoneDif.ElementAt(0) == '-') hours = hours * -1;
            if (_sunsetSunriseDbContext.TimeAdjustments.Where(t => t.Id == 1).First().season.Equals("summer") && _sunsetSunriseDbContext.TimeAdjustments.Where(t => t.Id == 1).First().adjusstment.Equals("false")) {
                hours = hours - 1;
            }
            if (Utils.Utils.Get(_sunsetSunriseDbContext, latitude, longitude, date) != null) {
                return Ok(new SunsetSunriseResult() {
                    SunsetSunrise = SunsetSunrise.parse(Utils.Utils.Get(_sunsetSunriseDbContext, latitude, longitude, dateTime.ToShortDateString())),
                    Error = false,
                    ErrorMessage = "null"
                });
            }
            string url = $"{urlRoute}lat={latitude}&lng={longitude}&date={dateTime.ToShortDateString()}";
            SunsetSunriseResult result;
            using (HttpResponseMessage response = await ApiHelper.ApiCLient.GetAsync(url)) {
                if (response.IsSuccessStatusCode) {
                    SunsetSunriseJsonModel sunsetSunriseJsonModel = await response.Content.ReadFromJsonAsync<SunsetSunriseJsonModel>();
                    result = new SunsetSunriseResult() {
                        SunsetSunrise = SunsetSunrise.parse(sunsetSunriseJsonModel, dateTime.ToShortDateString(), hours),
                        Error = false,
                        ErrorMessage = "null"
                    };
                } else {
                    result = new SunsetSunriseResult() {
                        SunsetSunrise = null,
                        Error = true,
                        ErrorMessage = $"Exception: {response.ReasonPhrase}"
                    };
                }
            }
            await _sunsetSunriseDbContext.SunsetSunrises.AddAsync(SunsetSunriseDbModel.parse(result, latitude, longitude, dateTime.ToShortDateString()));
            await _sunsetSunriseDbContext.SaveChangesAsync();
            return Ok(result);
        }

        /// <summary>
        /// Return the information for every day that the api request was made in the past
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SunsetSunriseGetModel>> GetByLatitudeAndLongitude(double latitude, double longitude) {
            IQueryable<SunsetSunriseDbModel> list;
            if (Math.Abs(latitude) > 90 || Math.Abs(longitude) > 180) {
                return BadRequest(new SunsetSunriseGetModel() {
                    SunsetSunrise = null,
                    count = 0,
                    LatitudeInput = latitude,
                    LongitudeInput = longitude,
                    Found = false,
                    Error = true
                });
            }
            list = Utils.Utils.GetList(_sunsetSunriseDbContext, latitude, longitude);
            if (list == null) {
                return NotFound(new SunsetSunriseGetModel() {
                    SunsetSunrise = null,
                    count = 0,
                    LatitudeInput = latitude,
                    LongitudeInput = longitude,
                    Found = false,
                    Error = false
                });
            } else {
                return Ok(new SunsetSunriseGetModel() {
                    SunsetSunrise = SunsetSunrise.parseList(list.ToList()),
                    count = SunsetSunrise.parseList(list.ToList()).Count(),
                    LatitudeInput = latitude,
                    LongitudeInput = longitude,
                    Found = true,
                    Error = false
                });
            }
        }
        /// <summary>
        /// Sets values of the current season in terms of time adjustment (only summer and winter acceptable) and wheather the time adjusts in the summer or not (only true or false acceptable)
        /// </summary>
        [HttpPut("time_adjustment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TimeAdjustment>> setTimeAdjustment(string season, string doesAdjust) {
            if (!season.Equals("summer") && !season.Equals("winter")) {
                return BadRequest("Wrong input");
            }
            if (!doesAdjust.Equals("true") && !doesAdjust.Equals("false")) {
                return BadRequest("Wrong input");
            }
            TimeAdjustment timeAdjustment = new TimeAdjustment() {
                Id = 1,
                season = season,
                adjusstment = doesAdjust
            };
            _sunsetSunriseDbContext.Entry(timeAdjustment).State = EntityState.Modified;
            await _sunsetSunriseDbContext.SaveChangesAsync();
            return Ok(timeAdjustment);
        }
        /// <summary>
        /// Sets the value of the current timezone
        /// </summary>
        [HttpPut("current_timezone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CurrentTimeZone>> setCurrentTimeZone(string timeZone) {
            if (!_sunsetSunriseDbContext.TimeZones.Where(t => t.zoneName.Equals(timeZone)).Any()) {
                return BadRequest("Wrong input");
            }
            string timeDif = _sunsetSunriseDbContext.TimeZones.Where(t => t.zoneName.Equals(timeZone)).First().zoneDif;
            CurrentTimeZone currentTimeZone = new CurrentTimeZone() {
                Id = 1,
                zoneName = timeZone,
                zoneDif = timeDif
            };
            _sunsetSunriseDbContext.Entry(currentTimeZone).State = EntityState.Modified;
            await _sunsetSunriseDbContext.SaveChangesAsync();
            return Ok(currentTimeZone);
        }
        /// <summary>
        /// Gets the value of the current season and wheather the time adjusts in the summer or not
        /// </summary>
        [HttpGet("time_adjustment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TimeAdjustment> getTimeAdjustment() {
            return Ok(_sunsetSunriseDbContext.TimeAdjustments.First());
        }
        /// <summary>
        /// Gets the value of the current timezone
        /// </summary>
        [HttpGet("current_timezone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CurrentTimeZone> getCurrentTimeZone() {
            return Ok(_sunsetSunriseDbContext.CurrentTimeZones.First());
        }
        /// <summary>
        /// Gets the mapping values of timezones and their time differentials
        /// </summary>
        [HttpGet("timezones")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IQueryable<TimeZonesMap>> GetTimeZonesMap() {
            return Ok(_sunsetSunriseDbContext.TimeZones.ToList());
        }
    }
}
