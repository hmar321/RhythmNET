using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RhythmBack.Model.Context;
using RhythmBack.Model.Util;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AutoMapperConfig>();
builder.Services.AddSingleton<IMapper>(sp => sp.GetRequiredService<AutoMapperConfig>().Configure());

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
                          policy =>
                          {
                              policy.AllowAnyOrigin()
                                     .AllowAnyHeader()
                                    .AllowAnyMethod();
                          });
});


builder.Services.AddDbContext<RhythmDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MySqlConnection"))), ServiceLifetime.Scoped
);
//MySqlRailWay
//MySqlConnection
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
app.UseCors("MyPolicy");
app.MapControllers();

app.Run();