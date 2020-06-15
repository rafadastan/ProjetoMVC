using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Projeto.Repository.SqlServer.Entities;

namespace Projeto.Presentation.Mvc.Models
{
    public class DependenteConsultaModel
    {
        [MinLength(3, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome desejado.")]
        public string Nome { get; set; }

        //declarando uma lista de dependentes
        //esta lista irá exibir na página o resultado da consulta
        public List<Dependente> Dependentes { get; set; }
    }
}
