using Crud.Devices.Infrastructure.Devices;
using MongoDB.Driver;

namespace Crud.Devices.Infrastructure.Tests;


internal static class ReposHolder
{
    private static RepoSetup instance;
    internal static RepoSetup Instance => instance ??= new();
}

public class RepoSetup : IDisposable
{
    
    
    internal RepoSetup()
    {
        MongoClient = new MongoClient(Environment.GetEnvironmentVariable("mongoDbConnectionString") ?? "mongodb://localhost:27017");

        IMongoDatabase db =
            MongoClient.GetDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? "DevicesTest");
        DevicesRepository = new DevicesRepository(db);

    }
    private IMongoClient MongoClient { get; }
    
    public IDevicesRepository DevicesRepository { get; }

    public void Dispose()
    {
        MongoClient.DropDatabase(Environment.GetEnvironmentVariable("DatabaseName") ?? "DevicesTest");
    }
}