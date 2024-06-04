namespace MQTTUI.Infrastructure;

public interface IClientRepository
{
    Task<List<string>> GetActiveClientIdsAsync();
    Task<List<ClientRegistration>> GetAllClientsAsync();
    Task<List<string>> GetAllClient();
    Task RegisterClientAsync(string deviceSNo, string deviceName);
    Task UpdateClientAsync(ClientRegistration clientRegistration);
}