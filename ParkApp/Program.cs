using Microsoft.EntityFrameworkCore;
using ParkApp.BusinessLogic;
using ParkApp.BusinessLogic.Interfaces;
using ParkApp.Data;
using ParkApp.Data.Interfaces;
using ParkApp.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ParkAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddTransient<TestDataSeeder>();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IParkHandler), typeof(ParkHandler));


builder.Services.AddControllers();

builder.Services.AddExceptionHandler<ParkAppExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seedOptions = builder.Configuration.GetSection("BbSeedOptions").Get<DbSeedOptions>();
    if (seedOptions != null)
    {
        if (seedOptions.SeedDB)
        {
            var seeder = scope.ServiceProvider.GetService<TestDataSeeder>();
            if (seeder != null)
                await seeder.Seed(seedOptions.EmptySpaces, seedOptions.OccupiedSpaces);
        }
    }
}

app.Run();
