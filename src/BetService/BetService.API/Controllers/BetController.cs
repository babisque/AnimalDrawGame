using BetService.Core.Dto;
using BetService.Core.Entities;
using BetService.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BetService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class BetController : ControllerBase
{
    private readonly IBetRepository _betRepository;

    public BetController(IBetRepository betRepository)
    {
        _betRepository = betRepository;
    }
    
    [Authorize]
    [HttpGet("HelloWorld")]
    public ActionResult Test()
    {
        return Ok("Hello World!");
    }

    [Authorize]
    [HttpPost("CreateABet")]
    public async Task<ActionResult> Post([FromBody] MakeBetReq req)
    {
        try
        {
            var bet = new Bet
            {
                Numbers = req.Numbers,
                EventDateTime = req.EventDateTime,
                UserId = req.UserId
            };

            await _betRepository.CreateAsync(bet);

            return CreatedAtAction(nameof(GetBetById), new { betId = bet.Id }, bet);
        }
        catch (Exception ex)
        {
            return BadRequest(new {ErrorMessages = ex.Message});
        }
    }

    [Authorize]
    [HttpGet("GetAll")]
    public async Task<ActionResult> GetBets()
    {
        try
        {
            var bets = await _betRepository.GetAllAsync();
            if (bets.Count == 0)
            {
                var betTest = new GetBetRes
                {
                    EventDateTime = DateTime.Now,
                    Id = Guid.NewGuid().ToString(),
                    Numbers = [1, 2, 3, 4],
                    UserId = "AJnmfaoj=Anhdoan d"
                };
                return Ok(betTest);
            }

            var res = new List<GetBetRes>();
            foreach (var bet in bets)
            {
                res.Add(new GetBetRes
                {
                    Id = bet.Id,
                    Numbers = bet.Numbers,
                    UserId = bet.UserId,
                    EventDateTime = bet.EventDateTime
                });
            }

            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new {ErrorMessages = ex.Message});
        }
    }

    [Authorize]
    [HttpGet("{betId}")]
    public async Task<ActionResult> GetBetById([FromRoute] string betId)
    {
        try
        {
            var bet = await _betRepository.GetByIdAsync(betId);
            if (String.IsNullOrEmpty(bet.Id))
                return NotFound();

            var res = new GetBetRes
            {
                Id = bet.Id,
                Numbers = bet.Numbers,
                UserId = bet.UserId,
                EventDateTime = bet.EventDateTime
            };

            return Ok(res);
        }
        catch (Exception ex)
        {
            return BadRequest(new {ErrorMessages = ex.Message});
        }
    }
}