using Platform.Services;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDistributedSqlServerCache(opts => {
//    opts.ConnectionString
//        = builder.Configuration["ConnectionStrings:CacheConnection"];
//    opts.SchemaName = "dbo";
//    opts.TableName = "DataCache";
//});

builder.Services.AddOutputCache(opts => {
    opts.AddBasePolicy(policy => {
        policy.Cache();
        policy.Expire(TimeSpan.FromSeconds(10));
    });
    opts.AddPolicy("30sec", policy => {
        policy.Cache(); 
        policy.Expire(TimeSpan.FromSeconds(30));
    });
});

//builder.Services.AddResponseCaching();
builder.Services.AddSingleton<IResponseFormatter,
    HtmlResponseFormatter>();

var app = builder.Build();

//app.UseResponseCaching();
app.UseOutputCache();

app.MapEndpoint<Platform
    .SumEndpoint>("/sum/{count:int=1000000000}")
    .CacheOutput();

app.MapEndpoint<Platform
    .SumEndpoint>("/sum30/{count:int=1000000000}")
    .CacheOutput("30sec");

app.MapGet("/", async context => {
    await context.Response.WriteAsync("Hello World!");
});

app.Run();
