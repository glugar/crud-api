using System.Text.Json.Serialization;
using Crud.Devices.Api.Configuration.Mongo;
using Crud.Devices.Api.Features;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddJsonFile("appsettings.json");

builder
    .Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.CustomSchemaIds(x => x.FullName);
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Devices Api", Version = "v1" });
    });


builder
    .Services
    .AddMongo(builder.Configuration)
    .AddDevicesFeature()
    .AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Singleton)
    .AddMediatR(o =>
    {
        o.Lifetime = ServiceLifetime.Singleton;
        o.RegisterServicesFromAssembly(typeof(Program).Assembly);
    })
    .Configure<JsonOptions>(options =>
    {
        options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

var app = builder.Build();

app
    .UseSwagger()
    .UseSwaggerUI()
    .UseDefaultFiles(GetDefaultFileOptions())
    .UseStaticFiles();

app
    .MapDevicesEndpoints();

app.Run();


DefaultFilesOptions GetDefaultFileOptions()
{
    var fileOptions = new DefaultFilesOptions();
    fileOptions.DefaultFileNames.Clear();
    fileOptions.DefaultFileNames.Add("index.html");
    return fileOptions;
}