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
        foreach (var group in rgDialogsClientsList
                     .GroupBy(x => x.IDRGDialog)
                     .ToList())
        {
            var groupedClients = group.Select(x => x.IDClient);
            if (clientIds.Union(groupedClients).SequenceEqual(clientIds))
            {
                return group.Key;
            }
        }

        return Guid.Empty;
    }
}