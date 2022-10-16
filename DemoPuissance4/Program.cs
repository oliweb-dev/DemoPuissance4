using DemoPuissance4.Hubs;
using DemoPuissance4.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GameService>();
builder.Services.AddSignalR();

builder.Services.AddCors(cb => cb.AddDefaultPolicy(o =>
{
    o.AllowAnyMethod();
    o.AllowAnyHeader();
    o.WithOrigins("http://localhost:4200");
    o.AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapHub<GameHub>("/hubs/game");

app.UseAuthorization();

app.MapControllers();

app.Run();
