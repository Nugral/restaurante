using System.Collections.Generic;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class Pedido : ValidacaoModel
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if (NumeroMesa == 0)
                mensagens.Add("A propriedade Mesa é obrigatória!");

            if ((Cpf != null) && (Cpf.Trim() != string.Empty))
            {
                if (!Validacoes.ValidarCpf(Cpf))
                    mensagens.Add("O CPF não está válido!");
            }

            return mensagens.Count() == 0;
        }
    }
}
