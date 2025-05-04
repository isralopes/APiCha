using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProject.Controllers;
using ApiProject.Models;
using ApiProject.Data; 

namespace ApiProject.Tests.Controllers
{
    public class ProdutosControllerTests
    {
        private readonly Mock<ILogger<ProdutosController>> _mockLogger;
        private readonly Mock<AppDbContext> _mockContext; // Mock do seu contexto de banco de dados
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            // Inicialização comum para os testes
            _mockLogger = new Mock<ILogger<ProdutosController>>();
            _mockContext = new Mock<AppDbContext>(); 

          
            _controller = new ProdutosController(_mockContext.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetProdutos_ReturnsOkResult()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Preco = 10.00 },
                new Produto { Id = 2, Nome = "Produto 2", Preco = 20.00 }
            };

            
            var mockDbSet = new Mock<Microsoft.EntityFrameworkCore.DbSet<Produto>>();
            mockDbSet.As<IQueryable<Produto>>().Setup(m => m.Provider).Returns(produtos.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Produto>>().Setup(m => m.Expression).Returns(produtos.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Produto>>().Setup(m => m.ElementType).Returns(produtos.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Produto>>().Setup(m => m.GetEnumerator()).Returns(produtos.GetEnumerator());

            _mockContext.Setup(c => c.Produtos).Returns(mockDbSet.Object);

            // Act
            var result = await _controller.GetProdutos();

            // Assert
            Assert.IsType<ActionResult<IEnumerable<Produto>>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnedProdutos = okResult.Value as IEnumerable<Produto>;
            Assert.NotNull(returnedProdutos);
            Assert.Equal(2, returnedProdutos.Count());
        }

        [Fact]
        public async Task GetProduto_ExistingId_ReturnsOkResultWithProduto()
        {
            // Arrange
            int testId = 1;
            var produto = new Produto { Id = testId, Nome = "Produto Teste", Preco = 15.00 };

            // Configura o mock do contexto para retornar o produto para o ID especificado
            _mockContext.Setup(c => c.Produtos.FindAsync(testId)).ReturnsAsync(produto);

            // Act
            var result = await _controller.GetProduto(testId);

            // Assert
            Assert.IsType<ActionResult<Produto>>(result);
            var okResult = result.Result as OkObjectResult;
            Assert.NotNull(okResult);
            var returnedProduto = okResult.Value as Produto;
            Assert.NotNull(returnedProduto);
            Assert.Equal(testId, returnedProduto.Id);
        }

        [Fact]
        public async Task GetProduto_NonExistingId_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistingId = 99;
            _mockContext.Setup(c => c.Produtos.FindAsync(nonExistingId)).ReturnsAsync((Produto)null);

            // Act
            var result = await _controller.GetProduto(nonExistingId);

            // Assert
            Assert.IsType<ActionResult<Produto>>(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }


    }
}