using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //validações
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.CrossCutting.Validations;

namespace Projeto.Presentation.Mvc.Models
{
    public class DependenteCadastroModel
    {
        [MinLength(6, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do dependente.")]
        public string Nome { get; set; }

        [MenorDeIdadeValidation(ErrorMessage = "O Dependente deve ser menor de idade.")]
        [Required(ErrorMessage = "Por favor, informe a data de nascimento do dependente.")]
        public string DataNascimento { get; set; }

        #region Campo de seleção de funcionário

        //armazenar o id do funcionário selecionando no campo DropDownList
        [Required(ErrorMessage = "Por favor, selecione 1 funcionário.")]
        public string IdFuncionario { get; set; }

        //gerar a lista de opções do campo DropDownList
        public List<SelectListItem> ListaDeFuncionarios { get; set; }

        #endregion
    }
}
