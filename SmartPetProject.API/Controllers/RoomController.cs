using Microsoft.AspNetCore.Mvc;
using SmartPetProject.BusinessLayer.Interfaces;

namespace SmartPetProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        // POST: api/room/create/{appointmentId}
        [HttpPost("create/{appointmentId}")]
        public async Task<IActionResult> CreateRoom(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                return BadRequest("AppointmentId is required.");

            try
            {
                var room = await _roomService.CreateRoomAsync(appointmentId);
                return Ok(room);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/room/{appointmentId}
        [HttpGet("{appointmentId}")]
        public async Task<IActionResult> GetRoomByAppointment(string appointmentId)
        {
            if (string.IsNullOrEmpty(appointmentId))
                return BadRequest("AppointmentId is required.");

            var room = await _roomService.GetRoomByAppointmentAsync(appointmentId);
            if (room == null)
                return NotFound("Room not found for this appointment.");

            return Ok(room);
        }
    }
}
