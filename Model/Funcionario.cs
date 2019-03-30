using System;
using System.Collections.Generic;
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
        public string Nivel { get; set; }

        public override bool IsValid(IList<string> mensagens)
        {
            if ((Nome == null) || (Nome.Trim().Length <= 3))
                mensagens.Add("O nome é obrigatório e deve ter ao menos 3 caracteres!");

            if ((Cpf != null) && (Cpf.Trim().Length == 11))
            {
                if (!Validacoes.ValidarCpf(Cpf))
                    mensagens.Add("O cpf não é válido!");
            } else
                mensagens.Add("O Cpf é obrigatório e deve ter 11 dígitos!");

            if ((Nivel == null) || (Nivel.Trim() == string.Empty))
                mensagens.Add("O nível de acesso é obrigatório!");

            return mensagens.Count() == 0;
        }
    }
}
