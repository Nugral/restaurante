using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string CodigoBarras { get; set; }
        public int IdCategoria { get; set; }
    }
}
