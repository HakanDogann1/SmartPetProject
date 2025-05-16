using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateRoomAsync(string appointmentId);
        Task<Room> GetRoomByAppointmentAsync(string appointmentId);
    }
}
