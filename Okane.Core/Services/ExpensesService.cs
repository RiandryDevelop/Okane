using Okane.Contracts;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Core.Services;

public class ExpensesService : IExpensesService
{
    private readonly IExpensesRepository _expenses;

    public ExpensesService(IExpensesRepository expenses) =>
        _expenses = expenses;

    public ExpenseResponse Register(CreateExpenseRequest request)
    {
        var expense = new Expense
        {
            Category = request.Category,
            Amount = request.Amount
        };
        
        _expenses.Add(expense);
        
        return new ExpenseResponse
        {
            Id = expense.Id,
            Category = expense.Category,
            Amount = expense.Amount
        };
    }

    public IEnumerable<ExpenseResponse> RetrieveAll() => 
        _expenses.All().Select(expense => new ExpenseResponse
        {
            Amount = expense.Amount,
            Category = expense.Category
        });

    public IEnumerable<ExpenseResponse> RetrieveByCategory(string category)
    {
        return _expenses
            .FindByCategory(category)
            .Select(expense => new ExpenseResponse
            {
                Amount = expense.Amount,
                Category = expense.Category
            });
    }
}