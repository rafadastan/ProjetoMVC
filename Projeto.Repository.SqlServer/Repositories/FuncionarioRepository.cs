using Dapper;
using Projeto.Repository.SqlServer.Contracts;
using Projeto.Repository.SqlServer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Projeto.Repository.SqlServer.Repositories
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        //atributo
        private string connectionString;

        //método construtor recebendo como parametro uma connectionString
        public FuncionarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Funcionario obj)
        {
            //escrevendo um comando SQL..
            var query = "insert into Funcionario(Nome, Salario, DataAdmissao) "
                      + "values(@Nome, @Salario, @DataAdmissao)";

            //conectando no sqlserver
            using (var connection = new SqlConnection(connectionString))
            {
                //executando a query sql e passando os dados do objeto Funcionario
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Funcionario obj)
        {
            var query = "update Funcionario set Nome = @Nome, Salario = @Salario, "
                      + "DataAdmissao = @DataAdmissao where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(Funcionario obj)
        {
            var query = "delete from Funcionario where IdFuncionario = @IdFuncionario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Funcionario> Consultar()
        {
            var query = "select * from Funcionario order by Nome";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcionario>(query).ToList();
            }
        }

        public Funcionario ObterPorId(int id)
        {
            var query = "select * from Funcionario where IdFuncionario = @IdFuncionario";

            using(var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcionario>
                    (query, new { IdFuncionario = id })
                    .FirstOrDefault(); //retornar 1 unico resultado
            }
        }

        public List<Funcionario> Consultar(string nome)
        {
            var query = "select * from Funcionario where Nome like @Nome order by Nome";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Funcionario>
                    (query, new { Nome = "%" + nome + "%" /*contendo*/ })
                    .ToList();
            }
        }
    }
}
