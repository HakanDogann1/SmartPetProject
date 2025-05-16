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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
