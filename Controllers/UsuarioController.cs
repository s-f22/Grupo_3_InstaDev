using System.IO;
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
            // usuarioParaReceberInfosDoFormulario.ImagemUsuario = (formulario["ImagemUsuario"]); - linha substituida pelo procedimento abaixo de upload de imagens


            // INICIO DO UPLOAD DE IMAGEM --------------------------------------


            // Verifica se o tamanho do arquivo carregado no formulario é maior que 0, ou seja, se existe alguma imagem
            if (formulario.Files.Count > 0) 
            {
                //os arquivos são armazenados por padrão em um array, por isso os [];
                //todos os arquivos estaticos (no caso, imagens, devem ser salvos na pasta local wwwroot). O metodo Path.Combine() da classe System.IO gerencia a combinação de locais internos à pasta do projeto ASP (wwwroot...) com a estrutura local do computador (C:/...)
                var arquivoDeImagem = formulario.Files[0];
                var pastaDeImagens = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Usuarios");

                //verifica existencia da pasta onde as imagens serão salvas e cria, caso não exista
                if ( ! Directory.Exists(pastaDeImagens) )
                {
                    Directory.CreateDirectory(pastaDeImagens);
                }

                //atribui o nome original do arquivo de imagem já com sua localização
                var caminhoDaImagem = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", pastaDeImagens, arquivoDeImagem.FileName);

                //FileStream cria um novo arquivo. Exige o caminho completo do arquivo e um modo de manipulação (Filemode), no caso, para criação do arquivo
                using (var variavelTemporaria = new FileStream(caminhoDaImagem, FileMode.Create))
                {
                    //o arquivoDeImagem definido acima servirá como base para uma cópia que será feita atraves do filestream
                    arquivoDeImagem.CopyTo(variavelTemporaria); //23:42
                }
            }

            // FINAL DO UPLOAD DE IMAGEM ---------------------------------------


            usuarioParaAcessoAosMetodosModel.Criar(usuarioParaReceberInfosDoFormulario);

            //Recarrega a viewbag atualizada com as informações do novo usuario cadastrado
            ViewBag.Usuarios = usuarioParaAcessoAosMetodosModel.LerTodosUsuarios();

            //Redireciona como retorno para a mesma pagina, pois queremos que apos o cadastro o usuario permaneça na mesma view
            return LocalRedirect("~/Usuario/Listar"); 

        }

    }
}