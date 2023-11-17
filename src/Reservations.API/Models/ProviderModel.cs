namespace Reservations.API.Models;

public class ProviderModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    //public virtual ICollection<AppointmentSlotModel> AppointmentSlots { get; set; }

    public ProviderModel()
    {
        
    }
    public ProviderModel(Guid id, string name, string username)
    {
        Id = id;
        Name = name;
        UserName = username;
    }
    


}
