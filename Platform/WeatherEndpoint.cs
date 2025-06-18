using Platform.Services;

namespace Platform {
    public class WeatherEndpoint {
        
        public static async Task Endpoint(HttpContext context) {
            await TypeBroker.Formatter.Format(context, 
                "Endpoint Class: It is cloudy in Milan");
        }
    }
}

