using MQTTUI.Data;
using MQTTUI.Infrastructure;

namespace MQTTUI;

public static class DependencyInjection
{
    public static IServiceCollection AddMqttService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();
        services.AddSingleton<MqttService>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddDbContext<ApplicationDbContext>(opt =>
            opt.UseSqlite(configuration.GetConnectionString("Database")));
        return services;
    }

    public static WebApplication UseMqttService(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

//app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        return app;

    }
    
}