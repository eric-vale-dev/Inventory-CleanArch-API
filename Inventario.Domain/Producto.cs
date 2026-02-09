namespace Inventario.Domain
{
    public class Producto
    {
        public int Id {get; set;}
        public string Nombre {get; set;} = string.Empty;
        public string Descripcion {get; set;} = string.Empty;
        public decimal Precio {get; set;}
        public int Stock{get; set;}

        //Propiedad de auditoria
        public DateTime FechaCreacion {get; set;} = DateTime.UtcNow;
    }
}