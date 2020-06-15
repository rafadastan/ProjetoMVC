using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Projeto.CrossCutting.Cryptography;
using Projeto.Presentation.Mvc.Models;
using Projeto.Repository.SqlServer.Entities;
using Projeto.Repository.SqlServer.Repositories;

namespace Projeto.Presentation.Mvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login() //abre a página
        {
            return View();
        }

        public IActionResult Register() //abre a página
        {
            return View();
        }

        [HttpPost] //recebe o SUBMIT do formulário (envio dos dados)
        public IActionResult Login(AccountLoginModel model, //campos do formulário
            [FromServices] UsuarioRepository usuarioRepository) //injeção de dependência
        {
            //verificar se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //buscando o usuário no banco de dados pelo email e senha
                    var usuario = usuarioRepository.Consultar(model.Email, MD5Encrypt.GenerateHash(model.Senha));

                    //verificando se o usuário foi encontrado
                    if (usuario != null)
                    {
                        //criando a credencial (permissão) de acesso do usuário..
                        var identity = new ClaimsIdentity(new[]
                            {
                                new Claim(ClaimTypes.Name, usuario.Email)
                            }, CookieAuthenticationDefaults.AuthenticationScheme);

                        //gravar esta permissão em um arquivo de cookie
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        //redirecionando para a página /Home/Index
                        return RedirectToAction("Index", "Home"); //Home/Index
                    }
                    else
                    {
                        TempData["MensagemErro"] = "Acesso Negado. Usuário não foi encontrado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        [HttpPost]
        public IActionResult Register(AccountRegisterModel model, //campos do formulário
            [FromServices] UsuarioRepository usuarioRepository) //injeção de dependência
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //verificar se o email informado já encontra-se cadastrado.
                    if (usuarioRepository.Consultar(model.Email) != null)
                    {
                        TempData["MensagemErro"] = "O email informado já encontra-se cadastrado. Tente outro.";
                    }
                    else
                    {
                        var usuario = new Usuario(); //criando um usuário

                        usuario.Nome = model.Nome;
                        usuario.Email = model.Email;
                        usuario.Senha = MD5Encrypt.GenerateHash(model.Senha);
                        usuario.DataCriacao = DateTime.Now;

                        usuarioRepository.Inserir(usuario); //gravando no banco..

                        TempData["MensagemSucesso"] = "Conta de usuário cadastrada com sucesso!";
                        ModelState.Clear(); //limpar o conteudo do formulário
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }

            return View();
        }

        //método para fazer o logout do usuário
        public IActionResult Logout()
        {
            //destruir o cookie de autenticação do usuário
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //redirecionamento para a página de login
            return RedirectToAction("Login");
        }

    }
}