using System;
using System.Collections.Generic;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{
    public class FeedController : Controller
    {
        Usuario U = new Usuario();

        Post P = new Post();

        List<Usuario> ListaUsuarios = new List<Usuario>();



        public IActionResult Index()
        {

            //ViewBag é um espaço na memoria semelhante a uma variavel que irá transportar a lista de Usuarios acessada e disponibiliza-la como visivel na View
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            ViewBag.Userimage = HttpContext.Session.GetString("_ImagemUsuario");

            ListaUsuarios = U.LerTodosUsuarios();
            ViewBag.LU = ListaUsuarios;
            
            
            return View();
        }

        public IActionResult Postar(IFormCollection form)
        {
            
            
            //PEGAR TUDO NO METODO TROCAR IMAGEM
            
            
            
            Post novoPost = new Post();

            Random gerarIDpost = new Random();


            novoPost.PostID = gerarIDpost.Next(1, 1000);
            novoPost.TextoPost = form[ "TextoPost" ];
            novoPost.ImagemPost = form[ "ImagemPost" ];
            


            return LocalRedirect("~/Feed");
        }


    }
}