using System.Collections.Generic;
using System.Linq;
using API.DAO.API;
using API.Utils;
using Model.Utils;
using MySql.Data.MySqlClient;

namespace API.Models.API
{
    public class Usuario : ValidacaoModel
    {
        public int Id { get; set; }
        public string UsuarioAcesso { get; set; }
        public bool Ativo { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if ((UsuarioAcesso == null) || (UsuarioAcesso.Trim().Length <= 3))
                mensagens.Add("O Usuário é obrigatório e deve ter ao menos 3 caracteres!");

            return (mensagens.Count() == 0);
        }

        public bool Login(string usuario, string senha, ConexaoGeral connection, MySqlTransaction transaction)
        {
            using (var dao = new UsuarioDAO(connection))
            {
                return dao.Login(this, usuario, senha, transaction);
            }
        }
    }
}
