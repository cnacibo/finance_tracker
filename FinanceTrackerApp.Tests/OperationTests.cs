using Xunit;
using FinanceTrackerApp;
using System;

public class OperationTests
{
    [Fact]
    public void Operation_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var type = true; // add
        var bankAccountId = Guid.NewGuid();
        var amount = 1000.0;
        var date = DateTime.Now;
        var description = "Зарплата";
        var categoryId = Guid.NewGuid();

        // Act
        var operation = new Operation(id, type, bankAccountId, amount, date, description, categoryId);

        // Assert
        Assert.Equal(id, operation.Id);
        Assert.Equal(type, operation.Type);
        Assert.Equal(bankAccountId, operation.BankAccountId);
        Assert.Equal(amount, operation.Amount);
        Assert.Equal(date, operation.Date);
        Assert.Equal(description, operation.Description);
        Assert.Equal(categoryId, operation.CategoryId);
    }

    [Fact]
    public void Operation_Constructor_ShouldThrowException_WhenAmountIsNotPositive()
    {
        // Arrange
        var id = Guid.NewGuid();
        var type = true; // add
        var bankAccountId = Guid.NewGuid();
        var amount = -100.0; // недопустимое значение
        var date = DateTime.Now;
        var description = "Зарплата";
        var categoryId = Guid.NewGuid();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Operation(id, type, bankAccountId, amount, date, description, categoryId));
        Assert.Equal("Сумма операции должна быть положительной.", exception.Message);
    }

    [Fact]
    public void ChangeDescription_ShouldUpdateDescription()
    {
        // Arrange
        var operation = new Operation(Guid.NewGuid(), true, Guid.NewGuid(), 1000.0, DateTime.Now, "Зарплата", Guid.NewGuid());

        // Act
        operation.ChangeDescription("Премия");

        // Assert
        Assert.Equal("Премия", operation.Description);
    }
}