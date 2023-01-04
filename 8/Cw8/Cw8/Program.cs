using Cw8.Models;
using Cw8.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PharmacyContext>(opt => 
{
    opt.LogTo(Console.WriteLine);
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IPrescriptionService, PrescriptionService>();

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
