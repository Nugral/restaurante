using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    class ProdutoPedido
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal Valor { get; set; }
    }
}
