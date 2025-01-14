using Okane.Contracts;
using Okane.Core.Validations;

namespace Okane.Tests.Validations;

public class CreateExpenseRequestValidatorTests
{
    private readonly IValidator<CreateExpenseRequest> _validator;

    public CreateExpenseRequestValidatorTests() => 
        _validator = new DataAnnotationsValidator();

    [Fact]
    public void Valid()
    {
        var result = _validator.Validate(new CreateExpenseRequest
        {
            Amount = 10,
            CategoryName = "Food"
        });
        
        Assert.Empty(result);
    }
    
    [Fact]
    public void CategoryTooLong()
    {
        var result = _validator.Validate(new CreateExpenseRequest
        {
            Amount = 10,
            CategoryName = string.Concat(Enumerable.Repeat("x", 100))
        });
        
        var (property, errors) = Assert.Single(result);
        Assert.Equal(nameof(CreateExpenseRequest.CategoryName), property);

        var error = Assert.Single(errors);
        Assert.Equal($"The field {nameof(CreateExpenseRequest.CategoryName)} must be a string or array type with a maximum length of '80'.", error);
    }
    
    [Fact]
    public void InvalidInvoiceUrl()
    {
        var result = _validator.Validate(new CreateExpenseRequest
        {
            Amount = 10,
            CategoryName = "Cat",
            InvoiceUrl = "http//invoices.com/invoice1"
        });
        
        var (property, errors) = Assert.Single(result);
        Assert.Equal(nameof(ExpenseResponse.InvoiceUrl), property);

        var error = Assert.Single(errors);
        Assert.Equal("The InvoiceUrl field is not a valid fully-qualified http, https, or ftp URL.", error);
    }
}