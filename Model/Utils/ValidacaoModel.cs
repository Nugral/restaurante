using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Utils
{
    public abstract class ValidacaoModel : IDisposable
    {
        public abstract bool IsValid(IList<string> mensagens);

        public bool IsValid(out string mensagem)
        {
            var mensagens = new List<string>();

            mensagem = "";

            if (IsValid(mensagens))
                return true;
            else
            {
                mensagem = mensagens[0];
                return false;
            };
        }

        public bool IsValid()
        {
            var mensagens = new List<string>();

            return IsValid(mensagens);
        }

        public void Dispose() { }
    }
}
