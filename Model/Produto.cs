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

        public override bool IsValid(out string[] mensagens)
        {
            mensagens = new string[0];

            if ((Descricao == null) || (Descricao.Trim().Length <= 3))
                mensagens.Append("A descrição é obrigatória e deve ter ao menos 3 caracteres!");

            if (Valor == 0)
                mensagens.Append("O valor não pode ser 0,00!");

            if (IdCategoria == 0)
                mensagens.Append("A categoria do produto deve ser informada!");

            return mensagens.Count() == 0;
        }
    }
}
