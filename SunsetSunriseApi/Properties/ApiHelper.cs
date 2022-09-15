using System.Net.Http.Headers;

namespace WeatherApi.Properties {
    public static class ApiHelper {
        public static HttpClient ApiCLient { get; set; }

        public static void InitializeClient() {
            ApiCLient = new HttpClient();
            ApiCLient.DefaultRequestHeaders.Accept.Clear();
            ApiCLient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
