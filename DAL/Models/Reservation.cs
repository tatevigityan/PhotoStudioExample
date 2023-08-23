using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public int NumberOfPeople { get; set; }
        public int ServicesNumber { get; set; }
        public int TotalPrice { get; set; }
        public bool isActive { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReservationDate { get; set; }
        public List<Guest> Guests { get; set; }

        [NotMapped]
        public string Manager { get; set; }
        [NotMapped]
        public string Room { get; set; }

        public Reservation()
        {
            Guests = new List<Guest>();
        }
    }
}
