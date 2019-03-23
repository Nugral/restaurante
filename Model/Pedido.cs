using System.Linq;
using Model.Utils;

namespace Model
{
    public class Pedido : ValidacaoModel
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid(out string[] mensagens)
        {
            mensagens = new string[0];

            if (NumeroMesa == 0)
                mensagens.Append("A propriedade Mesa é obrigatória!");

            if ((Cpf != null) && (Cpf.Trim() != string.Empty))
            {
                if (!Validacoes.ValidarCpf(Cpf))
                    mensagens.Append("O CPF não está válido!");
            }

            return mensagens.Count() == 0;
        }
    }
}
