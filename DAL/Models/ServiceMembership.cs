using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace DAL.Models
{
    [Table("ServiceMembership")]
    public class ServiceMembership
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int GuestId { get; set; }
        public int ReservationId { get; set; }
        public int Duration { get; set; }
        public int TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
    }
}
