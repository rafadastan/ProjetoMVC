using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Projeto.CrossCutting.Validations
{
    //Classe de validação customizada
    public class MenorDeIdadeValidation : ValidationAttribute
    {
        //sobrescrita (OVERRIDE) do método IsValid
        public override bool IsValid(object value)
        {
            try
            {
                //convertendo o valor recebido para DateTime
                var dataNascimento = DateTime.Parse(value.ToString());

                //calcular a idade..
                var idade = DateTime.Now.Year - dataNascimento.Year;
                
                //verificar se não fez aniversário
                if(DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                {
                    idade = idade - 1;
                }

                //retornar true ou false..
                return idade < 18;
            }
            catch(Exception)
            {
                return false;
            }            
        }
    }
}
