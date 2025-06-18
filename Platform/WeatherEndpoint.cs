using Platform.Services;

namespace Platform
{
    public class WeatherEndpoint
    {

        public static async Task Endpoint(HttpContext context)
        {
            await TextResponseFormatter.Singleton.Format(context,
                "Endpoint Class: It is cloudy in Milan");
        }
    }
}

