using Microsoft.EntityFrameworkCore;
using Reservations.API.Models;

namespace Reservations.API;

public class DataSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using( var context = new ReservationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ReservationDbContext>>()))
        {

            if( context.Provider.Any())
            {
                return;
            }

            var provider1 = new ProviderModel(Guid.NewGuid(), "Dr. Jekyll", "@DJekyll");
            var expiredAppointment = new AppointmentSlotModel(
                Guid.NewGuid(),
                new DateTime(2023, 11, 16, 8, 0, 0),
                new DateTime(2023, 11, 16, 8, 15, 0),
                provider1);
            expiredAppointment.ClientUserName = "ETPhoneHome";
            expiredAppointment.Status = AppointmentSlotStatus.Reserved;
            expiredAppointment.AppointmentReservationExpiration = DateTime.Now.AddMinutes(-60);
            context.AppointmentSlot.Add(expiredAppointment);
            context.Provider.Add(provider1);

            //createSlots(new DateTime(2023, 11, 16, 8, 0, 0), new DateTime(2023, 11, 16, 15, 59, 0), provider1, context);
            // context.AppointmentSlot.Add(
            //     new AppointmentSlotModel(
            //         Guid.NewGuid(),
            //         new DateTime(2023, 11, 16, 8, 0, 0),
            //         new DateTime(2023, 11, 16, 16, 0, 0),
            //         provider1
            //     ));
            
            context.SaveChanges();
        }
    }

    private static void createSlots(DateTime startTime, DateTime endTime, ProviderModel provider, ReservationDbContext context)
    {
        TimeSpan interval = TimeSpan.FromMinutes(15);

        for (DateTime slotStart = startTime; slotStart < endTime; slotStart += interval)
        {
            DateTime slotEnd = slotStart + interval;
            context.AppointmentSlot.Add(
                new AppointmentSlotModel(
                    Guid.NewGuid(),
                    slotStart,
                    slotEnd,
                    provider));
        }
    }
}