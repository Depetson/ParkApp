using ParkApp.Common.Enums;
using ParkApp.Data;
using ParkApp.Data.Models;

namespace ParkApp.Utils;

public class TestDataSeeder
{
    private readonly ParkAppDbContext _context;

    public TestDataSeeder(ParkAppDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated();
    }

    public async Task Seed(int emptySpaces, int occupiedSpaces)
    {
        if (!_context.ParkingSpaces.Any())
        {
            List<ParkingSpace> spaces = new List<ParkingSpace>();
            int spaceNumber = 0;
            var rnd = new Random();
            for (int i = 0; i < occupiedSpaces; i++)
            {
                spaces.Add(new ParkingSpace()
                {
                    SpaceNumber = spaceNumber++,
                    VehicleReg = Guid.NewGuid().ToString(),
                    TimeIn = RandomDate(rnd),
                    VehicleType = (VehicleType)rnd.Next(1, 4)
                });
            }

            for (int i = 0; i < emptySpaces; i++)
            {
                spaces.Add(new ParkingSpace()
                {
                    SpaceNumber = spaceNumber++,
                    VehicleReg = null,
                    TimeIn = null,
                    VehicleType = null
                });
            }

            await _context.AddRangeAsync(spaces);
            await _context.SaveChangesAsync();
        }
    }

    private DateTime RandomDate(Random rnd)
    {
        DateTime start = DateTime.Now.AddDays(-1);
        int range = (DateTime.Now.AddDays(1) - start).Days;
        return start.AddDays(rnd.Next(range));
    }
}
