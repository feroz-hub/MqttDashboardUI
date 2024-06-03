using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MQTTUI.Pages;

public class IndexModel(MqttService mqttService) : PageModel
{

    public List<string> Messages { get; private set; }
    public List<string> SubscribedTopics { get; private set; }
    public string ErrorMessage { get; private set; }
    public void OnGet()
    {
        Messages = [..mqttService.GetMessages()];
        SubscribedTopics = [..mqttService.GetSubscribedTopics()];
    }

    public async Task<IActionResult> OnPostAsync(string topic)
    {
        if (!string.IsNullOrEmpty(topic))
            
        {
            try
            {
                await mqttService.SubscribeToTopic(topic);

            }
            catch (Exception e)
            {
                ErrorMessage = $"Error while subscribing to{e.Message}:{e.StackTrace}";
            }
        }

        Messages = mqttService.GetMessages().ToList();
        SubscribedTopics = mqttService.GetSubscribedTopics().ToList();
        return Page();
    }

    public IActionResult OnGetMessages()
    {
        var messages = mqttService.GetMessages();
        return new JsonResult(messages);
    }
    public int GetMessageCountForTopic(string topic)
    {
        return mqttService.GetMessages().Count(m => m.Contains(topic));
    }
}