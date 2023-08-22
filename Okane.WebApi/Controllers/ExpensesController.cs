using Microsoft.AspNetCore.Mvc;
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
    public ExpenseResponse Post(CreateExpenseRequest request) => 
        _expensesService.Register(request);
    
    [HttpGet]
    public IEnumerable<ExpenseResponse> Get() => 
        _expensesService.RetrieveAll();

    [HttpGet("{category}")]
    public ActionResult<IEnumerable<ExpenseResponse>> RetrieveByCategory(string category)
    {
        var expenses = _expensesService.RetrieveByCategory(category);

        if (expenses == null || !expenses.Any())
        {
            return NotFound(); // Return a 404 Not Found response if no expenses are found for the category.
        }

        return Ok(expenses); // Return the found expenses as a successful response.
    }

}