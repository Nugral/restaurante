using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Utils
{
    public abstract class ValidacaoModel : IDisposable
    {
        public abstract bool IsValid(out string[] mensagens);

        public bool IsValid(out string mensagem)
        {
            string[] mensagens;

            mensagem = "";

            if (IsValid(out mensagens))
                return true;
            else
            {
                mensagem = mensagens[0];
                return false;
            };
        }

        public bool IsValid()
        {
            string[] mensagens;

            return IsValid(out mensagens);
        }

        public void Dispose() { }
    }
}
