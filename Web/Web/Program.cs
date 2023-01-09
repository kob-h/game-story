using StackExchange.Redis;
using Web.BusinessService;
using Web.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITicketReservations, TicketReservations>();
builder.Services.AddTransient<ITicketReservationRepository, TicketReservationRepository>();

builder.Services.AddScoped<IDatabase>(cfg =>
{
    IConnectionMultiplexer multiplexer = ConnectionMultiplexer.Connect("redis");
    return multiplexer.GetDatabase();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
