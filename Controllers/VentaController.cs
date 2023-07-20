using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VentaController : ControllerBase
    {

        [HttpGet]
        public List<Venta> GetVenta()
        {
            return VentaHandler.ObtenerVentas();
        }

        [HttpPost]
        public void ActualizarVentas(Venta Vent)
        {
            VentaHandler.CargarVenta(Vent);
        }

        [HttpDelete(Name = "Eliminar venta")]
        public void EliminarVenta(long id)
        {
            VentaHandler.EliminarVenta(id);
        }
    }
}
    

