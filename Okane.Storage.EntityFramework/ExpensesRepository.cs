using Microsoft.EntityFrameworkCore;
using Okane.Core.Entities;
using Okane.Core.Repositories;

namespace Okane.Storage.EntityFramework;

public class ExpensesRepository : IExpensesRepository
{
    private readonly OkaneDbContext _db;

    public ExpensesRepository(OkaneDbContext db) => _db = db;
        
    public void Add(Expense expense)
    {
        _db.Expenses.Add(expense);
        _db.SaveChanges();
    }

    public IEnumerable<Expense> All() => 
        _db.Expenses;

    public IEnumerable<Expense> ByCategory(string category) => 
        _db.Expenses.Where(expense => expense.Category.Name == category)
            .Include(expense => expense.Category);

    public Expense? ById(int id) => 
        _db.Expenses.FirstOrDefault(expense => expense.Id == id);


    public bool Delete(int id)
    {
        var expenseToDelete = ById(id);

        if (expenseToDelete == null)
            return false; // Expense not found, deletion failed

        _db.Expenses.Remove(expenseToDelete);
        _db.SaveChanges();
        return true; // Deletion successful
    }

    public Expense? Update(int id, Expense updatedExpense)
    {
        var existingExpense = ById(id);

        if (existingExpense == null)
            return null; 

        // Update the existing expense with the new data
        existingExpense.Name = updatedExpense.Name;
        existingExpense.Amount = updatedExpense.Amount;
        existingExpense.CategoryId = updatedExpense.CategoryId;
        existingExpense.Date = updatedExpense.Date;

        _db.SaveChanges();
        return existingExpense; // Updated expense
    }
}