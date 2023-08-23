namespace DAL
{
    using DAL.Models;
    using System.Data.Entity;

    public class PhotoStudioModel : DbContext
    {
        public PhotoStudioModel()
            : base("name=PhotoStudioModel")
        {
        }

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceMembership> ServiceMemberships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>()
                .HasMany(x => x.Guests)
                .WithMany(x => x.Reservations)
                .Map(m =>
                {
                    m.ToTable("GuestReservations");
                    m.MapLeftKey("GuestId");
                    m.MapRightKey("ReservationId");
                }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}