using Xunit;
using FinanceTrackerApp;
using System;

public class BankAccountTests
{
    [Fact]
    public void BankAccount_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Основной счет";
        var balance = 1000.0;

        // Act
        var account = new BankAccount(id, name, balance);

        // Assert
        Assert.Equal(id, account.Id);
        Assert.Equal(name, account.Name);
        Assert.Equal(balance, account.Balance);
    }

    [Fact]
    public void AddBalance_ShouldIncreaseBalance()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Основной счет", 1000.0);

        // Act
        account.AddBalance(500.0);

        // Assert
        Assert.Equal(1500.0, account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldDecreaseBalance_WhenSufficientFunds()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Основной счет", 1000.0);

        // Act
        account.Withdraw(500.0);

        // Assert
        Assert.Equal(500.0, account.Balance);
    }

    [Fact]
    public void Withdraw_ShouldThrowException_WhenInsufficientFunds()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Основной счет", 1000.0);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => account.Withdraw(1500.0));
        Assert.Equal("Недостаточно средств на счете.", exception.Message);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName()
    {
        // Arrange
        var account = new BankAccount(Guid.NewGuid(), "Основной счет", 1000.0);

        // Act
        account.ChangeName("Новый счет");

        // Assert
        Assert.Equal("Новый счет", account.Name);
    }
}