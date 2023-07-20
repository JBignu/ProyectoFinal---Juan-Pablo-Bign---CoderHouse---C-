using Microsoft.AspNetCore.Mvc;
using ProyectoFinal.Controllers.DTOS;

namespace ProyectoFinal.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {

        [HttpGet]
        public List<Usuario> GetUsuario()
        {
            return UsuarioHandler.ObtenerUsuarios();
        }

        [HttpPut]
        public void ActualizarUsuario(Usuario Usu)
        {
            UsuarioHandler.ModificarUsuario(Usu);
        }

        [HttpDelete(Name = "Eliminar usuario")]
        public void EliminarUsuario(long id)
        {
            UsuarioHandler.EliminarUsuario(id);


        }
    }
}


