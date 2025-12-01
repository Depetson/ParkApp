using ParkApp.Common.Enums;
using ParkApp.Utils;

namespace ParkApp.BusinessLogic;

public static class ParkCharger
{
    public static double ChargeCar(VehicleType vType, DateTime timeIn, DateTime timeOut)
    {
        double charge = 0;
        switch (vType)
        {
            case VehicleType.SmallCar:
                charge = 0.10;
                break;
            case VehicleType.MediumCar:
                charge = 0.20;
                break;
            case VehicleType.LargeCar:
                charge = 0.40;
                break;
        }

        var totalMinutes = Math.Ceiling((timeOut.Truncate(TimeSpan.TicksPerSecond) - timeIn.Truncate(TimeSpan.TicksPerSecond)).TotalMinutes);
        return charge * totalMinutes;
    }
}
