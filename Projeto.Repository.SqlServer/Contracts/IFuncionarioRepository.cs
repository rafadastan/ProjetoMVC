using Projeto.Repository.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Contracts
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        //método abstrato
        List<Funcionario> Consultar(string nome);
    }
}
