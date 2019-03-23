using System;
using System.Data;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace API.Utils
{
    public class Conexao : IDisposable
    {
        protected MySqlConnection _conn;

        public Conexao(string connectionString)
        {
            _conn = new MySqlConnection(connectionString);
            _conn.Open();
        }

        private void SetParametersToCommand(MySqlCommand command, IList<MySqlParameter> parameters)
        {
            foreach (MySqlParameter parameter in parameters)
                command.Parameters.Add(parameter);
        }

        public int Execute(string sql, IList<MySqlParameter> parameters = null, MySqlTransaction transaction = null)
        {
            using (var command = new MySqlCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                return command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteReader(string sql, IList<MySqlParameter> parameters = null, MySqlTransaction transaction = null)
        {
            using (var command = new MySqlCommand(sql, _conn))
            {
                if (parameters != null)
                    SetParametersToCommand(command, parameters);

                if (transaction != null)
                    command.Transaction = transaction;

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    var dt = new DataTable();
                    dt.Load(dr);

                    dr.Close();

                    return dt;
                }
            }
        }

        public int UltimoIdInserido() => int.Parse(ExecuteReader("SELECT LAST_INSERT_ID()").Rows[0].ToString());

        public MySqlTransaction BeginTransaction() => _conn.BeginTransaction();

        public void Dispose()
        {
            _conn.Dispose();
        }
    }
}
