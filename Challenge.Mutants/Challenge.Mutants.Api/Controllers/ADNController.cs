using Challenge.Mutants.Application.Handlers;
using Challenge.Mutants.Application.Models;
using Challenge.Mutants.Application.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Challenge.Mutants.Api.Controllers
{
    [Route("Challenge/Mutant/[controller]")]
    [ApiController]
    public class ADNController : ControllerBase
    {
        private readonly IMediator mediator;

        public ADNController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<ActionResult> PostADN([FromBody] SaveADNModel model)
        {
            await mediator.Send(new SaveADNRequest(model));

            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAllADN()
        {
            var result = await mediator.Send(new GetAllADNRequest());

            return Ok(result);
        }
    }
}
