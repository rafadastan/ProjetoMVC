using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Entities
{
    public class Dependente
    {
        //propriedades -> [prop] + 2x[tab]
        public int IdDependente { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public int IdFuncionario { get; set; }

        //Relacionamento de Associação (TER-1)
        public Funcionario Funcionario { get; set; }

        //construtor -> [ctor] + 2x[tab]
        public Dependente()
        {
            //vazio (default)
        }

        //sobrecarga de métodos (Overloading)
        public Dependente(int idDependente, string nome, DateTime dataNascimento, int idFuncionario)
        {
            IdDependente = idDependente;
            Nome = nome;
            DataNascimento = dataNascimento;
            IdFuncionario = idFuncionario;
        }
    }
}
