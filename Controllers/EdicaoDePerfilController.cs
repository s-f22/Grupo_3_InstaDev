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
            return View();
        }





        public IActionResult TrocarImagem(IFormCollection form)
        {
            Usuario usuarioParaReceberInfosDoFormulario = new Usuario();


            List<string> listaComConteudoDoArquivoCSV = usuarioParaAcessoAosMetodosModel.LerTodasLinhasCSV("DataBase/Usuario.csv");

            //Remove um determinado objeto da lista utilizando expressão lambda referenciada pelo Id do usuario para localiza-lo na lista
            listaComConteudoDoArquivoCSV.RemoveAll( cadaAtributoNaLinha => cadaAtributoNaLinha.Split(";")[0] == int.Parse(ViewBag.UserID)  );

            

            // List<string> UsuarioCSV = usuarioParaAcessoAosMetodosModel.LerTodasLinhasCSV("DataBase/Usuario.csv");

            // var alterar = UsuarioCSV.Find(x =>
            //   x.Split(";")[0] == ViewBag.UserID 
            //     );

            // if (alterar != null)
            // {
            //     HttpContext.Session.SetString("_NomeDeUsuario", alterar.Split(";")[4]);
            //     HttpContext.Session.SetString("_ImagemUsuario", alterar.Split(";")[5]);
            //     return LocalRedirect("~/Feed");
            // }






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

                usuarioParaReceberInfosDoFormulario.ImagemUsuario = "~/img/Usuarios/" + file.FileName.ToString();
            }
            // else
            // {
            //     usuarioParaReceberInfosDoFormulario.ImagemUsuario = "wwwroot/img/Usuarios/padrao.png";
            // }

            // Upload Final

            //Adiciona o novo objeto com alterações à lista utilizando o metodo Preparar(), que antes o converte para string conforme exigencia do metodo Add
            listaComConteudoDoArquivoCSV.Add( usuarioParaAcessoAosMetodosModel.Preparar(usuarioParaReceberInfosDoFormulario) );

            //Reescreve o csv utilizando a lista atualizada acima
            usuarioParaAcessoAosMetodosModel.ReescreverCSV("DataBase/Usuario.csv", listaComConteudoDoArquivoCSV);


            //usuarioParaAcessoAosMetodosModel.Alterar(usuarioParaReceberInfosDoFormulario);


            return LocalRedirect("~/EdicaoDePerfil");



        }







    }
}