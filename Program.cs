using MongoDB.Driver;
using DND.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve the MongoDB connection string and database name from environment variables
var mongoConnectionString = builder.Configuration["ConnectionString"];
var mongoDatabaseName = "AdventurersArchive";

// Configure MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(mongoConnectionString));

// Register the database
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabaseName));

// Register the CharacterSheet collection
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<CharacterSheet>("CharacterSheets"));

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();