using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Appointment> _appointmentRepository;

        public RoomService(IRepository<Room> roomRepository, IRepository<Appointment> appointmentRepository)
        {
            _roomRepository = roomRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<Room> CreateRoomAsync(string appointmentId)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
                throw new ArgumentException("Appointment not found");

            var existingRoom = await _roomRepository.GetAllAsync();
            var roomForAppointment = await Task.FromResult(existingRoom.FirstOrDefault(r => r.AppointmentId == appointmentId));
            if (roomForAppointment != null)
                return roomForAppointment;

            var roomName = Guid.NewGuid().ToString("N");
            var roomUrl = $"https://meet.jit.si/{roomName}";

            var room = new Room
            {
                Id = Guid.NewGuid().ToString(),
                RoomName = roomName,
                RoomUrl = roomUrl,
                AppointmentId = appointmentId
            };

            await _roomRepository.AddAsync(room);
            await _roomRepository.SaveChangesAsync();

            return room;
        }

        public async Task<Room> GetRoomByAppointmentAsync(string appointmentId)
        {
            var allRooms = await _roomRepository.GetAllAsync();
            return await Task.FromResult(allRooms.FirstOrDefault(r => r.AppointmentId == appointmentId));
        }
    }
}
