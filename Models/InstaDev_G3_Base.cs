using System.Collections.Generic;
using System.IO;

namespace Grupo_3_InstaDev.Models
{
    public class InstaDev_G3_Base
    {
        public void CriarPastaEArquivo(string _caminho)     // Metodo para criar arquivo e pasta conforme caminho fornecido pela string
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

        public List<string> LerTodasLinhasCSV(string _caminho)      // Metodo para ler as linhas/conteudo do arquivo CSV, salvar este conteudo em uma lista de strings e retornar a LISTA DE STRINGS (não o arquivo csv) para acesso onde for chamada
        {
            List<string> linhas = new List<string>();

            using (StreamReader file = new StreamReader(_caminho))
            {
                string linha;

                while ((linha = file.ReadLine()) != null)
                {
                    linhas.Add(linha);
                }
            }
            return linhas;
        }

        public void ReescreverCSV(string _caminho, List<string> linhas)     // Metodo que recebe uma lista de strings, faz a leitura linha-a-linha da lista e converte os dados em um arquivo csv, conforme caminho especificado que tambem deve ser fornecido como parametro; funciona em conjunto com o metodo LerTodasLinhasCSV, acima
        {
            using (StreamWriter output = new StreamWriter(_caminho))
            {
                foreach (var item in linhas)
                {
                    output.Write(item + "\n");
                }
            }
        }
    }
}