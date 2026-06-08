using Microsoft.EntityFrameworkCore;
using DeviceTrackingAPI;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=tracking.db"));

var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();

// --- MULAI BLOK API ENDPOINT ---

// 1. Dapatkan Dasbor & Semua Perangkat
app.MapGet("/api/devices", async (AppDbContext db) => {
    var devices = await db.Devices.ToListAsync();
        
            // Kalkulasi metrik dasbor secara real-time
                var dashboard = new {
                        TotalDevices = devices.Count,
                                AssignedDevices = devices.Count(d => !string.IsNullOrEmpty(d.AssignedEmployee)),
                                        UnassignedDevices = devices.Count(d => d.Status == "Unassigned"),
                                                InRepair = devices.Count(d => d.Status == "In Repair")
                                                    };
                                                        
                                                            return Results.Ok(new { DashboardMetrics = dashboard, DevicesList = devices });
                                                            });

                                                            // 2. Tambah Perangkat Baru
                                                            app.MapPost("/api/devices", async (Device device, AppDbContext db) => {
                                                                db.Devices.Add(device);
                                                                    await db.SaveChangesAsync();
                                                                        return Results.Created($"/api/devices/{device.Id}", device);
                                                                        });

                                                                        // 3. Update Perangkat & Catat Log (Fitur Bonus!)
                                                                        app.MapPut("/api/devices/{id}", async (int id, Device inputDevice, AppDbContext db) => {
                                                                            var device = await db.Devices.FindAsync(id);
                                                                                if (device is null) return Results.NotFound();

                                                                                    // Deteksi perubahan status untuk dicatat ke dalam log history
                                                                                        if (device.Status != inputDevice.Status || device.AssignedEmployee != inputDevice.AssignedEmployee) {
                                                                                                db.DeviceLogs.Add(new DeviceLog {
                                                                                                            DeviceId = id,
                                                                                                                        PreviousStatus = device.Status,
                                                                                                                                    NewStatus = inputDevice.Status,
                                                                                                                                                ActionBy = "System Admin" 
                                                                                                                                                        });
                                                                                                                                                            }

                                                                                                                                                                // Update data utama
                                                                                                                                                                    device.DeviceName = inputDevice.DeviceName;
                                                                                                                                                                        device.DeviceType = inputDevice.DeviceType;
                                                                                                                                                                            device.AssignedEmployee = inputDevice.AssignedEmployee;
                                                                                                                                                                                device.Status = inputDevice.Status;
                                                                                                                                                                                    device.Notes = inputDevice.Notes;
                                                                                                                                                                                        device.LastCheckedDate = DateTime.UtcNow;

                                                                                                                                                                                            await db.SaveChangesAsync();
                                                                                                                                                                                                return Results.NoContent();
                                                                                                                                                                                                });

                                                                                                                                                                                                // 4. Hapus Perangkat
                                                                                                                                                                                                app.MapDelete("/api/devices/{id}", async (int id, AppDbContext db) => {
                                                                                                                                                                                                    if (await db.Devices.FindAsync(id) is Device device) {
                                                                                                                                                                                                            db.Devices.Remove(device);
                                                                                                                                                                                                                    await db.SaveChangesAsync();
                                                                                                                                                                                                                            return Results.NoContent();
                                                                                                                                                                                                                                }
                                                                                                                                                                                                                                    return Results.NotFound();
                                                                                                                                                                                                                                    });

                                                                                                                                                                                                                                    // --- AKHIR BLOK API ENDPOINT ---



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
