using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{
    public class FeedController : Controller
    {
        Usuario U = new Usuario();
        public IActionResult Index()
        {

            //ViewBag é um espaço na memoria semelhante a uma variavel que irá transportar a lista de Usuarios acessada e disponibiliza-la como visivel na View
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            ViewBag.Userimage = HttpContext.Session.GetString("_ImagemUsuario");
            
            return View();
        }

        public IActionResult ListarUsuarios()
        {
            ViewBag.LU = U.LerTodosUsuarios();
            return View();
        }


    }
}