using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_de_Vendas.Class
{
    public class Clientes
    {
        int idClientes { get; set; }
        string nomeClientes { get; set; }   
        string cpfClientes { get; set; }    
        DateOnly dataNascimentoClientes { get; set; }

        string telefone { get; set; }

        public Clientes(int idClientes, string nome, string cpf, DateOnly dataNascimento, string telefone)
        {
            this.idClientes = idClientes;
            this.nomeClientes = nome;
            this.cpfClientes = cpf;
            this.dataNascimentoClientes = dataNascimento;
            this.telefone = telefone;
        }

    }
}
