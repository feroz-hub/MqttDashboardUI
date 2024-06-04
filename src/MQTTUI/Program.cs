var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMqttService(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMqttService();
app.Run();