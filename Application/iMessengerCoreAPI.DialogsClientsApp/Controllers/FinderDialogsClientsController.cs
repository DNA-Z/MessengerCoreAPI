using iMessengerCoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace iMessengerCoreAPI.DialogsClientsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinderDialogsClientsController : ControllerBase
    {
        private static readonly List<Guid> idClient = new List<Guid>
        {
            new Guid("7de3299b-2796-4982-a85b-2d6d1326396e"),
            new Guid("0a58955e-342f-4095-88c6-1109d0f70583"),
            new Guid("50454d55-a73c-4cbc-be25-3c5729dcb82b")
        };

        private readonly ILogger<FinderDialogsClientsController> _logger;

        public FinderDialogsClientsController(ILogger<FinderDialogsClientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "SearchIdDialog")]
        public Guid Get([FromQuery] List<Guid> idClientsInput)
        {
            RGDialogsClients dialogs = new RGDialogsClients();
            var allDialogs = dialogs.Init();

            return allDialogs.GroupBy(x => x.IDRGDialog)
                .Where(x => x.Select(y => y.IDClient).OrderBy(y => y)
                .SequenceEqual(idClientsInput.OrderBy(o => o)))
                .Select(x => x.Key).FirstOrDefault();
        }
    }
}