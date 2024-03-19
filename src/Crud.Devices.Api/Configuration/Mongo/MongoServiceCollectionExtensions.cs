using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Crud.Devices.Api.Configuration.Mongo;

public static class MongoServiceCollectionExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoConfiguration = configuration.GetSection("Mongo").Get<MongoConfiguration>();
        if(mongoConfiguration is null)
            throw new MongoConfigurationException("Missing Mongo configuration.");
        var mongoClient = new MongoClient(MongoClientSettings.FromConnectionString(mongoConfiguration.ConnectionString));
        
        AddConventions();
        services
            .AddSingleton(p => mongoClient.GetDatabase(mongoConfiguration.Database));
        
        return services;
    }


    private static void AddConventions()
    {
        ConventionRegistry.Register("Ignore extra elements and do not serialize nulls",
            new ConventionPack
            {
                new IgnoreExtraElementsConvention(true), 
                new IgnoreIfNullConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String)
            },
            t => true);
    }
}