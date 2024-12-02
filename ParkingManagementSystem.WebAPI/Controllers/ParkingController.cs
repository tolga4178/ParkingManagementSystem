using Microsoft.AspNetCore.Mvc;
using ParkingManagementSystem.Application.Services;


namespace ParkingManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;

        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }

        /// <summary>
        /// Bir araç için uygun bir park yeri bulur ve kaydeder.
        /// </summary>
        [HttpPost("park")]
        public async Task<IActionResult> ParkVehicle([FromBody] string vehicleSize)
        {
            if (string.IsNullOrEmpty(vehicleSize))
                return BadRequest("Vehicle size is required.");

            var result = await _parkingService.ParkVehicleAsync(vehicleSize);

            if (result == null)
                return NotFound("No suitable parking spot found.");

            return Ok(result);
        }

        /// <summary>
        /// Araç çıkışı gerçekleştirir ve park ücretini hesaplar.
        /// </summary>
        [HttpPost("exit/{parkingRecordId:guid}")]
        public async Task<IActionResult> ExitVehicle(Guid parkingRecordId)
        {
            try
            {
                var result = await _parkingService.ExitVehicleAsync(parkingRecordId);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Parking record not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
