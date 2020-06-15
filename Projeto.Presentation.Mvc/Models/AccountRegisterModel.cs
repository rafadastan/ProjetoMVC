using Projeto.CrossCutting.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Presentation.Mvc.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage = "Por favor, informe o nome de usuário.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email de acesso.")]
        public string Email { get; set; }

        [SenhaValidation(ErrorMessage = "A senha deve conter 1 letra maíuscula, 1 minúscula e 1 caracteres especial ")]
        [MinLength(6, ErrorMessage = "Pro favor, informe no mínimo {1} caracteres")]
        [MaxLength(20, ErrorMessage = "Por favor, informe no máximo {1} caracteres")]
        [Required(ErrorMessage = "Por favor, digite a senha de acesso.")]
        public string Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem.")]
        [Required(ErrorMessage = "Por favor, confirme a senha de acesso.")]
        public string SenhaConfirmacao { get; set; }
    }
}
