using Microsoft.Extensions.Configuration;

namespace API.Utils
{
    public class ConexaoGeral : Conexao
    {
        public ConexaoGeral(IConfiguration configuration) : base(configuration.GetConnectionString("Default")) { }
    }
}
