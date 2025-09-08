using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Vendas.Class
{
    public class Fornecedores
    {
        int idFornecedores { get; set; }
        string nomeFornecedores { get; set; }
        string cnpjFornecedores { get; set; }
        string enderecoFornecedores { get; set; }
        public Fornecedores(int idFornecedores, string nomeFornecedores, string cnpjFornecedores, string enderecoFornecedores)
        {
            this.idFornecedores = idFornecedores;
            this.nomeFornecedores = nomeFornecedores;
            this.cnpjFornecedores = cnpjFornecedores;
            this.enderecoFornecedores = enderecoFornecedores;    
        }
    }
}
