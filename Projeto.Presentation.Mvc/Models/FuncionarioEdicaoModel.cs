using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações

namespace Projeto.Presentation.Mvc.Models
{
    public class FuncionarioEdicaoModel
    {
        //campo oculto
        public int IdFuncionario { get; set; }

        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do funcionário.")]
        public string Nome { get; set; } //campo

        [Required(ErrorMessage = "Por favor, informe o salário do funcionário.")]
        public string Salario { get; set; } //campo

        [Required(ErrorMessage = "Por favor, informe a data de admissão do funcionário.")]
        public string DataAdmissao { get; set; } //campo
    }
}
