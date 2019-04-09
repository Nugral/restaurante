using MySql.Data.MySqlClient;
using API.DAO;
using API.Utils;
using System;

namespace API.Models
{
    public class Funcionario : Model.Funcionario
    {
        public void Salvar(string senha, ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new FuncionarioDAO(conn))
            {
                if (Id != 0)
                    throw new Exception("Não é possível inserir um registro que já possui identificador!");

                dao.Insert(this, senha, transaction);
            }
        }

        public void Salvar(ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new FuncionarioDAO(conn))
            {
                if (Id == 0)
                    throw new Exception("Não é possível alterar um registro que não possui identificador!");

                dao.Update(this, transaction);
            }
        }

        public bool SetById(int id, ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new FuncionarioDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public bool Login(string cpf, string senha, ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new FuncionarioDAO(conn))
            {
                return dao.Login(this, cpf, senha, transaction);
            }
        }
    }
}
