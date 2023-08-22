using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.InMemory;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private readonly IList<Expense> _expenses;
    private int _nextId = 1;
    public InMemoryExpensesRepository() : this(new List<Expense>())
    {
    }

    private InMemoryExpensesRepository(IList<Expense> expenses) =>
        _expenses = expenses;

    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> All() => _expenses;

    public IEnumerable<Expense> FindByCategory(string category)
    {
        return _expenses.Where(expense => expense.Category == category);
    }
}