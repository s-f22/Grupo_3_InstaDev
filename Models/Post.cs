using System.Collections.Generic;
using System.IO;

namespace Grupo_3_InstaDev.Models
{
    public class Post : InstaDev_G3_Base
    {

        public string TextoPost { get; set; }
        public string ImagemPost { get; set; }
        public int PostID { get; set; }



        private const string CAMINHO = "DataBase/Post.csv";



        public Post()
        {
            CriarPastaEArquivo(CAMINHO);
        }

        private string Preparar(Post aConverter)
        {
            return $"{aConverter.PostID};{aConverter.TextoPost};{aConverter.ImagemPost}";
        }


        public void Criar(Post _novo)
        {
            string[] linha = { Preparar(_novo) };

            File.AppendAllLines(CAMINHO, linha);
        }



        public List<Post> LerTodosPosts()
        {
            List<Post> listaDePosts = new List<Post>();

            string[] arrayDeLinhas = File.ReadAllLines(CAMINHO);

            foreach (var cadaLinha in arrayDeLinhas)
            {
                string[] atributosEmCadaLinha = cadaLinha.Split(";");

                Post cadaPostDaLista = new Post();

                cadaPostDaLista.PostID = int.Parse(atributosEmCadaLinha[0]);
                cadaPostDaLista.TextoPost = atributosEmCadaLinha[1];
                cadaPostDaLista.ImagemPost = atributosEmCadaLinha[2];

                listaDePosts.Add(cadaPostDaLista);
            }

            return listaDePosts;
        }




        public void Deletar(int idDoUsuarioADeletar)
        {
            List<string> listaComConteudoDoArquivoCSV = LerTodasLinhasCSV(CAMINHO);

            
            listaComConteudoDoArquivoCSV.RemoveAll(cadaAtributoNaLinha => cadaAtributoNaLinha.Split(";")[0] == idDoUsuarioADeletar.ToString());

            ReescreverCSV(CAMINHO, listaComConteudoDoArquivoCSV);
        }






    }
}