using ZdravotniSystem.Configuration;
using ZdravotniSystem.Configuration.Data;
using ZdravotniSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<LifeTimeService>();

builder.Services.AddSingleton<DataService>();

builder.Services.Add(new ServiceDescriptor(typeof(IAppointmentService), new AppointmentService()));
builder.Services.Add(new ServiceDescriptor(typeof(IDoctorService), new DoctorService()));
builder.Services.Add(new ServiceDescriptor(typeof(IPatientService), new PatientService()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
