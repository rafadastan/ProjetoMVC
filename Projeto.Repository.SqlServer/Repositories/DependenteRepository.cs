using Dapper;
using Projeto.Repository.SqlServer.Contracts;
using Projeto.Repository.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Projeto.Repository.SqlServer.Repositories
{
    public class DependenteRepository : IDependenteRepository
    {
        private string connectionString;

        //construtor para receber a connectionstring do banco de dados
        public DependenteRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Dependente obj)
        {
            var query = "insert into Dependente(Nome, DataNascimento, IdFuncionario) "
                      + "values(@Nome, @DataNascimento, @IdFuncionario)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Dependente obj)
        {
            var query = "update Dependente set Nome = @Nome, DataNascimento = @DataNascimento, "
                      + "IdFuncionario = @IdFuncionario where IdDependente = @IdDependente";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(Dependente obj)
        {
            var query = "delete from Dependente where IdDependente = @IdDependente";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Dependente> Consultar()
        {
            var query = "select * from Dependente d "
                      + "inner join Funcionario f "
                      + "on f.IdFuncionario = d.IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, 
                    (Dependente d, Funcionario f) =>
                    {
                        d.Funcionario = f; //armazenando o funcionario
                        return d; //retornando dados do tipo Dependente
                    },
                    splitOn : "IdFuncionario" //chave estrangeira
                    ).ToList(); //lista (muitos registros)
            }
        }

        public Dependente ObterPorId(int id)
        {
            var query = "select * from Dependente d "
                      + "inner join Funcionario f "
                      + "on f.IdFuncionario = d.IdFuncionario "
                      + "where d.IdDependente = @IdDependente";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, 
                    (Dependente d, Funcionario f) =>
                    {
                        d.Funcionario = f;
                        return d;
                    },
                    new { IdDependente = id },
                    splitOn: "IdFuncionario"
                    ).FirstOrDefault();
            }
        }

        public List<Dependente> Consultar(string nome)
        {
            var query = "select * from Dependente d "
                      + "inner join Funcionario f "
                      + "on f.IdFuncionario = d.IdFuncionario "
                      + "where d.Nome like @Nome order by d.Nome";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query(query, 
                    (Dependente d, Funcionario f) => //LAMBDA / ARROW FUNCTION
                    {
                        d.Funcionario = f;
                        return d;
                    },
                    new { Nome = "%" + nome + "%" },
                    splitOn: "IdFuncionario"
                    ).ToList();
            }
        }
    }
}
