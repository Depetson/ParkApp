using ParkApp.BusinessLogic.Interfaces;
using ParkApp.Common.Enums;
using ParkApp.Data.Interfaces;
using ParkApp.Data.Models;
using ParkApp.DTOs.Response;
using ParkApp.Utils;
using System.Data.Common;

namespace ParkApp.BusinessLogic;

public class ParkHandler : IParkHandler
{
    private readonly IRepository<ParkingSpace> _repository;

    public ParkHandler(IRepository<ParkingSpace> repository)
    {
        _repository = repository;
    }

    public async Task<CarExitResponse?> ExitCar(string VehicleRig)
    {
        try
        {
            var releasedParkingSpace = await _repository.Get(x => x.VehicleReg == VehicleRig);
            if (releasedParkingSpace != null)
            {
                var timeOut = DateTime.Now;

                CarExitResponse toReturn = new()
                {
                    VehicleReg = releasedParkingSpace.VehicleReg!,
                    TimeIn = releasedParkingSpace.TimeIn!.Value.FormatDate(TimeSpan.TicksPerSecond),
                    TimeOut = timeOut.FormatDate(TimeSpan.TicksPerSecond),
                    VehicleCharge = ParkCharger.ChargeCar(releasedParkingSpace.VehicleType!.Value, releasedParkingSpace.TimeIn.Value, timeOut)
                };

                releasedParkingSpace.VehicleReg = string.Empty;
                releasedParkingSpace.TimeIn = null;
                _repository.Update(releasedParkingSpace);

                return toReturn;
            }

            return null;
        }
        catch (DbException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ParkingSpaceResponse> GetParkingSpace()
    {
        List<ParkingSpace> parkingSpace;

        try
        {
            parkingSpace = await _repository.GetAll();
            var totalSpace = parkingSpace.Count;
            if (totalSpace <= 0)
            {
                throw new Exception("Database not seeded!");
            }
            var availableSpace = parkingSpace.Count(x => string.IsNullOrEmpty(x.VehicleReg));
            var occupiedSpace = totalSpace - availableSpace;

            return new ParkingSpaceResponse()
            {
                AvailableSpaces = availableSpace,
                OccupiedSpaces = occupiedSpace
            };
        }
        catch (DbException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ParkCarResponse?> ParkCar(string VehicleRig, VehicleType VehicleType)
    {
        try
        {
            var freeParkingSpace = await _repository.Get(x => string.IsNullOrEmpty(x.VehicleReg));
            if (freeParkingSpace == null) { return null; }

            freeParkingSpace.TimeIn = DateTime.Now;
            freeParkingSpace.VehicleReg = VehicleRig;
            freeParkingSpace.VehicleType = VehicleType;

            _repository.Update(freeParkingSpace);

            return new ParkCarResponse()
            {
                VehicleReg = freeParkingSpace.VehicleReg,
                SpaceNumber = freeParkingSpace.SpaceNumber,
                TimeIn = freeParkingSpace.TimeIn.Value.FormatDate(TimeSpan.TicksPerSecond)
            };
        }
        catch (DbException)
        {
            throw;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
