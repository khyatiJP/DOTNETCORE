using Microsoft.EntityFrameworkCore;
using Serilog;

using Serilog.Events;
using Theatre.Infrastructure.Data;
using Theatre.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connection = builder.Configuration.GetConnectionString("DataConnection");

var Logger=new LoggerConfiguration()
    .WriteTo.MySQL(connection,"Errolog",LogEventLevel.Error,false,100,null).CreateLogger();

builder.Host.UseSerilog(Logger);

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseMySql(connection,ServerVersion.AutoDetect(connection));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
