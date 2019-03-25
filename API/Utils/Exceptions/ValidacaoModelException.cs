using System;

namespace API.Utils.Exceptions
{
    public class ValidacaoModelException : Exception
    {
        public ValidacaoModelException(string mensagem) : base(mensagem) { }
    }
}
