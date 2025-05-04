using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace SuaApi.Controllers
{
    public class ProdutosController 
    {
       // init
        public virtual Task<IActionResult> GetProduto(int id)
        {
         
            return Task.FromResult<IActionResult>(new OkObjectResult(new { Id = id, Nome = "Produto Teste" }));
        }
    }
}

namespace ProjetoTestesController.Controllers
{
    public class ProdutosControllerTests
    {
        private readonly Mock<ILogger<SuaApi.Controllers.ProdutosController>> _mockLogger;
        private readonly SuaApi.Controllers.ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _mockLogger = new Mock<ILogger<SuaApi.Controllers.ProdutosController>>();
            _controller = new SuaApi.Controllers.ProdutosController();
        }

        [Fact]
        public async Task GetProduto_ExistingId_ReturnsOkResult()
        {
            // Arrange
            int testId = 1;

            // Act
            var result = await _controller.GetProduto(testId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            var produto = okResult.Value as dynamic; 
            Assert.Equal(testId, produto?.Id);
            Assert.Equal("Produto Teste", produto?.Nome);
        }

    }
}