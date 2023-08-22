namespace Okane.Contracts;

public interface IExpensesService
{
    ExpenseResponse Register(CreateExpenseRequest request);
    IEnumerable<ExpenseResponse> RetrieveAll();
    IEnumerable<ExpenseResponse> RetrieveByCategory(string category);
}