var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Run(context => {
    throw new Exception("Something has gone wrong");
});

app.Run();
