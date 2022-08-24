using Microsoft.AspNetCore.Mvc;

namespace SZTestTask.Controllers;

[ApiController]
[Route("[controller]")]
public class RGDialogsClientsController : ControllerBase
{
    [HttpPost(Name = "GetDIalogID")]
    public Guid Post([FromBody] List<Guid> clientIds)
    {
        var rgdc = new RGDialogsClients();
        var rgDialogsClientsList = rgdc.Init();
        var dict = new Dictionary<Guid, List<Guid>>();
        foreach (var dialog in rgDialogsClientsList)
        {
            if (dict.ContainsKey(dialog.IDRGDialog))
            {
                dict[dialog.IDRGDialog].Add(dialog.IDClient);
            }
            else
            {
                dict.Add(dialog.IDRGDialog, new List<Guid>{dialog.IDClient});
            }
        }

        foreach (var kvp in dict)
        {
           if (kvp.Value.All(clientIds.Contains) && clientIds.All(kvp.Value.Contains))
           {
                return kvp.Key;
           }
        }

        return Guid.Empty;
    }
}