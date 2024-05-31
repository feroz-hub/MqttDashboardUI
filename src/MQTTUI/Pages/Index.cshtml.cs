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

    public IActionResult OnGetMessages()
    {
        var messages = mqttService.GetMessages();
        return new JsonResult(messages);
    }
}