using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto.Presentation.Mvc.Models;
using Projeto.Repository.SqlServer.Entities;
using Projeto.Repository.SqlServer.Repositories;

namespace Projeto.Presentation.Mvc.Controllers
{
    [Authorize] //Acesso de autenticação
    public class DependenteController : Controller
    {
        public IActionResult Cadastro([FromServices] FuncionarioRepository funcionarioRepository)
        {
            var result = new DependenteCadastroModel();
            result.ListaDeFuncionarios = ObterListaDeFuncionarios(funcionarioRepository);

            return View(result); //enviando os dados para a página
        }

        [HttpPost] //recebe o SUBMIT do formulário
        public IActionResult Cadastro(DependenteCadastroModel model, 
            [FromServices] FuncionarioRepository funcionarioRepository, 
            [FromServices] DependenteRepository dependenteRepository)
        {
            if(ModelState.IsValid) //verifica se todos os campos passaram nas validações
            {
                try
                {
                    var dependente = new Dependente();
                    dependente.Nome = model.Nome;
                    dependente.DataNascimento = DateTime.Parse(model.DataNascimento);
                    dependente.IdFuncionario = int.Parse(model.IdFuncionario);

                    dependenteRepository.Inserir(dependente);

                    TempData["MensagemSucesso"] = "Dependente cadastrado com sucesso.";
                    ModelState.Clear(); //limpar o formulário
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
            }

            var result = new DependenteCadastroModel();
            result.ListaDeFuncionarios = ObterListaDeFuncionarios(funcionarioRepository);

            return View(result); //enviando os dados para a página
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost] //recebe a requisição do formulário
        public IActionResult Consulta(DependenteConsultaModel model, //dados do formulário
            [FromServices] DependenteRepository dependenteRepository) //injeção de dependência
        {
            if(ModelState.IsValid) //validação da model
            {
                try
                {
                    model.Dependentes = dependenteRepository.Consultar(model.Nome);
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
            }

            //enviando a model..
            return View(model);
        }

        public IActionResult Exclusao(int id, //parametro passado na URL (querystring)
           [FromServices] DependenteRepository dependenteRepository) //injeção de dependência
        {
            try
            {
                //buscar o dependente no banco de dados atraves do id
                var dependente = dependenteRepository.ObterPorId(id);

                //verificando o dependente foi encontrado
                if (dependente != null)
                {
                    //excluindo o dependente
                    dependenteRepository.Excluir(dependente);
                    TempData["MensagemSucesso"] = "Dependente excluído com sucesso.";
                }
                else
                {
                    TempData["MensagemErro"] = "Dependente não encontrado.";
                }
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = "Erro: " + e.Message;
            }

            //redirecionamento
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(int id, //parametro passado pela URL (querystring)
            [FromServices] FuncionarioRepository funcionarioRepository,
            [FromServices] DependenteRepository dependenteRepository) //injeção de dependência
        {
            var model = new DependenteEdicaoModel();

            try
            {
                //buscar o dependente no banco de dados atraves do id
                var dependente = dependenteRepository.ObterPorId(id);

                //verificar se o funcionário foi encontrado
                if (dependente != null)
                {
                    model.IdDependente = dependente.IdDependente;
                    model.Nome = dependente.Nome;
                    model.DataNascimento = dependente.DataNascimento.ToString("dd/MM/yyyy");
                    model.IdFuncionario = dependente.IdFuncionario.ToString();
                    model.ListaDeFuncionarios = ObterListaDeFuncionarios(funcionarioRepository);
                }
                else
                {
                    TempData["MensagemErro"] = "Dependente não foi encontrado.";
                }
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = "Erro: " + e.Message;
            }

            //enviando o objeto para a página
            return View(model);
        }

        [HttpPost] //recebe o SUBMIT do formulário
        public IActionResult Edicao(DependenteEdicaoModel model,
            [FromServices] FuncionarioRepository funcionarioRepository,
            [FromServices] DependenteRepository dependenteRepository)
        {
            if (ModelState.IsValid) //verifica se todos os campos passaram nas validações
            {
                try
                {
                    var dependente = new Dependente();

                    dependente.IdDependente = model.IdDependente;
                    dependente.Nome = model.Nome;
                    dependente.DataNascimento = DateTime.Parse(model.DataNascimento);
                    dependente.IdFuncionario = int.Parse(model.IdFuncionario);

                    dependenteRepository.Alterar(dependente);

                    TempData["MensagemSucesso"] = "Dependente atualizado com sucesso.";
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = "Erro: " + e.Message;
                }
            }

            var result = new DependenteEdicaoModel();
            result.ListaDeFuncionarios = ObterListaDeFuncionarios(funcionarioRepository);

            return View(result); //enviando os dados para a página
        }


        //método privado para carregar todos os funcionários do banco de dados
        //e retornar uma lista do tipo List<SelectListItem>
        private List<SelectListItem> ObterListaDeFuncionarios(FuncionarioRepository funcionarioRepository)
        {
            //declarando uma lista do tipo SelectListItem
            //utilizada para gerar o conteudo (opções) do campo DropDownList
            var lista = new List<SelectListItem>();

            //consultar todos os funcionarios do banco de dados..
            foreach (var item in funcionarioRepository.Consultar())
            {
                var opcao = new SelectListItem();
                opcao.Value = item.IdFuncionario.ToString();
                opcao.Text = item.Nome;

                lista.Add(opcao); //populando a lista de opções
            }

            //retornando a lista
            return lista;
        }
    }
}