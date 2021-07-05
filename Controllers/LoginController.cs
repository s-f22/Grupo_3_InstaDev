using System.Collections.Generic;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{
    public class LoginController : Controller{
        Usuario usuarioModel = new Usuario();
        public IActionResult Logar(IFormCollection form){

            List<string> UsuarioCSV = usuarioModel.LerTodasLinhasCSV("Database/Usuario.csv");

            var logado = UsuarioCSV.Find(x=>
            x.Split(";")[0] == form["Email"] &&
            x.Split(";")[1] == form["Senha"] 
            );

            if (logado != null){
                HttpContext.Session.SetString("_IdUsuario", logado.Split(";")[4]);
                return LocalRedirect("~/Feed");
            }
            Mensagem = "Dados incorretos, tente novamente";
            return LocalRedirect("~/")
        }
    }
}