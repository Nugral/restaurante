using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class CategoriaProduto : ValidacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if ((Descricao == null) || (Descricao.Trim().Length <= 3))
                mensagens.Add("A descrição é obrigatória e deve ter ao menos 3 caracteres!");

            return mensagens.Count() == 0;
        }
    }
}
