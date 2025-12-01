using Microsoft.EntityFrameworkCore;
using ParkApp.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ParkApp.Data.Models;

[Index(nameof(SpaceNumber), nameof(VehicleReg), IsUnique = true)]
public class ParkingSpace
{
    [Key]
    public int Id { get; set; }
    public int SpaceNumber { get; set; }
    public string? VehicleReg { get; set; }
    public DateTime? TimeIn { get; set; }
    public VehicleType? VehicleType { get; set; }
}
