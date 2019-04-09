using System;
using System.Collections.Generic;
using API.Models;
using API.Models.ViewModel;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class FuncionarioController : ControllerBase
    {
        private ConexaoGeral ConexaoGeral { get; set; }

        public FuncionarioController(ConexaoGeral conexaoGeral) => ConexaoGeral = conexaoGeral;

        [HttpGet("{id:int}")]
        public IActionResult Index(int id)
        {
            try
            {
                var funcionario = new Funcionario();

                if (funcionario.SetById(id, ConexaoGeral, null))
                    return new JsonResult(new
                    {
                        id = funcionario.Id,
                        nome = funcionario.Nome,
                        cpf = funcionario.Cpf
                    });
                else
                    return NotFound($"Não existe nenhum funcionário com o id {id}!");
            }
            catch (Exception erro)
            {
#if DEBUG
                return StatusCode(500, erro.Message);
#else
                return StatusCode(500);
#endif
            }
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastroFuncionarioViewModel funcionarioViewModel)
        {
            try {
                using (var funcionario = new Funcionario())
                {

                    funcionario.Nome = funcionarioViewModel.Nome;
                    funcionario.DataNascimento = funcionarioViewModel.DataNascimento;
                    funcionario.Nivel = funcionarioViewModel.Nivel;
                    funcionario.Cpf = funcionarioViewModel.Cpf;

                    var mensagens = new List<string>();

                    if (!funcionario.IsValid(mensagens))
                        return NotFound(new {erros = mensagens});

                    using (MySqlTransaction transaction = ConexaoGeral.BeginTransaction())
                    {
                        try
                        {
                            funcionario.Salvar(funcionarioViewModel.Senha, ConexaoGeral, transaction);
                            transaction.Commit();
                            return new OkResult();
                        }
                        catch (Exception erro)
                        {
                            transaction.Rollback();
#if DEBUG
                            return StatusCode(500, erro.Message);
#else
                            return StatusCode(500);
#endif
                        }
                    }
                }
            }
            catch (Exception erro)
            {
#if DEBUG
                return StatusCode(500, erro.Message);
#else
                return StatusCode(500);
#endif
            }
        }

        [Authorize(Policy = "AdministradorOuProprioFuncionario")]
        [HttpPost("alterar/{id:int}")]
        public IActionResult Alterar(int id, DadosFuncionarioViewModel funcionarioViewModel)
        {
            try
            {
                using (var funcionario = new Funcionario())
                {
                    if (!funcionario.SetById(id, ConexaoGeral, null))
                        return NotFound($"Não existe nenhum funcionário com o id {id}!");

                    funcionario.Nome = funcionarioViewModel.Nome;
                    funcionario.DataNascimento = funcionarioViewModel.DataNascimento;
                    funcionario.Nivel = funcionarioViewModel.Nivel;
                    funcionario.Cpf = funcionarioViewModel.Cpf;

                    var mensagens = new List<string>();

                    if (!funcionario.IsValid(mensagens))
                    {
                        return NotFound(new
                        {
                            erros = mensagens
                        });
                    }

                    using (MySqlTransaction transaction = ConexaoGeral.BeginTransaction())
                    {
                        try
                        {
                            funcionario.Salvar(ConexaoGeral, transaction);
                            transaction.Commit();
                            return new OkResult();
                        }
                        catch (Exception erro)
                        {
                            transaction.Rollback();
#if DEBUG
                            return StatusCode(500, erro.Message);
#else
                            return StatusCode(500);
#endif
                        }
                    }
                }
            }
            catch (Exception erro)
            {
#if DEBUG
                return StatusCode(500, erro.Message);
#else
                return StatusCode(500);
#endif
            }
        }
    }
}