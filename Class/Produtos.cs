using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Vendas.Class
{
    public class Produtos
    {
        int idProdutos { get; set; }
        string nomeProdutos { get; set; }
        string descricaoProdutos { get; set; }
        decimal precoProdutos { get; set; }
        int quantidadeEstoqueProdutos { get; set; }
        Fornecedores fornecedorProdutos { get; set; }
        public Produtos(int idProduto, string nome, string descricao, decimal preco, int quantidadeEstoqueProdutos, Fornecedores fornecedorProdutos)
        {
            this.idProdutos = idProduto;
            this.nomeProdutos = nome;
            this.descricaoProdutos = descricao;
            this.precoProdutos = preco;
            this.quantidadeEstoqueProdutos = quantidadeEstoqueProdutos;
            this.fornecedorProdutos = fornecedorProdutos;
        }
    }
}
