using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Windows;

namespace DAL.Models
{
    [Table("Service")]
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Cost { get; set; }
        public List<Reservation> Reservations { get; set; }
        [NotMapped]
        public Visibility closeVisibility { get; set; }

        protected void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasOptional(r => r.Reservations);
            OnModelCreating(modelBuilder);
        }
    }
}
