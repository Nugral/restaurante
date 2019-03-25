using System;

namespace API.Utils.Exceptions
{
    public class DataBaseException : Exception
    {
        public DataBaseException(string mensagem) : base(mensagem) { }
    }
}
