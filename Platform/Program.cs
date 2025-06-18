using Platform;
using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IResponseFormatter, TimeResponseFormatter>();
builder.Services.AddScoped<ITimeStamper, DefaultTimeStamper>();

var app = builder.Build();

app.UseMiddleware<WeatherMiddleware>();

app.MapEndpoint<WeatherEndpoint>("endpoint/class");

app.MapGet("endpoint/function",
    async (HttpContext context, IResponseFormatter formatter) => {
        await formatter.Format(context,
            "Endpoint Function: It is sunny in LA");
    });

app.Run();
