using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartPetProject.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.DataAccessLayer.Context
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Veterinarian> Veterinarians { get; set; }
        public DbSet<AnimalOwner> AnimalOwners { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<VaccinationCard> VaccinationCards { get; set; }
        public DbSet<AnimalSpecies> AnimalSpecieses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Animal>()
                .HasOne(a=>a.AnimalSpecies)
                .WithMany()
                .HasForeignKey(b => b.AnimalSpeciesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vaccination>()
               .HasOne(a => a.AnimalSpecies)
               .WithMany()
               .HasForeignKey(vc => vc.AnimalSpeciesId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VaccinationCard>()
                .HasOne(a=>a.Vaccination)
                .WithMany()
                .HasForeignKey(vc => vc.VaccinationId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<VaccinationCard>()
                .HasOne(a => a.Animal)
                .WithMany()
                .HasForeignKey(vc => vc.AnimalId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Veterinarian)
                .WithMany()
                .HasForeignKey(a => a.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.AnimalOwner)
                .WithMany()
                .HasForeignKey(a => a.AnimalOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(r => r.RoomName).IsRequired();
                entity.Property(r => r.RoomUrl).IsRequired();

                entity.HasOne(r => r.Appointment)
                      .WithOne(a => a.Room)
                      .HasForeignKey<Room>(r => r.AppointmentId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
   

}
