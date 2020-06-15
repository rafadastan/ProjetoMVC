using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações
using Projeto.Repository.SqlServer.Entities;

namespace Projeto.Presentation.Mvc.Models
{
    public class FuncionarioConsultaModel
    {        
        [Required(ErrorMessage = "Por favor, informe o nome do funcionário desejado.")]
        [MinLength(3, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        public string Nome { get; set; }

        //declarando uma lista de Funcionários
        //esta lista irá exibir na página o resultado da pesquisa
        public List<Funcionario> Funcionarios { get; set; }
    }
}
