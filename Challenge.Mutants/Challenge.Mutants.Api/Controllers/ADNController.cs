using Challenge.Mutants.Application.Handlers;
using Challenge.Mutants.Application.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Challenge.Mutants.Api.Controllers
{
    [Route("")]
    [ApiController]
    public class ADNController : ControllerBase
    {
        private readonly IMediator mediator;

        public ADNController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("mutant")]
        public async Task<ActionResult> PostADN([FromBody] SaveADNModel model)
        {
            await mediator.Send(new SaveADNRequest(model));

            return Ok();
        }

        [HttpGet]
        [Route("stats")]
        public async Task<ActionResult> GetStats()
        {
            var result = await mediator.Send(new GetStatsADNRequest());

            return Ok(result);
        }
    }
}
