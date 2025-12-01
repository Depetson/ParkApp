using ParkApp.Common.Enums;
using ParkApp.DTOs.Response;

namespace ParkApp.BusinessLogic.Interfaces;

public interface IParkHandler
{
    public Task<ParkingSpaceResponse> GetParkingSpace();
    public Task<ParkCarResponse?> ParkCar(string VehicleRig, VehicleType VehicleType);
    public Task<CarExitResponse?> ExitCar(string VehicleRig);
}
