namespace Reservations.API.Models;

public class ClientModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }

    public ClientModel()
    {
        
    }
    public ClientModel(Guid id, string name, string username)
    {
        Id = id;
        Name = name;
        UserName = username;
    }
    


}
