using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Vendas.Class
{
    public class Vendas
    {
        string docVenda { get; set; }
        DateOnly dataVenda { get; set; }
        Clientes clienteVenda { get; set; }
        List<Produtos> produtosVenda { get; set; }
        decimal valorTotalVenda { get; set; }
        public Vendas(string docVenda, DateOnly dataVenda, Clientes clienteVenda, List<Produtos> produtosVenda, decimal valorTotalVenda)
        {
            this.docVenda = docVenda;
            this.dataVenda = dataVenda;
            this.clienteVenda = clienteVenda;
            this.produtosVenda = produtosVenda;
            this.valorTotalVenda = valorTotalVenda;
        }
    }
}
