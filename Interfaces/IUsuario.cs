using System.Collections.Generic;
using Grupo_3_InstaDev.Models;

namespace Grupo_3_InstaDev.Interfaces
{
    public interface IUsuario
    {
         void Criar(Usuario _novo);     //  Recebe um objeto Usuario para ser criado
         List<Usuario> LerTodosUsuarios();  //  Lê todos os usuarios e retorna uma lista de Usuarios
         void Alterar(Usuario _paraAlterar);    //  Recebe um usuario para ser alterado
         void Deletar(int _idDoUsuario);    //  Deleta um usuario atraves do Id informado/passado como parametro
    }
}