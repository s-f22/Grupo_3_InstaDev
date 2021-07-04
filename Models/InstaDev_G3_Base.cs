
//------------------------------------SUPERCLASSE COM METEDOS QUE SERÃO HERDADOS--------------------

using System.Collections.Generic;
using System.IO;

namespace Grupo_3_InstaDev.Models
{

    public class InstaDev_G3_Base
    {



        // Metodo para criar arquivo e pasta conforme caminho fornecido pela string
        public void CriarPastaEArquivo(string _caminho)
        {
            string pasta = _caminho.Split("/")[0];
            string arquivo = _caminho.Split("/")[1];

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            if (!File.Exists(_caminho))
            {
                File.Create(_caminho).Close();
            }

        }





        // Metodo utilizado como parte do processo de alteração/atualização dos CRUDs. *Ver comentários no metodo Alterar() da classe Usuario.
        // Metodo para ler as linhas/conteudo do arquivo CSV, salvar este conteudo em uma lista de strings e retornar a LISTA DE STRINGS (não o arquivo csv) para local onde for chamado
        public List<string> LerTodasLinhasCSV(string _caminhoDoArquivo)
        {
            List<string> listaComConteudoDasLinhas = new List<string>();

            //using é um recurso/biblioteca que abre o arquivo para leitura, fechando-o assim que sua utilização for concluída. Trabalha em conjunto com as bibliotecas Stream
            using (StreamReader arquivoTemporario = new StreamReader(_caminhoDoArquivo))
            {
                string conteudoDeCadaLinha;

                while ((conteudoDeCadaLinha = arquivoTemporario.ReadLine()) != null)
                {
                    listaComConteudoDasLinhas.Add(conteudoDeCadaLinha);
                }
            }

            return listaComConteudoDasLinhas;
        }





        // Metodo utilizado como parte do processo de alteração/atualização dos CRUDs. *Ver comentários no metodo Alterar() da classe Usuario.
        // Metodo que recebe uma lista de strings, faz a leitura linha-a-linha da lista e converte os dados em um arquivo csv, conforme caminho especificado que tambem deve ser fornecido como parametro; funciona em conjunto com o metodo LerTodasLinhasCSV, acima
        public void ReescreverCSV(string _caminhoDoArquivo, List<string> listaComConteudoEmLinhas)
        {
            //using é um recurso/biblioteca que abre o arquivo para leitura, fechando-o assim que sua utilização for concluída. Trabalha em conjunto com as bibliotecas Stream
            using (StreamWriter reescreverDadosTemporarios = new StreamWriter(_caminhoDoArquivo))
            {
                foreach (var cadaLinha in listaComConteudoEmLinhas)
                {
                    reescreverDadosTemporarios.Write(cadaLinha + "\n");
                }
            }
        }
    }
}