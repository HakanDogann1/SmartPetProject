using SmartPetProject.DtoLayer.Dtos.AppointmentDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(string id);
        Task AddAppointmentAsync(Appointment appointment);
        Task<bool> IsAppointmentSlotAvailableAsync(DateTime date, TimeSpan time, string vetId);
        Task<List<UpcomingAppointmentDto>> GetUpcomingAppointmentsForVeterinarianAsync(string userId);
        Task<List<UpcomingAppointmentDto>> GetUpcomingAppointmentsForOwnerAsync(string userId);
        Task<List<UpcomingAppointmentDto>> GetAllAppointmentsForVeterinarianAsync(string userId);
        Task UpdateVeterinarianAppointmentAsync(VeterinarianUpdateAppointmentDto veterinarianUpdateAppointmentDto);
        Task CheckAndUpdateAppointmentStatusesAsync();
    }
}
