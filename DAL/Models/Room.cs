using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    [Table("Room")]
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Category { get; set; }
        public int Cost { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
    }
}

