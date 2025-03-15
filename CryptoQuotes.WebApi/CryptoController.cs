using CryptoQuotes.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CryptoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{cryptoSymbol}")]
    public async Task<IActionResult> GetCryptoQuote(string cryptoSymbol)
    {
        var result = await _mediator.Send(new GetCryptoQuoteQuery(cryptoSymbol));
        
        return result.IsSuccess ? Ok(result) : Problem(result.Error.Message);
    }
}