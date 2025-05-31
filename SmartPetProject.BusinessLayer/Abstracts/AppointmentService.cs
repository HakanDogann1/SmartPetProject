using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.BusinessLayer.Interfaces;
using SmartPetProject.DataAccessLayer.Context;
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
        private readonly AppDbContext _context;
        public AppointmentService(IRepository<Appointment> appointmentRepository, IRepository<Veterinarian> veterinarianRepository, IRepository<AnimalOwner> animalOwnerRepository, UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _appointmentRepository = appointmentRepository;
            _veterinarianRepository = veterinarianRepository;
            _animalOwnerRepository = animalOwnerRepository;
            _userManager = userManager;
            _context = context;
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
            appointment.Status = true;
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
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == appt.AnimalId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user.Name} {user.Surname}",
                    Age = animal?.Age,
                    Genus = animal?.Genus,
                    Name = animal?.Name,
                    Status = appt.Status,
                    weight = animal?.weight
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
                var value = (await _veterinarianRepository.GetByIdAsync(appt.VeterinarianId)).UserId;
                var veterinarian = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == value);
                var vet = await _veterinarianRepository.GetByIdAsync(appt.VeterinarianId);
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == appt.AnimalId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{veterinarian?.Name} {veterinarian?.Surname}",
                    weight = animal?.weight,
                    Age = animal?.Age,
                    Status = appt.Status,
                    Name = animal?.Name,
                    Genus = animal?.Genus
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
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == appt.AnimalId);
                result.Add(new UpcomingAppointmentDto
                {
                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user.Name} {user.Surname}",
                    Genus = animal?.Genus,
                    Name = animal?.Name,
                    Age = animal?.Age,
                    Status = appt.Status,
                    weight = animal?.weight
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
                var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == appt.AnimalId);
                result.Add(new UpcomingAppointmentDto
                {

                    Id = appt.Id,
                    AppointmentDate = appt.AppointmentDate,
                    AppointmentTime = appt.AppointmentTime,
                    VeterinarianName = $"{user?.Name} {user?.Surname}",
                    weight = animal?.weight,
                    Name = animal?.Name,
                    Status = appt.Status,
                    Genus = animal?.Genus,
                    Age = animal?.Age
                });
            }

            return result;
        }

        public async Task UpdateVeterinarianAppointmentAsync(VeterinarianUpdateAppointmentDto veterinarianUpdateAppointmentDto)
        {
            var value =await _appointmentRepository.GetByIdAsync(veterinarianUpdateAppointmentDto.appointmentId);
            var appointment = new Appointment
            {
                Id = value.Id,
                AppointmentDate = value.AppointmentDate,
                AppointmentTime = value.AppointmentTime,
                DurationInMinutes = value.DurationInMinutes,
                AnimalId = value.AnimalId,
                AnimalOwnerId = value.AnimalOwnerId,
                VeterinarianId = value.VeterinarianId,
                Room = value.Room,
                Status = true,
                Note = veterinarianUpdateAppointmentDto.Note 
            };
            
             _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task CheckAndUpdateAppointmentStatusesAsync()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            foreach (var appointment in appointments)
            {
                var appointmentDateTime = appointment.AppointmentDate.Date + appointment.AppointmentTime;

                if (appointmentDateTime < DateTime.Now && appointment.Status == true)
                {
                    appointment.Status = false;
                    _appointmentRepository.Update(appointment);
                }
            }

            await _appointmentRepository.SaveChangesAsync();
        }

    }
}
