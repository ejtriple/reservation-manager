using Microsoft.EntityFrameworkCore;
using Reservations.API.Models;

namespace Reservations.API;

public class ReservationDbContext : DbContext
{
    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ProviderModel> Provider { get; set; }
    public DbSet<AppointmentSlotModel> AppointmentSlot { get; set; }
}