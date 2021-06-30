using System.Collections.Generic;
using System.IO;

namespace Grupo_3_InstaDev.Models
{
    public class InstaDev_G3_Base
    {
        public void CriarPastaEArquivo(string _caminho)
        {
            string pasta = _caminho.Split("/")[1];
            string arquivo = _caminho.Split("/")[2];

            if (!Directory.Exists(pasta))
            {
                Directory.CreateDirectory(pasta);
            }

            if (!File.Exists(_caminho))
            {
                File.Create(_caminho).Close();
            }

        }

        public List<string> LerTodasLinhasCSV(string _caminho)
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

        public void ReescreverCSV(string _caminho, List<string> linhas)
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