using MQTTUI.Data;

namespace MQTTUI.Infrastructure;

public class ClientRepository(ApplicationDbContext dbContext):IClientRepository
{
    public async Task<List<string>> GetActiveClientIdsAsync()
    {
        return await dbContext.ClientRegistrations
            .Where(client => client.IsActive)
            .Select(client => client.DeviceSNo)
            .ToListAsync();
    }

    public async Task<List<string>> GetAllClient()
    {
       return await dbContext.ClientRegistrations
                                    .Select(client => client.DeviceName)
                                    .ToListAsync();
    }

    public async Task<List<ClientRegistration>> GetAllClientsAsync()
    {
        return await dbContext.ClientRegistrations.ToListAsync();
    }

    public async Task RegisterClientAsync(string deviceSNo, string deviceName)
    {
        var client = await dbContext.ClientRegistrations
            .FirstOrDefaultAsync(c => c.DeviceSNo == deviceSNo);

        if (client == null)
        {
            client = new ClientRegistration
            {
                DeviceSNo = deviceSNo,
                DeviceName = deviceName,
                RegisteredDateTime = DateTime.UtcNow,
                LastAccessed = DateTime.UtcNow,
                IsActive = true
            };
            dbContext.ClientRegistrations.Add(client);
        }
        else
        {
            client.LastAccessed = DateTime.UtcNow;
            client.IsActive = true;
            dbContext.ClientRegistrations.Update(client);
        }

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateClientAsync(ClientRegistration clientRegistration)
    {
        var existingClient = await dbContext.ClientRegistrations
            .FirstOrDefaultAsync(c => c.Id == clientRegistration.Id);

        if (existingClient != null)
        {
            existingClient.DeviceSNo = clientRegistration.DeviceSNo;
            existingClient.DeviceName = clientRegistration.DeviceName;
            existingClient.LastAccessed = clientRegistration.LastAccessed;
            existingClient.IsActive = clientRegistration.IsActive;

            dbContext.ClientRegistrations.Update(existingClient);
            await dbContext.SaveChangesAsync();
        }
    }
}