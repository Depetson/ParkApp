using Moq;
using ParkApp.BusinessLogic;
using ParkApp.Common.Enums;
using ParkApp.Data.Interfaces;
using ParkApp.Data.Models;
using System.Linq.Expressions;

namespace UnitTests;

public class ParkHandlerTests
{
    private readonly Mock<IRepository<ParkingSpace>> _parkingSpaceRepositoryMock;
    private readonly ParkHandler _parkHandler;
    public ParkHandlerTests()
    {
        _parkingSpaceRepositoryMock = new Mock<IRepository<ParkingSpace>>();
        _parkHandler = new ParkHandler(_parkingSpaceRepositoryMock.Object);
    }

    [Fact]
    public async Task ExitCar_Should_Return_CorrectCarExitResponse()
    {
        //Arrange
        var parkingSpace = GetTestParkingSpace(1, false);
        _parkingSpaceRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<ParkingSpace, bool>>>())).ReturnsAsync(parkingSpace);

        //Act
        var result = await _parkHandler.ExitCar(parkingSpace.VehicleReg!);
        var totalMinutes = Math.Ceiling((result!.TimeOut - result.TimeIn).TotalMinutes);
        double charge = totalMinutes * GetBaseCharge(parkingSpace.VehicleType!.Value);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(result.VehicleCharge, charge);
    }

    [Fact]
    public async Task ParkCar_Should_Return_CorrectParkCarResponse()
    {
        //Arrange
        var parkingSpace = GetTestParkingSpace(1);
        _parkingSpaceRepositoryMock.Setup(x => x.Get(It.IsAny<Expression<Func<ParkingSpace, bool>>>())).ReturnsAsync(parkingSpace);

        //Act
        var result = await _parkHandler.ParkCar(parkingSpace.VehicleReg!, VehicleType.MediumCar);

        //Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetParkingSpace_Should_Return_CorrectValues()
    {
        //Arange
        List<ParkingSpace> parkingSpaces = [];
        Random rnd = new();
        for (var i = 0; i < 10; i++)
        {
            parkingSpaces.Add(GetTestParkingSpace(i, rnd.Next(2) == 1));
        }
        _parkingSpaceRepositoryMock.Setup(x => x.GetAll(It.IsAny<bool>())).ReturnsAsync(parkingSpaces);


        //Act
        var result = await _parkHandler.GetParkingSpace();

        Assert.NotNull(result);
        Assert.Equal(result.AvailableSpaces, parkingSpaces.Count(x => string.IsNullOrEmpty(x.VehicleReg)));
        Assert.Equal(result.OccupiedSpaces, parkingSpaces.Count(x => !string.IsNullOrEmpty(x.VehicleReg)));

    }


    private static ParkingSpace GetTestParkingSpace(int spaceNumber, bool free = true)
    {
        return new ParkingSpace()
        {
            VehicleReg = free ? "" : "TESTABC 00234",
            SpaceNumber = spaceNumber,
            TimeIn = free ? null : DateTime.Now.AddDays(-1),
            VehicleType = free ? null : VehicleType.SmallCar
        };
    }

    private static double GetBaseCharge(VehicleType vehicleType)
    {
        return vehicleType switch
        {
            VehicleType.SmallCar => 0.10,
            VehicleType.MediumCar => 0.20,
            VehicleType.LargeCar => 0.40,
            _ => 0.0,
        };
    }
}
