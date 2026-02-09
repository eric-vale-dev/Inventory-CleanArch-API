using Microsoft.AspNetCore.Mvc;
using Inventario.Application.Interfaces;
using Inventario.Domain;

namespace Inventario.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoRepository _repository;

        // Se inyecta el repositorio automáticamente. Puede usar PostgreSQL, SQL Server, etc.
        public ProductosController(IProductoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/productos - Devuelve todos los productos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var productos = await _repository.GetAllAsync();
            return Ok(productos);
        }

        // GET: api/productos/5 - Devuelve un producto específico por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            
            if (producto == null)
                return NotFound();
                
            return Ok(producto);
        }

        // POST: api/productos - Crea un nuevo producto
        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            // Valida automáticamente las Data Annotations del modelo Producto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevoProducto = await _repository.CreateAsync(producto);
            
            // Retorna 201 Created con la URL del nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = nuevoProducto.Id }, nuevoProducto);
        }
        
        // DELETE: api/productos/5 - Elimina un producto por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
             await _repository.DeleteAsync(id);
             return NoContent();
        }

        // PUT: api/productos/5 - Actualiza completamente un producto
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Producto producto)
        {
            // Validación: el ID en la URL debe coincidir con el ID en el cuerpo
            if (id != producto.Id)
                return BadRequest("El ID de la URL no coincide con el del cuerpo.");

            // Verificar que el producto exista antes de actualizar
            var productoExistente = await _repository.GetByIdAsync(id);
            if (productoExistente == null)
                return NotFound($"No existe un producto con el ID {id}");

            await _repository.UpdateAsync(producto);
            return NoContent();
        }

        // POST: api/productos/masivo - Crea múltiples productos en una sola operación
        [HttpPost("masivo")]
        public async Task<IActionResult> CreateMany(List<Producto> productos)
        {
            if (productos == null || productos.Count == 0)
                return BadRequest("No enviaste ningún producto.");

            var cantidadGuardada = await _repository.AddRangeAsync(productos);
            return Ok($"Se guardaron {cantidadGuardada} productos exitosamente.");
        }
    }
}