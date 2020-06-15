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
    public class UsuarioRepository : IUsuarioRepository
    {
        //atributo
        private string connectionString;

        //construtor recebendo a string de conexão do banco de dados
        public UsuarioRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Inserir(Usuario obj)
        {
            var query = "insert into Usuario(Nome, Email, Senha, DataCriacao) "
                      + "values(@Nome, @Email, @Senha, @DataCriacao)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Alterar(Usuario obj)
        {
            var query = "update Usuario set Nome = @Nome, Email = @Email, Senha = @Senha "
                      + "where IdUsuario = @IdUsuario"; 

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public void Excluir(Usuario obj)
        {
            var query = "delete from Usuario where IdUsuario = @IdUsuario";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Usuario> Consultar()
        {
            var query = "select * from Usuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>(query).ToList();
            }
        }

        public Usuario ObterPorId(int id)
        {
            var query = "select * from Usuario where IdUsuario = @IdUsuario";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                    (query, new { IdUsuario = id }).FirstOrDefault();
            }
        }

        public Usuario Consultar(string email)
        {
            var query = "select * from Usuario where Email = @Email";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                    (query, new { Email = email }).FirstOrDefault();
            }
        }

        public Usuario Consultar(string email, string senha)
        {
            var query = "select * from Usuario where Email = @Email and Senha = @Senha";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<Usuario>
                    (query, new { Email = email, Senha = senha }).FirstOrDefault();
            }
        }
    }
}
