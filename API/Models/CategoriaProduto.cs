using API.DAO;
using API.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class CategoriaProduto : Model.CategoriaProduto
    {
        public void Salvar(ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new CategoriaProdutoDAO(conn))
            {
                if (Id == 0)
                    dao.Insert(this, transaction);
                else
                    dao.Update(this, transaction);
            }
        }

        public bool SetById(int id, ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new CategoriaProdutoDAO(conn))
            {
                return dao.SetById(this, id, transaction);
            }
        }

        public static IList<CategoriaProduto> GetAll(ConexaoGeral conn, MySqlTransaction transaction)
        {
            using (var dao = new CategoriaProdutoDAO(conn))
            {
                return dao.GetAll(transaction);
            }
        }
    }
}
