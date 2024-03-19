using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Crud.Devices.Infrastructure.Devices.Models;

public class DeviceEntity
{
    [BsonId(IdGenerator = typeof (StringObjectIdGenerator))]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Brand { get; set; }
    
    public DateTime CreationDate { get; set; }
}