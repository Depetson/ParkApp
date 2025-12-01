using ParkApp.Common.Enums;
using ParkApp.Utils;
using System.ComponentModel.DataAnnotations;

namespace ParkApp.DTOs.Request;

public class ParkCarRequest
{
    [Required(AllowEmptyStrings = false)]
    public required string VehicleReg { get; set; }

    [RequiredEnum]
    public VehicleType VehicleType { get; set; }
}