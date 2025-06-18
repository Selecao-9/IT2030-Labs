using Platform;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IResponseFormatter,
    HtmlResponseFormatter>();

var app = builder.Build();

app.UseMiddleware<WeatherMiddleware>();

app.MapGet("endpoint/class", WeatherEndpoint.Endpoint);

//IResponseFormatter formatter = TypeBroker.Formatter;
app.MapGet("endpoint/function",
    async (HttpContext context, IResponseFormatter formatter) => {
        await formatter.Format(context,
            "Endpoint Function: It is sunny in LA");
    });

app.Run();
