using Projeto.Repository.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Contracts
{
    public interface IDependenteRepository : IBaseRepository<Dependente>
    {
        List<Dependente> Consultar(string nome);
    }
}
