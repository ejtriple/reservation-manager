using Reservations.API.Models;
using Reservations.API.Services;

namespace Reservations.API.GraphQL;

public class Mutation
{
    private readonly IProviderService _providerService;

    public Mutation(
        IProviderService providerService
    )
    {
        _providerService = providerService;
    }

    public async Task<IEnumerable<AppointmentSlotModel>> CreateProviderAppointments(string userName, DateTime startTime,
        DateTime endTime)
    {
        return await _providerService.CreateProviderAppointmentSlots(userName, startTime, endTime);
    }

    public async Task<AppointmentSlotModel> ReserveAppointment(string userName, Guid appointmentId)
    {
        return await _providerService.ReserveAppointmentSlot(userName, appointmentId);
    }

    public async Task<AppointmentSlotModel> ConfirmAppointment(string userName, Guid appointmentId)
    {
        return await _providerService.ConfirmAppointmentSlot(userName, appointmentId);
    }

    public async Task<IEnumerable<AppointmentSlotModel>> ExpireAppointmentSlot()
    {
        return await _providerService.ExpireAppointmentSlot();
    }
}