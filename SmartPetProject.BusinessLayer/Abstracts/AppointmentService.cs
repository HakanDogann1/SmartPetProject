using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Interfaces;
using SmartPetProject.DtoLayer.Dtos.AppointmentDtos;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.BusinessLayer.Abstracts
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IRepository<Appointment> _appointmentRepository;
        private readonly IRepository<Veterinarian> _veterinarianRepository;
        private readonly IRepository<AnimalOwner> _animalOwnerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AppointmentService(IRepository<Appointment> appointmentRepository, IRepository<Veterinarian> veterinarianRepository, IRepository<AnimalOwner> animalOwnerRepository, UserManager<ApplicationUser> userManager)
        {
            _appointmentRepository = appointmentRepository;
            _veterinarianRepository = veterinarianRepository;
            _animalOwnerRepository = animalOwnerRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(string id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task<bool> IsAppointmentSlotAvailableAsync(DateTime date, TimeSpan time, string vetId)
        {
            var allAppointments = await _appointmentRepository.GetAllAsync();

            return !allAppointments.Any(a =>
                a.VeterinarianId == vetId &&
                a.AppointmentDate.Date == date.Date &&
                a.AppointmentTime == time);
        }

        public async Task<List<UpcomingAppointmentDto>> GetUpcomingAppointmentsForVeterinarianAsync(string userId)
        {
            var vet = (await _veterinarianRepository.GetAllAsync()).FirstOrDefault(v => v.UserId == userId);
            if (vet == null) return new();
            var user = await _userManager.FindByIdAsync(vet.UserId);
            var today = DateTime.Today;
            var threeDaysLater = today.AddDays(3);

            var appointments = (await _appointmentRepository.GetAllAsync())
                .Where(a => a.VeterinarianId == vet.Id && a.AppointmentDate.Date >= today && a.AppointmentDate.Date <= threeDaysLater)
                .ToList();

            var result = new List<UpcomingAppointmentDto>();
            foreach (var appt in appointments)
            {
                var owner = await _animalOwnerRepository.GetByIdAsync(appt.AnimalOwnerId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user.Name} {user.Surname}"
                });
            }

            return result;
        }

        public async Task<List<UpcomingAppointmentDto>> GetUpcomingAppointmentsForOwnerAsync(string userId)
        {
            var owner = (await _animalOwnerRepository.GetAllAsync()).FirstOrDefault(o => o.UserId == userId);
            if (owner == null) return new();

            var today = DateTime.Today;
            var threeDaysLater = today.AddDays(3);
            var user = await _userManager.FindByIdAsync(owner.UserId);

            var appointments = (await _appointmentRepository.GetAllAsync())
                .Where(a => a.AnimalOwnerId == owner.Id && a.AppointmentDate.Date >= today && a.AppointmentDate.Date <= threeDaysLater)
                .ToList();

            var result = new List<UpcomingAppointmentDto>();
            foreach (var appt in appointments)
            {
                var vet = await _veterinarianRepository.GetByIdAsync(appt.VeterinarianId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user?.Name} {user?.Surname}",
                });
            }

            return result;
        }

        public async Task<List<UpcomingAppointmentDto>> GetAllAppointmentsForVeterinarianAsync(string userId)
        {
            var vet = (await _veterinarianRepository.GetAllAsync()).FirstOrDefault(v => v.UserId == userId);
            if (vet == null) return new();
            var user = await _userManager.FindByIdAsync(vet.UserId);

            var appointments = (await _appointmentRepository.GetAllAsync())
                .Where(a => a.VeterinarianId == vet.Id)
                .ToList();

            var result = new List<UpcomingAppointmentDto>();
            foreach (var appt in appointments)
            {
                var owner = await _animalOwnerRepository.GetByIdAsync(appt.AnimalOwnerId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user.Name} {user.Surname}"
                });
            }

            return result;
        }

        public async Task<List<UpcomingAppointmentDto>> GetAllAppointmentsForOwnerAsync(string userId)
        {
            var owner = (await _animalOwnerRepository.GetAllAsync()).FirstOrDefault(o => o.UserId == userId);
            if (owner == null) return new();
            var user = await _userManager.FindByIdAsync(owner.UserId);

            var appointments = (await _appointmentRepository.GetAllAsync())
                .Where(a => a.AnimalOwnerId == owner.Id)
                .ToList();

            var result = new List<UpcomingAppointmentDto>();
            foreach (var appt in appointments)
            {
                var vet = await _veterinarianRepository.GetByIdAsync(appt.VeterinarianId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user?.Name} {user?.Surname}",
                });
            }

            return result;
        }
    }
}
