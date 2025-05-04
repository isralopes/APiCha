namespace Projeto_api.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        int a = 5;
        int b = 10;

        // Act
        int resultado = a + b;

        // Assert
        Assert.Equal(15, resultado);
    }
}