using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Vendas.Class
{
    public class ContextoDados
    {
        public class Fornecedor
        {
            
            public int ID { get; set; }
            public string Cnpj { get; set; }
            public string Nome { get; set; }
            public string Rua { get; set; }
            public string Complemento { get; set; }
            public string Bairro { get; set; }
            public int IDUF { get; set; }
            public DateTime DataCadastro { get; set; }

        }

        public class Categoria
        {
            public int IDCat { get; set; }
            public string NomeCategoria { get; set; }
            public DateTime DataCadastro { get; set; }
        }
    }


    //MessageBox.Show(
    //            $"Fornecedor ID: {fornecedor.ID}\n" +
    //            $"Nome: {fornecedor.Nome}\n" +
    //            $"CNPJ: {fornecedor.Cnpj}\n" +
    //            $"Endereço: {fornecedor.Rua}, {fornecedor.Complemento}\n" +
    //            $"Bairro: {fornecedor.Bairro}\n" +
    //            $"ID UF: {fornecedor.IDUF}\n" +
    //            $"Data de Cadastro: {fornecedor.DataCadastro}",
    //            "Dados do Fornecedor"
    //        );
}
