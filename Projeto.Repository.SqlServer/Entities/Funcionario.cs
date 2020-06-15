using System;
using System.Collections.Generic;
using System.Text;

namespace Projeto.Repository.SqlServer.Entities
{
    public class Funcionario
    {
        //propriedades -> [prop] + 2x[tab]
        public int IdFuncionario { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
        public DateTime DataAdmissao { get; set; }

        //Relacionamento de associação (TER-MUITOS)
        public List<Dependente> Dependentes { get; set; }

        //construtor -> [ctor] + 2x[tab]
        public Funcionario()
        {
            //default
        }

        //sobrecarga de construtores (Overloading)
        public Funcionario(int idFuncionario, string nome, decimal salario, DateTime dataAdmissao)
        {
            IdFuncionario = idFuncionario;
            Nome = nome;
            Salario = salario;
            DataAdmissao = dataAdmissao;
        }
    }
}
