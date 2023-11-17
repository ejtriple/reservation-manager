using Reservations.API.Models;
using Reservations.API.Services;

namespace Reservations.API.GraphQL;

public class Query
{
    private readonly IProviderService _providerService;

    public Query(
        IProviderService providerService
    )
    {
        _providerService = providerService;
    }

    public async Task<IEnumerable<ProviderModel>> GetProviders()
    {
        return await _providerService.GetProviders();
    }

    public async Task<IEnumerable<AppointmentSlotModel>> GetAppointmentSlots()
    {
        return await _providerService.GetAppointmentSlots();
    }
}