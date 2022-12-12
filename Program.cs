using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ZdravotniSystem.Configuration;
using ZdravotniSystem.Configuration.Data;
using ZdravotniSystem.Configuration.Security;
using ZdravotniSystem.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHostedService<LifeTimeService>();

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

app.MapFallbackToFile("index.html");

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

builder.Services.AddSingleton<IDataService>();
builder.Services.AddScoped<JwtHandler>();

builder.Services.Add(new ServiceDescriptor(typeof(IAppointmentService), new AppointmentService()));
builder.Services.Add(new ServiceDescriptor(typeof(IDoctorService), new DoctorService()));
builder.Services.Add(new ServiceDescriptor(typeof(IPatientService), new PatientService()));

app.UseAuthentication();
app.UseAuthorization();

app.Run();

