using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace DAL.Models
{
    [Table("Guest")]
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<ServiceMembership> Services { get; set; }

        public Guest()
        {
            Services = new List<ServiceMembership>();
        }

        protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>().HasOptional(r => r.Services);
            OnModelCreating(modelBuilder);
        }
    }
}
