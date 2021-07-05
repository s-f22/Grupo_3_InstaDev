using Grupo_3_InstaDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grupo_3_InstaDev.Controllers
{

    //Rotas são endereços que ficam na URL do navegador, representando qual o controller a ser acessado após a raiz localhost5001
    [Route("Usuario")]
    public class UsuarioController : Controller  //  Herdar a SuperClasse AspNet Controller

    {


        //OBS: TODOS OS METODOS EM CONTROLLER DEVEM TER UM RETORNO, normalmente sendo para uma View



        //Instancia da classe Usuario para acessar seus metodos em model
        Usuario usuarioParaAcessoAosMetodosModel = new Usuario();



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
        public IActionResult Cadastrar( IFormCollection formulario )
        {
            Usuario usuarioParaReceberInfosDoFormulario = new Usuario();

            usuarioParaReceberInfosDoFormulario.IdUsuario = int.Parse(formulario["IdUsuario"]);
            usuarioParaReceberInfosDoFormulario.Email = (formulario["Email"]);
            usuarioParaReceberInfosDoFormulario.Senha = (formulario["Senha"]);
            usuarioParaReceberInfosDoFormulario.NomeCompleto = (formulario["NomeCompleto"]);
            usuarioParaReceberInfosDoFormulario.NomeDeUsuario = (formulario["NomeDeUsuario"]);
            usuarioParaReceberInfosDoFormulario.ImagemUsuario = (formulario["ImagemUsuario"]);

            usuarioParaAcessoAosMetodosModel.Criar(usuarioParaReceberInfosDoFormulario);

            //Recarrega a viewbag atualizada com as informações do novo usuario cadastrado
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();

            //Redireciona como retorno para a mesma pagina, pois queremos que apos o cadastro o usuario permaneça na mesma view
            return LocalRedirect("~/Usuario/Listar"); 

        }


    }
}