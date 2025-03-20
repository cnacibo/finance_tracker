using Xunit;
using FinanceTrackerApp;
using System;

public class CategoryTests
{
    [Fact]
    public void Category_Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var type = true; // gain
        var name = "Зарплата";

        // Act
        var category = new Category(id, type, name);

        // Assert
        Assert.Equal(id, category.Id);
        Assert.Equal(type, category.Type);
        Assert.Equal(name, category.Name);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName()
    {
        // Arrange
        var category = new Category(Guid.NewGuid(), true, "Зарплата");

        // Act
        category.ChangeName("Премия");

        // Assert
        Assert.Equal("Премия", category.Name);
    }
}