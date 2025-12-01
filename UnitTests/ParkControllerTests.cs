using Microsoft.AspNetCore.Mvc;
using Moq;
using ParkApp.BusinessLogic.Interfaces;
using ParkApp.Common.Enums;
using ParkApp.Controllers;
using ParkApp.DTOs.Response;

namespace UnitTests;
public class ParkControllerTests
{
    private readonly Mock<IParkHandler> _parkHandlerMock;
    private readonly ParkController _controller;

    public ParkControllerTests()
    {
        _parkHandlerMock = new Mock<IParkHandler>();
        _controller = new ParkController(_parkHandlerMock.Object);
    }

    [Fact]
    public async Task CarExit_Should_Return_404()
    {
        //Arrange
        _parkHandlerMock.Setup(x => x.ExitCar(It.IsAny<string>())).ReturnsAsync((CarExitResponse?)null);

        //Act
        var result = await _controller.CarExit("1234");
        var objResult = result.Result as ObjectResult;
        //Assert
        Assert.NotNull(result);
        Assert.Equal(objResult!.StatusCode, 404);
    }

    [Fact]
    public async Task ParkCar_Should_Return_404()
    {
        //Arrange
        _parkHandlerMock.Setup(x => x.ParkCar(It.IsAny<string>(), It.IsAny<VehicleType>())).ReturnsAsync((ParkCarResponse?)null);

        //Act
        var result = await _controller.ParkCar(new ParkApp.DTOs.Request.ParkCarRequest() { VehicleReg = "TestReg", VehicleType = VehicleType.LargeCar });
        var objResult = result.Result as ObjectResult;
        //Assert
        Assert.NotNull(result);
        Assert.Equal(objResult!.StatusCode, 406);
    }
}
