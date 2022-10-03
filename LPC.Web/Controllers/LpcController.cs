using Microsoft.AspNetCore.Mvc;
using LPC.Domain.Database;
using System.Threading.Tasks;
using MediatR;
using LPC.Domain.Commands;

namespace LPC.Web.Controller;
// public class LpcController : ControllerBase
// {
//     private readonly LpcDbContext _dbContext;

//     public LpcController(LpcDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }

    public class LpcController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LpcController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("homepage")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllRecordsQuery());

            return Ok(result);
        }
    }
