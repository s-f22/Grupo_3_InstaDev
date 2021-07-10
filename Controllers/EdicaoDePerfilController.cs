using System.IO;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Grupo_3_InstaDev.Controllers
{
    public class EdicaoDePerfilController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            return View();
        }


        public IActionResult TrocarFoto()
        {






            return View();
        }


        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Images"),
                                               Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }




    }
}