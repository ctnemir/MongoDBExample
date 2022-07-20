using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDBExample.Models;
using MongoDBExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<MongoDBSettings>(
   builder.Configuration.GetSection(nameof(MongoDBSettings)));
builder.Services.AddSingleton<IMongoDBSettings>(sp =>
sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
builder.Services.AddSingleton<UserMusicFavoritesService>();
builder.Services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
