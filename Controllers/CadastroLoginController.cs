using System;
using System.Collections.Generic;
using System.IO;
using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{

    //Rotas são endereços que ficam na URL do navegador, representando qual o controller a ser acessado após a raiz localhost5001
    [Route("CadastroLoginController")]
    public class CadastroLoginController : Controller  //  Herdar a SuperClasse AspNet Controller

    {


        //OBS: TODOS OS METODOS EM CONTROLLER DEVEM TER UM RETORNO, normalmente sendo para uma View



        //Instancia da classe Usuario para acessar seus metodos em model
        Usuario usuarioParaAcessoAosMetodosModel = new Usuario();

        public string Mensagem { get; set; }





        //---------------------------METODOS DA CLASSE-----------------------------------------

        //IActionResult é um tipo de retorno do método Index, especifico da classe Controller. Metodo padrão que retorna para a pagina principal correspondente a Usuario (neste caso)
        [Route("Listar")]
        public IActionResult Index()
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
            // usuarioParaReceberInfosDoFormulario.ImagemUsuario = (formulario["ImagemUsuario"]); - linha substituida pelo procedimento abaixo de upload de imagens



            usuarioParaAcessoAosMetodosModel.Criar(usuarioParaReceberInfosDoFormulario);

            //Recarrega a viewbag atualizada com as informações do novo usuario cadastrado
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();

            //Redireciona como retorno para a mesma pagina, pois queremos que apos o cadastro o usuario permaneça na mesma view
            return LocalRedirect("~/CadastroLoginController/Listar");
            //return View();

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
                HttpContext.Session.SetString("_IdUsuario", logado.Split(";")[4]);
                return LocalRedirect("~/Feed");
            }
            Mensagem = "Dados incorretos, tente novamente";
            return LocalRedirect("~/CadastroLoginController/Login");
        }


    }
}