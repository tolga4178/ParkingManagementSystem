using Microsoft.EntityFrameworkCore;
using ParkingManagementSystem.Application.Repositories;
using ParkingManagementSystem.Application.Repositories.ParkingRecord;
using ParkingManagementSystem.Application.Repositories.Region;
using ParkingManagementSystem.Application.Services;
using ParkingManagementSystem.Persistance.Context;
using ParkingManagementSystem.Persistance.Repositories.ParkingRecord;
using ParkingManagementSystem.Persistance.Repositories.Region;
using ParkingManagementSystem.Persistance.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<ParkingManagementSystemContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IParkingRecordReadRepository, ParkingRecordReadRepository>();
builder.Services.AddScoped<IParkingRecordWriteRepository, ParkingRecordWriteRepository>();
builder.Services.AddScoped<IRegionReadRepository, RegionReadRepository>();
builder.Services.AddScoped<IRegionWriteRepository, RegionWriteRepository>();
builder.Services.AddScoped<IParkingService,ParkingService>();



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
