using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{
    public class FeedController : Controller
    {
        public IActionResult Index()
        {

            //ViewBag é um espaço na memoria semelhante a uma variavel que irá transportar a lista de Usuarios acessada e disponibiliza-la como visivel na View
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            return View();
        }
    }
}