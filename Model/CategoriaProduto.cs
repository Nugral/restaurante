using System.Linq;
using Model.Utils;

namespace Model
{
    public class CategoriaProduto : ValidacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public override bool IsValid(out string[] mensagens)
        {
            mensagens = new string[0];

            if ((Descricao == null) || (Descricao.Trim().Length <= 3))
                mensagens.Append("A descrição é obrigatória e deve ter ao menos 3 caracteres!");

            return mensagens.Count() == 0;
        }
    }
}
