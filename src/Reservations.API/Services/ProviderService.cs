using Microsoft.EntityFrameworkCore;
using Reservations.API.Models;

namespace Reservations.API.Services;
public interface IProviderService
{
    Task<IEnumerable<ProviderModel>> GetProviders();
    Task<IEnumerable<AppointmentSlotModel>> GetAppointmentSlots();
    Task<IEnumerable<AppointmentSlotModel>> CreateProviderAppointmentSlots(string userName, DateTime startTime,
        DateTime endTime);
    Task<AppointmentSlotModel> ReserveAppointmentSlot(string clientUserName, Guid appointmentId);
    Task<AppointmentSlotModel> ConfirmAppointmentSlot(string clientUserName, Guid appointmentId);

    Task<IEnumerable<AppointmentSlotModel>> ExpireAppointmentSlot();
}

public class ProviderService : IProviderService
{

    private readonly ReservationDbContext _dbContext;
    
    public ProviderService(IDbContextFactory<ReservationDbContext> dbContextFactory)
    {
        _dbContext = dbContextFactory.CreateDbContext();
    }
    public async Task<IEnumerable<ProviderModel>> GetProviders()
    {
        return await _dbContext.Provider.ToListAsync();
    }

    public async Task<IEnumerable<AppointmentSlotModel>> GetAppointmentSlots()
    {
        return await _dbContext.AppointmentSlot.Include(a => a.Provider).ToListAsync();
    }

    public Task<IEnumerable<AppointmentSlotModel>> CreateProviderAppointmentSlots(string userName, DateTime startTime, DateTime endTime)
    {
        var provider = _dbContext.Provider.FirstOrDefault(_ => _.UserName == userName);

        //If the provider doesn't exist, let's get out of here.
        if (provider == null)
            return Task.FromResult(Enumerable.Empty<AppointmentSlotModel>());
        
        TimeSpan interval = TimeSpan.FromMinutes(15);

        var appointmentsToAdd = new List<AppointmentSlotModel>();
        // Get a list of the existing start times for this provider so that we don't duplicate
        var existingStartTimes = _dbContext.AppointmentSlot.Where(x =>
            x.ProviderId == provider.Id && x.StartTime >= startTime && startTime <= endTime).Select(x => x.StartTime);
        
        for (DateTime slotStart = startTime; slotStart < endTime; slotStart += interval)
        {
            // Don't add the slot if it already exists
            if( existingStartTimes.Contains(slotStart) )
                continue;
            
            DateTime slotEnd = slotStart + interval;
            appointmentsToAdd.Add(new AppointmentSlotModel(
                Guid.NewGuid(),
                slotStart,
                slotEnd,
                provider));
                
        }
        _dbContext.AddRange(appointmentsToAdd);
        _dbContext.SaveChanges();

        return Task.FromResult<IEnumerable<AppointmentSlotModel>>(appointmentsToAdd);
        
    }

    public Task<AppointmentSlotModel> ReserveAppointmentSlot(string clientUserName, Guid appointmentId)
    {
        var appointmentToReserve = _dbContext.AppointmentSlot.FirstOrDefault(_ => _.Id == appointmentId);

        // If we can't find the appointment, or if it's within 24 hours, don't reserve the reservation.
        // TODO: Change this to return a GraphQL Error based on either condition.
        if (appointmentToReserve == null || appointmentToReserve.StartTime < DateTime.Now.AddHours(24))
            return Task.FromResult(appointmentToReserve);

        appointmentToReserve.ClientUserName = clientUserName;
        appointmentToReserve.Status = AppointmentSlotStatus.Reserved;
        appointmentToReserve.AppointmentReservationExpiration = DateTime.Now.AddMinutes(30);

        _dbContext.SaveChanges();

        return Task.FromResult(appointmentToReserve);
    }
    
    public Task<AppointmentSlotModel> ConfirmAppointmentSlot(string clientUserName, Guid appointmentId)
    {
        //TODO: Change this to use a confirmation ID instead of clientUserName + appointmentId
        var appointmentToConfirm = _dbContext.AppointmentSlot.FirstOrDefault(_ => _.Id == appointmentId);

        // If we can't find the appointment, or if it's within 24 hours, don't reserve the reservation.
        // TODO: Change this to return a GraphQL Error based on either condition.
        if (appointmentToConfirm == null || appointmentToConfirm.StartTime < DateTime.Now.AddHours(24))
            return Task.FromResult(appointmentToConfirm);
        
        appointmentToConfirm.Status = AppointmentSlotStatus.Confirmed;
        appointmentToConfirm.AppointmentReservationExpiration = DateTime.MaxValue;

        _dbContext.SaveChanges();

        return Task.FromResult(appointmentToConfirm);
    }
    
    public Task<IEnumerable<AppointmentSlotModel>> ExpireAppointmentSlot()
    {
        //TODO: This should probably be filterable
        //TODO: Convert all DateTimes to UTC or Offset
        var appointmentsToExpire = _dbContext.AppointmentSlot.Include(_ => _.Provider).Where(x => x.AppointmentReservationExpiration < DateTime.Now).ToList();

        foreach (var appointment in appointmentsToExpire)
        {
            appointment.Status = AppointmentSlotStatus.Free;
            appointment.ClientUserName = string.Empty;
            appointment.AppointmentReservationExpiration = null;

        }

        _dbContext.SaveChanges();

        return Task.FromResult<IEnumerable<AppointmentSlotModel>>(appointmentsToExpire);
    }
}