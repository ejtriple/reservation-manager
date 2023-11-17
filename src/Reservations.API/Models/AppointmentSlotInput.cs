namespace Reservations.API.Models;

public class AppointmentSlotInput
{
    public string UserName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}