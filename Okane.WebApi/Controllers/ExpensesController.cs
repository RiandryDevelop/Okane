using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Services;

namespace Okane.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpensesService _expensesService;

    public ExpensesController(IExpensesService expensesService) => 
        _expensesService = expensesService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ModelStateDictionary))]
    public ActionResult<ExpenseResponse> Post(CreateExpenseRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            request.CreatedDate = DateTime.UtcNow;


        return _expensesService.Register(request);
    }

    [HttpGet]
    public IEnumerable<ExpenseResponse> Get(string? category) => 
        _expensesService.Retrieve(category);
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult ById(int id)
    {
        var response = _expensesService.ById(id);

        if (response == null)
            return NotFound();
        
        return Ok(response);
    }
}