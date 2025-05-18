using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPetProject.EntityLayer.Entities
{
    public class Room : BaseEntity
    {
        public string RoomName { get; set; }   
        public string RoomUrl { get; set; }    

        public string AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
