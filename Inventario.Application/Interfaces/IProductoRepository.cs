using Inventario.Domain;

namespace Inventario.Application.Interfaces
{
    public interface IProductoRepository
    {
        //Usaremos Task para operaciones asincronas
        Task<List<Producto>> GetAllAsync();
        Task<Producto?> GetByIdAsync(int id);
        Task <Producto> CreateAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(int id);
        Task<int> AddRangeAsync(List<Producto> productos);
    }
}