using System;
using System.Linq;
using Model.Utils;

namespace Model
{
    public class Funcionario : ValidacaoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }

        public override bool IsValid(out string[] mensagens)
        {
            mensagens = new string[0];

            if ((Nome == null) || (Nome.Trim().Length <= 3))
                mensagens.Append("O nome é obrigatório e deve ter ao menos 3 caracteres!");

            if ((Cpf != null) && (Cpf.Trim().Length == 11))
            {
                if (!Validacoes.ValidarCpf(Cpf))
                    mensagens.Append("O cpf não é válido!");
            } else
                mensagens.Append("O Cpf é obrigatório e deve ter 11 dígitos!");

            return mensagens.Count() == 0;
        }
    }
}
