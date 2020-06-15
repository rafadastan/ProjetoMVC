using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Contracts
{
    //<T> tipo de dado genérico (não-definido)
    public interface IBaseRepository<T>
    {
        //métodos abstratos
        void Inserir(T obj);
        void Alterar(T obj);
        void Excluir(T obj);
        List<T> Consultar();
        T ObterPorId(int id);
    }
}
