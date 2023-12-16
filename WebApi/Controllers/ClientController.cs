using Application.Features.Clients.Commands.CreateClientCommand;
using Application.Features.Clients.Queries.GetAllClientsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseApiController
    {
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await Mediator!.Send(new GetAllClientsQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> SaveClientAsync(CreateClientCommand request)
        {
            var response = await Mediator!.Send(request);

            return Ok(response);
        }
    }
}
