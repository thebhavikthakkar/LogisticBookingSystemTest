using LogisticBookingSystem;
using LogisticBookingSystem.Application.Commands;
using LogisticBookingSystem.Application.Handlers;
using LogisticBookingSystem.Application.Queries;
using LogisticsBookingSystem.Core.Entities;
using LogisticsBookingSystem.Core.Repositories;
using LogisticsBookingSystem.Infrastructure.Data.Contexts;
using LogisticsBookingSystem.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseSqlServer(connectionString));
MappingProfiles.InstallServices(builder.Services);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    
    cfg.RegisterServicesFromAssemblyContaining<GetAllBookingsQuery>();
    cfg.RegisterServicesFromAssemblyContaining<GetBookingsByDateRangeQuery>();
    cfg.RegisterServicesFromAssemblyContaining<IRequest<List<Booking>>>();
    cfg.RegisterServicesFromAssemblyContaining<GetBookingByLocationIdQuery>();


    cfg.RegisterServicesFromAssemblyContaining<IRequestHandler<GetAllBookingsQuery, List<Booking>>>();
    cfg.RegisterServicesFromAssemblyContaining<IRequestHandler<GetBookingByLocationIdQuery, List<Booking>>>();
    cfg.RegisterServicesFromAssemblyContaining<IRequestHandler<GetBookingsByDateRangeQuery, List<Booking>>>();
    cfg.RegisterServicesFromAssemblyContaining<IRequestHandler<GetLocationByLocationIDQuery, Location>>();
    cfg.RegisterServicesFromAssemblyContaining<IRequestHandler<GetLocationQuery, List<Location>>>();
 
    cfg.Lifetime = ServiceLifetime.Scoped;
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
