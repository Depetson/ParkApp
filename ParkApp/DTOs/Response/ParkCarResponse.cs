namespace ParkApp.DTOs.Response;

public class ParkCarResponse
{
    public required string VehicleReg { get; set; }
    public required int SpaceNumber { get; set; }
    public required DateTime TimeIn { get; set; }
}
