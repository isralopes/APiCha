using Microsoft.AspNetCore.Mvc;
using ApiProject.Models;
using ApiProject.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProdutosController> _logger;

        public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            try
            {
                _logger.LogInformation("Obtendo todos os produtos");
                return await _context.Produtos.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar produtos");
                return StatusCode(500, "Erro interno ao buscar produtos");
            }
        }

        /// <summary>
        /// Obtém um produto específico por ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            try
            {
                _logger.LogInformation($"Buscando produto com ID {id}");
                var produto = await _context.Produtos.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == id);

                return produto == null ? NotFound() : produto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar produto com ID {id}");
                return StatusCode(500, "Erro interno ao buscar produto");
            }
        }

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Produto>> PostProduto([FromBody] Produto produto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Modelo inválido recebido");
                    return BadRequest(ModelState);
                }

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Produto criado com ID {produto.Id}");
                return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar produto");
                return StatusCode(500, "Erro interno ao criar produto");
            }
        }

        /// <summary>
        /// Atualiza um produto existente
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProduto(int id, [FromBody] Produto produto)
        {
            try
            {
                if (id != produto.Id)
                {
                    _logger.LogWarning($"ID mismatch: {id} vs {produto.Id}");
                    return BadRequest("ID do produto não corresponde");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Entry(produto).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Produto com ID {id} atualizado");
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                _logger.LogError(ex, $"Erro de concorrência ao atualizar produto ID {id}");
                return StatusCode(500, "Erro de concorrência ao atualizar produto");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar produto com ID {id}");
                return StatusCode(500, "Erro interno ao atualizar produto");
            }
        }

        /// <summary>
        /// Remove um produto
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduto(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                if (produto == null)
                {
                    _logger.LogWarning($"Produto com ID {id} não encontrado para exclusão");
                    return NotFound();
                }

                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Produto com ID {id} removido");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar produto com ID {id}");
                return StatusCode(500, "Erro interno ao deletar produto");
            }
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}