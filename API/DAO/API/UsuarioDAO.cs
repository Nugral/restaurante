using System;
using System.Collections.Generic;
using System.Data;
using API.Models.API;
using API.Utils;
using MySql.Data.MySqlClient;

namespace API.DAO.API
{
    public class UsuarioDAO : IDAO<Usuario>
    {
        private ConexaoGeral _conn;

        public UsuarioDAO(ConexaoGeral conn) => _conn = conn;

        public int Insert(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Remove(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public int Update(Usuario model, MySqlTransaction transaction) => throw new NotImplementedException();

        public bool Login(Usuario model, string usuario, string senha, MySqlTransaction transaction)
        {
            string sql = " SELECT *                      " +
                         " FROM api_usuario a            " +
                         " WHERE a.`usuario` = @usuario  " +
                         "   AND a.`senha` = MD5(@senha) " +
                         "   AND a.`ativo`               ";

            var parameters = new List<MySqlParameter>();
            parameters.Add(new MySqlParameter("@usuario", MySqlDbType.String) { Value = usuario });
            parameters.Add(new MySqlParameter("@senha", MySqlDbType.String) { Value = senha });

            DataTable dt = _conn.ExecuteReader(sql, parameters, transaction);

            if (dt.Rows.Count == 0)
                return false;
            else if (dt.Rows.Count > 1)
                throw new Exception($"Foram encontrados {dt.Rows.Count} usuários com esses parâmetros!");

            PreencherModel(model, dt.Rows[0]);

            return true;
        }

        private void PreencherModel(Usuario model, DataRow dr)
        {
            model.Id = int.Parse(dr["id"].ToString());
            model.UsuarioAcesso = dr["usuario"].ToString();
        }

        public void Dispose() { }
    }
}
