using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.ViewModel;
using API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class CategoriaProdutoController : ControllerBase
    {
        private ConexaoGeral ConexaoGeral { get; set; }

        public CategoriaProdutoController(ConexaoGeral conexaoGeral) => ConexaoGeral = conexaoGeral;

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                IList<CategoriaProduto> categoriasProduto = CategoriaProduto.GetAll(ConexaoGeral, null);

                var categoriasProdutoExibicao = new List<object>();

                foreach (CategoriaProduto categoriaProduto in categoriasProduto)
                {
                    categoriasProdutoExibicao.Add(new {
                        id = categoriaProduto.Id,
                        descricao = categoriaProduto.Descricao
                    });
                }

                return new JsonResult(categoriasProdutoExibicao);
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

        [HttpGet("{id:int}")]
        public IActionResult Index(int id)
        {
            try
            {
                var categoriaProduto = new CategoriaProduto();

                if (categoriaProduto.SetById(id, ConexaoGeral, null))
                    return new JsonResult(new
                    {
                        id = categoriaProduto.Id,
                        descricao = categoriaProduto.Descricao
                    });
                else
                    return NotFound($"Não existe nenhuma categoria de produto com o id {id}!");
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
        public IActionResult Cadastrar(DadosCategoriaProdutoViewModel categoriaProdutoViewModel)
        {
            try
            {
                using (var categoriaProduto = new CategoriaProduto())
                {

                    categoriaProduto.Descricao = categoriaProdutoViewModel.Descricao;

                    var mensagens = new List<string>();

                    if (!categoriaProduto.IsValid(mensagens))
                        return NotFound(new { erros = mensagens });

                    using (MySqlTransaction transaction = ConexaoGeral.BeginTransaction())
                    {
                        try
                        {
                            categoriaProduto.Salvar(ConexaoGeral, transaction);
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

        [HttpPost("alterar/{id:int}")]
        public IActionResult Alterar(int id, DadosCategoriaProdutoViewModel categoriaProdutoViewModel)
        {
            try
            {
                using (var categoriaProduto = new CategoriaProduto())
                {
                    if (!categoriaProduto.SetById(id, ConexaoGeral, null))
                        return NotFound($"Não existe nenhum funcionário com o id {id}!");

                    categoriaProduto.Descricao = categoriaProdutoViewModel.Descricao;

                    var mensagens = new List<string>();

                    if (!categoriaProduto.IsValid(mensagens))
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
                            categoriaProduto.Salvar(ConexaoGeral, transaction);
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