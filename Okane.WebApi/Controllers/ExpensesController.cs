using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Okane.Contracts;

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



    // DELETE /expenses/:id endpoint
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] 
    [ProducesResponseType(StatusCodes.Status404NotFound)]   
    public IActionResult Delete(int id)
    {
        
        var deleted = _expensesService.Delete(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

        [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExpenseResponse))]    
    [ProducesResponseType(StatusCodes.Status404NotFound)]                        
    public IActionResult Update(int id, ExpenseResponse request)
    {
 
        var updatedExpense = _expensesService.Update(id, request);

        if (updatedExpense == null)
            return NotFound(); 

        return Ok(updatedExpense); 
    }
}