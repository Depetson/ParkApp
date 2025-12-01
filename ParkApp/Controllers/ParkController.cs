using Microsoft.AspNetCore.Mvc;
using ParkApp.BusinessLogic.Interfaces;
using ParkApp.DTOs.Request;
using ParkApp.DTOs.Response;
using System.Net;

namespace ParkApp.Controllers;
[ApiController]
public class ParkController : ControllerBase
{
    private readonly IParkHandler _parkHandler;

    public ParkController(IParkHandler parkHandler)
    {
        _parkHandler = parkHandler;
    }

    /// <summary>
    /// Gets available and occupied number of spaces
    /// </summary>    
    /// <response code="200">Returns count of available and occupied number of spaces</response>
    /// <response code="500">Internal error</response>
    [HttpGet]
    [Route("parking")]
    [ProducesResponseType(typeof(ParkingSpaceResponse), 200)]
    [ProducesResponseType(500)]

    public async Task<ActionResult<ParkingSpaceResponse>> GetParkingSpace()
    {
        var parkingSpaceResponse = await _parkHandler.GetParkingSpace();
        return parkingSpaceResponse;
    }

    /// <summary>
    /// Parks a given vehicle in the first available space
    /// </summary>
    /// <param name="request">Request with vehicle reg and type</param>
    /// <response code="200">Vehicle and its space number</response>
    /// <response code="406">No free spaces</response>
    /// <response code="500">Internal error</response>
    [HttpPost]
    [Route("parking")]
    [ProducesResponseType(typeof(ParkCarResponse), 200)]
    [ProducesResponseType(406)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ParkCarResponse>> ParkCar(ParkCarRequest request)
    {
        var parkCarResponse = await _parkHandler.ParkCar(request.VehicleReg, request.VehicleType);
        if (parkCarResponse == null)
        {
            return StatusCode((int)HttpStatusCode.NotAcceptable, "No free spaces");
        };
        return Ok(parkCarResponse);
    }

    /// <summary>
    /// Free up given vehicles space
    /// </summary>
    /// <param name="VehicleReg">Vehicle reg</param>
    /// /// <response code="200">Details of final charge</response>
    /// <response code="404">No vehicle found</response>
    /// <response code="500">Internal error</response>
    [HttpPost]
    [Route("parking/exit")]
    [ProducesResponseType(typeof(CarExitResponse), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<CarExitResponse>> CarExit([FromBody] string VehicleReg)
    {
        var carExitResponse = await _parkHandler.ExitCar(VehicleReg);
        if (carExitResponse == null)
        {
            return NotFound($"Car with reg {VehicleReg} not found");
        };
        return Ok(carExitResponse);
    }
}
