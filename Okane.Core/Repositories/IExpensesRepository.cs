using Okane.Core.Entities;

namespace Okane.Core.Repositories;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> All();
    IEnumerable<Expense> FindByCategory(string category);
}