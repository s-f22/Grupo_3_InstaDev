using System;
using System.Collections.Generic;
using System.IO;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Grupo_3_InstaDev.Controllers
{
    public class EdicaoDePerfilController : Controller
    {
        Usuario usuarioParaAcessoAosMetodosModel = new Usuario();

        [Route("EdicaoDePerfil")]
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            ViewBag.UserID = HttpContext.Session.GetString("_IdUsuario");
            ViewBag.Userimage = HttpContext.Session.GetString("_ImagemUsuario");

            ViewBag.Useremail = HttpContext.Session.GetString("_EmailUsuario");
            ViewBag.Usersenha = HttpContext.Session.GetString("_SenhaUsuario");
            ViewBag.Usernomecompleto = HttpContext.Session.GetString("_NomeCompletoUsuario");

                
                

            return View();
        }





        public IActionResult TrocarImagem(IFormCollection form)
        {
            Usuario usuarioParaReceberInfosDoFormulario = new Usuario();


            List<string> listaComConteudoDoArquivoCSV = usuarioParaAcessoAosMetodosModel.LerTodasLinhasCSV("DataBase/Usuario.csv");

            //Remove um determinado objeto da lista utilizando expressão lambda referenciada pelo Id do usuario para localiza-lo na lista
            listaComConteudoDoArquivoCSV.RemoveAll( cadaAtributoNaLinha => cadaAtributoNaLinha.Split(";")[0] == HttpContext.Session.GetString("_IdUsuario")  );

            


            // Upload Inicio
            if (form.Files.Count > 0)
            {
                var file = form.Files[0];
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Usuarios");

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", folder, file.FileName);

                using (var stream = new FileStream(path, FileMode.Create)) // stream poderia ter qualeuer outro nome
                {
                    file.CopyTo(stream);
                }

                usuarioParaReceberInfosDoFormulario.IdUsuario = int.Parse( HttpContext.Session.GetString("_IdUsuario") ) ;
                usuarioParaReceberInfosDoFormulario.Email = HttpContext.Session.GetString("_EmailUsuario");
                usuarioParaReceberInfosDoFormulario.Senha = HttpContext.Session.GetString("_SenhaUsuario");
                usuarioParaReceberInfosDoFormulario.NomeCompleto = HttpContext.Session.GetString("_NomeCompletoUsuario");
                usuarioParaReceberInfosDoFormulario.NomeDeUsuario = HttpContext.Session.GetString("_NomeDeUsuario");
                usuarioParaReceberInfosDoFormulario.ImagemUsuario = "/img/Usuarios/" + file.FileName.ToString();
                ViewBag.Userimage = usuarioParaReceberInfosDoFormulario.ImagemUsuario;
                
            }
         

            // Upload Final

            //Adiciona o novo objeto com alterações à lista utilizando o metodo Preparar(), que antes o converte para string conforme exigencia do metodo Add
            listaComConteudoDoArquivoCSV.Add( usuarioParaAcessoAosMetodosModel.Preparar(usuarioParaReceberInfosDoFormulario) );

            //Reescreve o csv utilizando a lista atualizada acima
            usuarioParaAcessoAosMetodosModel.ReescreverCSV("DataBase/Usuario.csv", listaComConteudoDoArquivoCSV);


            //usuarioParaAcessoAosMetodosModel.Alterar(usuarioParaReceberInfosDoFormulario);


            return LocalRedirect("~/EdicaoDePerfil");



        }


        public IActionResult AlterarDados(IFormCollection form) 
        {
            Usuario metodos = new Usuario();

            List<Usuario> usuarios = new List<Usuario>();

            usuarios = metodos.LerTodosUsuarios();

            Usuario u_alterado = usuarios.Find( x => x.IdUsuario == int.Parse( HttpContext.Session.GetString("_IdUsuario") ) );

            u_alterado.NomeCompleto = form[ "NomeCompleto" ];
            u_alterado.NomeDeUsuario = form[ "NomeUsuario" ];
            u_alterado.Email = form[ "Email" ];


            metodos.Alterar(u_alterado);

            return LocalRedirect("~/EdicaoDePerfil");
        }

           





    }
}