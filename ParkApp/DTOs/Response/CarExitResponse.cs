using System.ComponentModel.DataAnnotations;

namespace ParkApp.DTOs.Response;

public class CarExitResponse
{
    public required string VehicleReg { get; set; }
    [DataType(DataType.Currency)]
    public required double VehicleCharge { get; set; }
    public required DateTime TimeIn { get; set; }
    public required DateTime TimeOut { get; set; }
}
