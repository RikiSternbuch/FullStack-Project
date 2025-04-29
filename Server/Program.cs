
using BL;
using BL.Api;
using BL.Services;
using Dal;
using Dal.Api;
using Dal.Models;
using Dal.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDal, DalManager>();
builder.Services.AddSingleton<IBL, BLManager>();
//builder.Services.AddScoped<IBLUser, BLUserService>();
//builder.Services.AddScoped<ITherapist, TherapistService>();
builder.Services.AddSingleton<DatabaseManager>();
builder.Services.AddSingleton<IBusyAppointment, BusyAppoitmentService>();
builder.Services.AddSingleton<IClient, ClientService>();
builder.Services.AddSingleton<IEmptyAppointment, EmptyAppointmentService>();
builder.Services.AddSingleton<ITherapist, TherapistService>();
//builder.Services.AddSingleton<IDal, DalManager>();
builder.Services.AddControllers();

var app = builder.Build();


app.MapControllers();

app.Run();