using Microsoft.EntityFrameworkCore;
using ParkApp.Data.Models;

namespace ParkApp.Data;

public class ParkAppDbContext : DbContext
{
    public ParkAppDbContext(DbContextOptions<ParkAppDbContext> options) : base(options)
    {

    }
    public DbSet<ParkingSpace> ParkingSpaces { get; set; }
}
