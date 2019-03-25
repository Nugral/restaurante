using MySql.Data.MySqlClient;
using API.DAO;
using API.Utils;

namespace API.Models
{
    public class Funcionario : Model.Funcionario
    {
        public void Salvar(ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new FuncionarioDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
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
    }
}
