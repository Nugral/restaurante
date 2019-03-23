using System;

namespace API.Utils.Exceptions
{
    public class ConfiguracaoException : Exception
    {
        public ConfiguracaoException(string mensagem) : base(mensagem) { }
    }
}
