using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto.Presentation.Mvc.Models; //importando
using Projeto.Repository.SqlServer.Entities;
using Projeto.Repository.SqlServer.Repositories;

namespace Projeto.Presentation.Mvc.Controllers
{
    [Authorize]
    public class FuncionarioController : Controller
    {
        public IActionResult Cadastro() //abre a página
        {
            return View();
        }

        [HttpPost] //método para receber todos os campos enviados pelo formulário
        public IActionResult Cadastro(FuncionarioCadastroModel model, //dados enviados pela página
            [FromServices] FuncionarioRepository funcionarioRepository) //injeção de dependência
        {
            //verificar se todos os campos enviados passaram nas validações
            if(ModelState.IsValid)
            {
                try
                {
                    //resgatar os dados do funcionário..
                    var funcionario = new Funcionario();

                    funcionario.Nome = model.Nome;
                    funcionario.Salario = decimal.Parse(model.Salario);
                    funcionario.DataAdmissao = DateTime.Parse(model.DataAdmissao);

                    //gravando no banco de dados..
                    funcionarioRepository.Inserir(funcionario);

                    TempData["MensagemSucesso"] = "Funcionário cadastrado com sucesso.";
                    ModelState.Clear(); //limpar o formulário
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                }
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost] //recebe o submit enviado pelo formulário
        public IActionResult Consulta(FuncionarioConsultaModel model, //dados enviados pela página 
            [FromServices] FuncionarioRepository funcionarioRepository) //injeção de dependência
        {
            //verificar se todos os campos da model passaram nas
            //regras de validação.
            if(ModelState.IsValid)
            {
                try
                {
                    model.Funcionarios = funcionarioRepository.Consultar(model.Nome);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
            }

            //enviando o conteudo da classe model de volta para a página
            return View(model); 
        }

        public IActionResult Exclusao(int id, //parametro passado na URL (querystring)
            [FromServices] FuncionarioRepository funcionarioRepository) //injeção de dependência
        {
            try
            {
                //buscar o funcionário no banco de dados atraves do id
                var funcionario = funcionarioRepository.ObterPorId(id);

                //verificando o funcionario foi encontrado
                if(funcionario != null)
                {
                    //excluindo o funcionário
                    funcionarioRepository.Excluir(funcionario);
                    TempData["MensagemSucesso"] = "Funcionário excluído com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Funcionário não encontrado.";
                }
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = "Erro: " + e.Message;
            }

            //redirecionamento
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(int id, //parametro passado pela URL (querystring)
            [FromServices] FuncionarioRepository funcionarioRepository) //injeção de dependência
        {
            var model = new FuncionarioEdicaoModel();

            try
            {
                //buscar o funcionário no banco de dados atraves do id
                var funcionario = funcionarioRepository.ObterPorId(id);

                //verificar se o funcionário foi encontrado
                if(funcionario != null)
                {
                    model.IdFuncionario = funcionario.IdFuncionario;
                    model.Nome = funcionario.Nome;
                    model.Salario = funcionario.Salario.ToString();
                    model.DataAdmissao = funcionario.DataAdmissao.ToString("dd/MM/yyyy");
                }
                else
                {
                    TempData["MensagemErro"] = "Funcionário não foi encontrado.";
                }
            }
            catch(Exception e)
            {
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            //enviando o objeto para a página
            return View(model);
        }

        [HttpPost] //método para receber todos os campos enviados pelo formulário
        public IActionResult Edicao(FuncionarioEdicaoModel model, //dados enviados pela página
            [FromServices] FuncionarioRepository funcionarioRepository) //injeção de dependência
        {
            //verificar se todos os campos enviados passaram nas validações
            if (ModelState.IsValid)
            {
                try
                {
                    //resgatar os dados do funcionário..
                    var funcionario = new Funcionario();

                    funcionario.IdFuncionario = model.IdFuncionario;
                    funcionario.Nome = model.Nome;
                    funcionario.Salario = decimal.Parse(model.Salario);
                    funcionario.DataAdmissao = DateTime.Parse(model.DataAdmissao);

                    //atualizando no banco de dados..
                    funcionarioRepository.Alterar(funcionario);

                    TempData["MensagemSucesso"] = "Funcionário atualizado com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Ocorreu um erro: " + e.Message;
                }
            }

            return View();
        }

    }
}