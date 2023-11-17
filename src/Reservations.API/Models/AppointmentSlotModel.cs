using System.ComponentModel.DataAnnotations.Schema;

namespace Reservations.API.Models;

public class AppointmentSlotModel
{
    [Key]
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public AppointmentSlotStatus Status { get; set; }
    
    public DateTime? AppointmentReservationExpiration { get; set; }
    
    
    [ForeignKey(nameof(Provider))]
    public Guid ProviderId { get; set; }
    public virtual ProviderModel Provider { get; set; }
    
    public string? ClientUserName { get; set; }
    
    // [ForeignKey(nameof(Client))]
    // public Guid? ClientId { get; set; }
    // public virtual ClientModel? Client { get; set; }
    
    

    public AppointmentSlotModel()
    {
        
    }
    public AppointmentSlotModel(Guid id, DateTime startTime, DateTime endTime, ProviderModel provider )
    {
        Id = id;
        StartTime = startTime;
        EndTime = endTime;
        Provider = provider;
        Status = AppointmentSlotStatus.Free;
    }
    


}
