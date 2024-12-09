using MongoDB.Driver;
using DND.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve the MongoDB connection string and database name from environment variables
var mongoConnectionString = builder.Configuration["ConnectionString"];
var mongoDatabaseName = "Adventurers_Archive";

// Configure MongoDB client
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(mongoConnectionString));

// Register the database
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase(mongoDatabaseName));

// Register the CharacterSheet collection
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<CharacterSheet>("Sheets"));

// Register the OverallClasses collection
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<OverallClasses>("Classes"));

// Register the Armor collection
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Armor>("Armor"));
    
// Register the Weapon collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Weapon>("Weapons"));
    
// Register the Spell collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Spell>("Spells"));
    
// Register the Possession collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Possession>("Possessions"));

// Register the Races collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Race>("Races"));
    
// Register the Feats collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Feats>("Feats"));
    
// Register the Subclass collection
    builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<Subclass>("Subclasses"));

// Register the ClassFeature collection
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IMongoDatabase>().GetCollection<ClassFeature>("ClassFeatures"));

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();