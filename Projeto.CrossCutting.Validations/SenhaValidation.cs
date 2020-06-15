using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.CrossCutting.Validations
{
    public class SenhaValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var senha = value.ToString();
                return senha.Any(char.IsUpper) 
                    && senha.Any(char.IsLower)
                    && senha.Any(char.IsDigit)
                    && (
                        senha.Contains("!") || 
                        senha.Contains("@") ||
                        senha.Contains("#") ||
                        senha.Contains("$") ||
                        senha.Contains("%") ||
                        senha.Contains("&") 
                       );
            }
            return false;
        }
    }
}
