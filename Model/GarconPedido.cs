using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class GarconPedido : ValidacaoModel
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdFuncionario { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if (IdPedido == 0)
                mensagens.Add("A propriedade IdPedido é obrigatória!");

            if (IdFuncionario == 0)
                mensagens.Add("A propriedade IdPedido é obrigatória!");

            return mensagens.Count() == 0;
        }
    }
}
