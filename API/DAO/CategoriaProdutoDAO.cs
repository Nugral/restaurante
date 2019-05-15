using API.Models;
using API.Utils;
using API.Utils.Exceptions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace API.DAO
{
    public class CategoriaProdutoDAO : IDAO<CategoriaProduto>
    {
        private ConexaoGeral _conn;

        public CategoriaProdutoDAO(ConexaoGeral conn) => _conn = conn;

        public int Insert(CategoriaProduto model, MySqlTransaction transaction)
        {
            if (model.Id != 0)
                throw new DataBaseException("Não é possível inserir um registro que já possui identificador!");

            if (!model.IsValid())
                throw new ValidacaoModelException("O modelo não está em um estado válido!");

            string sql = " INSERT INTO categoria_produto " +
                         "   (descricao)                 " +
                         " VALUES                        " +
                         "   (@descricao)                ";

            var parameters = GetParameters(model);

            int linhasAfetadas = _conn.Execute(sql, parameters, transaction);

            if (linhasAfetadas != 1)
                return linhasAfetadas;

            model.Id = _conn.UltimoIdInserido();

            return linhasAfetadas;
        }

        public int Remove(CategoriaProduto model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(CategoriaProduto model, MySqlTransaction transaction)
        {
            if (model.Id == 0)
                throw new DataBaseException("Não é possível alterar um registro que não possui identificador!");

            if (!model.IsValid())
                throw new ValidacaoModelException("O modelo não está em um estado válido!");

            string sql = " UPDATE categoria_produto SET " +
                         "   descricao = @descricao     " +
                         " WHERE id = @id               ";

            var parameters = GetParameters(model);
            parameters.Add(new MySqlParameter("@id", DbType.Int32) { Value = model.Id });

            return _conn.Execute(sql, parameters, transaction);
        }

        public bool SetById(CategoriaProduto model, int id, MySqlTransaction transaction)
        {
            string sql = " SELECT *                 " +
                         " FROM categoria_produto a " +
                         " WHERE a.`id` = @id       ";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32) { Value = id });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Existem {dt.Rows.Count} categorias de produto com o id `{id}`!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        public IList<CategoriaProduto> GetAll(MySqlTransaction transaction)
        {
            var categoriasProduto = new List<CategoriaProduto>();

            string sql = " SELECT a.*               " +
                         " FROM categoria_produto a ";

            DataTable dt = _conn.ExecuteReader(sql, null, transaction);

            foreach (DataRow dr in dt.Rows)
            {
                var categoriaProduto = new CategoriaProduto();
                PreencherModel(categoriaProduto, dr);

                categoriasProduto.Add(categoriaProduto);
            }

            return categoriasProduto;
        }

        private List<MySqlParameter> GetParameters(CategoriaProduto model)
        {
            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@descricao", MySqlDbType.String) { Value = model.Descricao });

            return parameters;
        }

        private void PreencherModel(CategoriaProduto model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.Descricao = dr["descricao"].ToString();
        }

        public void Dispose() { }
    }
}
