using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    class ProdutoPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
