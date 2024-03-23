using Crud.Devices.Domain;
using Crud.Devices.Infrastructure.Devices.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Crud.Devices.Infrastructure.Devices;

public class DevicesRepository(IMongoDatabase database) : IDevicesRepository
{
    private readonly IMongoCollection<DeviceEntity> _devices = database.GetCollection<DeviceEntity>("Devices");

    public async Task<Device> CreateDeviceAsync(Device device, CancellationToken cancellationToken)
    {
        var entity = device.MapToDeviceEntity();
        await _devices.InsertOneAsync(entity, null, cancellationToken).ConfigureAwait(false);
        return Device.Fill(entity.Id, entity.Name, entity.Brand, entity.CreationDate);
    }

    public async Task<Device?> GetDeviceByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<DeviceEntity>.Filter.Eq(d => d.Id, id);
        var device = await _devices.Find(filter).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        return device is not null ? Device.Fill(device.Id, device.Name, device.Brand, device.CreationDate) : null;
    }

    public async Task<List<Device>> GetDevicesAsync(string? brand, CancellationToken cancellationToken)
    {
        var filter = Builders<DeviceEntity>.Filter.Empty;
        
        if(brand is not null)
            filter = Builders<DeviceEntity>.Filter.Regex(d => d.Brand,  new BsonRegularExpression(brand, "i"));


        var devices = await _devices.Find(filter).ToListAsync(cancellationToken).ConfigureAwait(false);
        
        return devices?.Select(d => Device.Fill(d.Id, d.Name, d.Brand, d.CreationDate)).ToList() ?? [];
    }

    public async Task<bool> DeleteDeviceAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<DeviceEntity>.Filter.Eq(d => d.Id, id);
        var result=  await _devices.DeleteOneAsync(filter, cancellationToken).ConfigureAwait(false);

        return result.DeletedCount > 0;
    }

    public async Task<bool> UpdateDeviceAsync(string id, string? name, string? brand, CancellationToken cancellationToken)
    {
        var updates = new List<UpdateDefinition<DeviceEntity>>();
        if(name is not null)
            updates.Add(Builders<DeviceEntity>.Update.Set(d => d.Name, name));
        
        if(brand is not null)
            updates.Add(Builders<DeviceEntity>.Update.Set(d => d.Brand, brand));
        
        
        var filter = Builders<DeviceEntity>.Filter.Eq(d => d.Id, id);
        var result = await _devices.UpdateOneAsync(filter, Builders<DeviceEntity>.Update.Combine(updates), cancellationToken: cancellationToken).ConfigureAwait(false);
        return result.MatchedCount > 0;
    }
}