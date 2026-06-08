using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DeviceTrackingAPI;

// --- STRUKTUR ENTITAS (MODELS) ---

public class Device
{
    public int Id { get; set; }
        
            [Required]
                public string DeviceName { get; set; } = string.Empty;
                    
                        [Required]
                            public string DeviceType { get; set; } = string.Empty;
                                
                                    public string? AssignedEmployee { get; set; }
                                        
                                            public string Status { get; set; } = "Unassigned"; 
                                                
                                                    public DateTime LastCheckedDate { get; set; } = DateTime.UtcNow;
                                                        
                                                            public string? Notes { get; set; }
                                                            }

                                                            public class DeviceLog
                                                            {
                                                                [Key]
                                                                    public int LogId { get; set; }
                                                                        public int DeviceId { get; set; }
                                                                            public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
                                                                                public string? PreviousStatus { get; set; }
                                                                                    public string? NewStatus { get; set; }
                                                                                        public string? ActionBy { get; set; }
                                                                                        }

                                                                                        // --- JEMBATAN DATABASE (DBCONTEXT) ---

                                                                                        public class AppDbContext : DbContext
                                                                                        {
                                                                                            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
                                                                                                
                                                                                                    public DbSet<Device> Devices => Set<Device>();
                                                                                                        public DbSet<DeviceLog> DeviceLogs => Set<DeviceLog>();
                                                                                                        }
                                                                                                        