using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using API.Models;
using API.Utils;
using API.Utils.Exceptions;

namespace API.DAO
{
    public class FuncionarioDAO : IDAO<Funcionario>
    {
        private ConexaoGeral _conn;

        public FuncionarioDAO(ConexaoGeral conn) => _conn = conn;

        public int Insert(Funcionario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Insert(Funcionario model, string senha, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new DataBaseException("Não é possível inserir um registro que já possui identificador!");

            if (!model.IsValid())
                throw new ValidacaoModelException("O modelo não está em um estado válido!");

            if (string.IsNullOrEmpty(senha))
                throw new ValidacaoModelException("A senha é obrigatória para cadastrar um funcionário!");

            string sql = " INSERT INTO funcionario                                      " +
                         "   (nome, dataNascimento, cpf, nivel, senha, dataCadastro)    " +
                         " VALUES                                                       " +
                         "   (@nome, @dataNascimento, @cpf, @nivel, MD5(@senha), NOW()) ";

            var parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@senha", MySqlDbType.String) { Value = senha });

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(Funcionario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(Funcionario model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new DataBaseException("Não é possível alterar um registro que não possui identificador!");

            if (!model.IsValid())
                throw new ValidacaoModelException("O modelo não está em um estado válido!");

            string sql = " UPDATE funcionario SET                            " +
                         "   nome = @nome, dataNascimento = @dataNascimento, " +
                         "   cpf = @cpf, dataModificacao = NOW()             " +
                         " WHERE id = @id                                    ";

            var parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", DbType.Int32) { Value = model.Id });

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public bool SetById(Funcionario model, int id, MySqlTransaction transaction)
        {
            string sql = " SELECT *           " +
                         " FROM funcionario a " +
                         " WHERE a.`id` = @id ";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} funcionários com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public bool Login(Funcionario model, string cpf, string senha, MySqlTransaction transaction)
        {
            string sql = " SELECT *                      " +
                         " FROM funcionario a            " +
                         " WHERE a.`cpf` = @cpf          " +
                         "   AND a.`senha` = MD5(@senha) ";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = cpf });
            parameters.Add(new MySqlParameter("@senha", MySqlDbType.String) { Value = senha });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Foram encontrados {dt.Rows.Count} funcionários com essas informações!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        private List<MySqlParameter> GetParameters(Funcionario model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@nome", MySqlDbType.String) { Value = model.Nome });
            parameters.Add(new MySqlParameter("@dataNascimento", MySqlDbType.Date) { Value = model.DataNascimento });
            parameters.Add(new MySqlParameter("@nivel", MySqlDbType.String) { Value = model.Nivel });
            parameters.Add(new MySqlParameter("@cpf", MySqlDbType.String) { Value = model.Cpf });

            return parameters;
        }

        private void PreencherModel(Funcionario model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.Nome = dr["nome"].ToString();
            model.DataNascimento = DateTime.Parse(dr["dataNascimento"].ToString());
            model.Nivel = dr["nivel"].ToString();
            model.Cpf = dr["cpf"].ToString();
        }

        public void Dispose() { }
    }
}
