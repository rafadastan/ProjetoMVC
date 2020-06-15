using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataCriacao { get; set; }

        //ctor + 2x[tab]
        public Usuario()
        {
            //default
        }

        public Usuario(int idUsuario, string nome, string email, string senha, DateTime dataCriacao)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataCriacao = dataCriacao;
        }
    }
}
