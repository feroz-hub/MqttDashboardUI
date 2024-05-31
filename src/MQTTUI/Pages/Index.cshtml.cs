using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MQTTUI.Pages;

public class IndexModel(MqttService mqttService) : PageModel
{

    public List<string> Messages { get; private set; }

    public void OnGet()
    {
        Messages = [..mqttService.GetMessages()];
    }

    public async Task<IActionResult> OnPostAsync(string topic)
    {
        if (!string.IsNullOrEmpty(topic))
        {
            await mqttService.SubscribeToTopic(topic);
        }

        Messages = mqttService.GetMessages().ToList();
        return Page();
    }

    public IActionResult OnGetMessages()
    {
        var messages = mqttService.GetMessages();
        return new JsonResult(messages);
    }
}