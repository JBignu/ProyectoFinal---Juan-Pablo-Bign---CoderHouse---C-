using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoVendidoController : ControllerBase
    {

        [HttpGet]
        public List<ProductoVendido> getProductoVendido()
        {
            return ProductoVendidoHandler.ObtenerProductosVendidos();
        }

        [HttpPost]
        public void CrearProductoVendido(List<ProductoVendido> ProdVend)
        {
            ProductoVendidoHandler.CargarProductoVendido(ProdVend);
        }

        [HttpDelete(Name = "Eliminar Producto Vendido")]
        public void EliminarProductoVendido(long id)
        {
            ProductoVendidoHandler.EliminarProductoVendido(id);
        }

    }
}
