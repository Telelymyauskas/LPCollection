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

    [HttpPut("add-to-wishlist")]
    public async Task<IActionResult> AddToWishlist([FromQuery(Name = "recordid")] int id)
    {
        var result = await _mediator.Send(new AddRecordToWishlistCommand
        {
            Id = id
        });
        if (result == 0)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPut("add-to-library")]
    public async Task<IActionResult> AddToLibrary([FromQuery(Name = "recordid")] int id)
    {
        var result = await _mediator.Send(new AddRecordToLibraryCommand
        {
            Id = id
        });
        if (result == 0)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}
