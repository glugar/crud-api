namespace Crud.Devices.Domain;

public class Device
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public string Brand { get; private set; }
    public DateTime CreationDate { get; private set; }
    
    
    public static Device Create(string name, string brand)
    {
        return new Device
        {
            Name = name,
            Brand = brand,
            CreationDate = DateTime.Now
        };
    }
    
    public static Device Fill(string id, string name, string brand, DateTime creationDate)
    {
        return new Device
        {
            Id = id,
            Name = name,
            Brand = brand,
            CreationDate = creationDate
        };
    }
    
}