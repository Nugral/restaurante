using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class ProdutoPedido : ValidacaoModel
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if (IdPedido == 0)
                mensagens.Add("A propriedade IdPedido é obrigatória!");

            if (IdProduto == 0)
                mensagens.Add("A propriedade IdProduto é obrigatória!");

            if (Valor == 0)
                mensagens.Add("A propriedade Valor é obrigatória!");

            return mensagens.Count() == 0;
        }
    }
}
