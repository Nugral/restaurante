using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class Produto : ValidacaoModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string CodigoBarras { get; set; }
        public int IdCategoria { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if ((Descricao == null) || (Descricao.Trim().Length <= 3))
                mensagens.Add("A descrição é obrigatória e deve ter ao menos 3 caracteres!");

            if (Valor == 0)
                mensagens.Add("O valor não pode ser 0,00!");

            if (IdCategoria == 0)
                mensagens.Add("A categoria do produto deve ser informada!");

            return mensagens.Count() == 0;
        }
    }
}
