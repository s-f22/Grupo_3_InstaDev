using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;

namespace Grupo_3_InstaDev.Controllers
{
    public class HomeController : Controller
    {
        Usuario usuarioParaAcessoAosMetodosModel = new Usuario();

        [TempData]
        public string Mensagem { get; set; }


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            ViewBag.Username = HttpContext.Session.GetString("_NomeDeUsuario");
            ViewBag.ImagemUsuario = HttpContext.Session.GetString("_ImagemUsuario");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Listar")]
        public IActionResult Listar()
        {

            //ViewBag é um espaço na memoria semelhante a uma variavel que irá transportar a lista de Usuarios acessada e disponibiliza-la como visivel na View
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();
            return View();
        }

        //--------------------------------------------------------------------------------------


        //IFormCollection é um tipo de classe AspNet capaz de receber dados de formularios html
        [Route("Cadastrar")]
        public IActionResult Cadastrar(IFormCollection formulario)
        {
            Usuario usuarioParaReceberInfosDoFormulario = new Usuario();


            Random gerarID = new Random();

            usuarioParaReceberInfosDoFormulario.IdUsuario = gerarID.Next(1, 1000);
            usuarioParaReceberInfosDoFormulario.Email = (formulario["Email"]);
            usuarioParaReceberInfosDoFormulario.Senha = (formulario["Senha"]);
            usuarioParaReceberInfosDoFormulario.NomeCompleto = (formulario["NomeCompleto"]);
            usuarioParaReceberInfosDoFormulario.NomeDeUsuario = (formulario["NomeDeUsuario"]);
            usuarioParaReceberInfosDoFormulario.ImagemUsuario = "null"; 



            usuarioParaAcessoAosMetodosModel.Criar(usuarioParaReceberInfosDoFormulario);

            //Recarrega a viewbag atualizada com as informações do novo usuario cadastrado
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();

            //Redireciona como retorno para a mesma pagina, pois queremos que apos o cadastro o usuario permaneça na mesma view
            //return LocalRedirect("~/CadastroLoginController/Listar");
            return LocalRedirect("~/");

        }


        //--------------------------------------------------------------------------------------


        // O id abaixo será fornecido como parametro de identificação do usuario a ser deletado, e é definido no index de usuario da View
        // Apalavra Excluir na rota abaixo não seria obrigatoria. Ela só é util para identificar o que está ocorrendo quando o usuario posicionar o cursor sobre o botão excluir, podendo então visualizar que ocorrerá uma exclusão, seguida pelo ID do que será excluido
        [Route("Excluir/{id}")]
        public IActionResult Excluir(int idParaExcluir)
        {
            usuarioParaAcessoAosMetodosModel.Deletar(idParaExcluir);
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();
            return LocalRedirect("~/CadastroLoginController/Listar");
        }


        //---------------------------------------------------------------------------------------



        [Route("Login")]
        public IActionResult Logar(IFormCollection form)
        {

            List<string> UsuarioCSV = usuarioParaAcessoAosMetodosModel.LerTodasLinhasCSV("DataBase/Usuario.csv");

            var logado = UsuarioCSV.Find( x =>
               x.Split(";")[1] == form["Email"] &&
               x.Split(";")[2] == form["Senha"]
                );

            if (logado != null)
            {
                HttpContext.Session.SetString("_NomeDeUsuario", logado.Split(";")[4]);
                HttpContext.Session.SetString("_ImagemUsuario", logado.Split(";")[5]);
                return LocalRedirect("~/Feed");
            }
            Mensagem = "Dados incorretos, tente novamente.";
            return LocalRedirect("~/");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("_UserName");
            return LocalRedirect("~/");
        }
    }
}

    

