using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoController : ControllerBase
    {
      
        [HttpGet(Name = "TraerProductos")]
        public List<Producto> TraerProductos()
        {
            return ProductoHandler.ObtenerProductos();
        }

        [HttpPut(Name = "ModificarProducto")]
        public void Actualizar(Producto Prod)
        {
            ProductoHandler.ModificarProducto(Prod);
        }

        [HttpPost(Name = "Crear Producto")]
        public void crear(Producto Prod)
        {
            ProductoHandler.CrearProducto(Prod);
        }


        [HttpDelete(Name = "Eliminar Producto")]
        public void Eliminar(long id)
        {
            ProductoHandler.EliminarProducto(id);
        }
    }
}
