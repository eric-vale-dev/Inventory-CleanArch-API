using Microsoft.EntityFrameworkCore;
using Inventario.Domain;
using Inventario.Application.Interfaces;
using Inventario.Infrastructure.Persistence;

namespace Inventario.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;
        
        // Inyección de dependencias: pedimos el DbContext
        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Producto>> GetAllAsync()
        {
            // Se traduce a: SELECT * FROM Productos
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> GetByIdAsync(int id)
        {
            // Se traduce a: SSELECT * FROM Productos WHERE Id = @id LIMIT 1
            return await _context.Productos.FindAsync(id);
        }

        public async Task<Producto> CreateAsync(Producto producto)
        {
            // Solo lo añade a la memoria del contexto
            _context.Productos.Add(producto);
            // Aquí es donde realmente se envía el INSERT a la base de datos
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            //  Buscamos si hay alguna entidad con ese ID ya cargada en la memoria local de EF
            var local = _context.Productos.Local
                .FirstOrDefault(p => p.Id == producto.Id);

            // Si existe, la "despegamos" (Detached) para que deje de estorbar
            if (local != null)
            {
                _context.Entry(local).State = EntityState.Detached;
            }

            _context.Entry(producto).State = EntityState.Modified;
            
            // Guardamos cambios
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<int> AddRangeAsync(List<Producto> productos)
        {
            // EF Core es muy listo: AddRange prepara todo el SQL de golpe
            await _context.Productos.AddRangeAsync(productos);
            
            // Guarda todos los cambios en una sola transacción
            return await _context.SaveChangesAsync();
        }
    }
}