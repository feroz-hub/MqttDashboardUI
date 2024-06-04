namespace MQTTUI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):DbContext(options)
{
    public DbSet<ClientRegistration> ClientRegistrations { get; set; }
}