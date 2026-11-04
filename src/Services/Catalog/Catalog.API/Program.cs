var builder = WebApplication.CreateBuilder(args);

// Add services to the container
// Register services in Dependency Injection
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(
        typeof(Program).Assembly);
});

// Register Marten Document Database
builder.Services.AddMarten(options =>
{
    options.Connection(
        builder.Configuration.GetConnectionString("Database")!
    );
})
.UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request pipeline
app.MapCarter();

app.Run();
