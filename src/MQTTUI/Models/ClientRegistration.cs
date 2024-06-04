namespace MQTTUI.Models;

public class ClientRegistration
{
    public int Id { get; set; }
    public string DeviceSNo { get; set; } = default!;
    public string DeviceName { get; set; } = default!;
    public DateTime RegisteredDateTime { get; set; }
    public DateTime LastAccessed { get; set; } 
    public bool IsActive { get; set; }
}