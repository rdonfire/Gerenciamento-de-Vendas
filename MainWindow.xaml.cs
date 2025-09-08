using Gerenciador_de_Vendas.Class;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gerenciador_de_Vendas
{
    public enum MoudlosTela
    {
        Vendas,
        Clientes,
        Produtos,
        Fornecedores
    }

    public partial class MainWindow : Window
    {

        DataeHora DataeHora = new();
        

        public MainWindow()
        {
            InitializeComponent();

            DataeHora.atualizarDataHora(lblDataHora);
        }

        private void btnVenda_Click(object sender, RoutedEventArgs e)
        {
            ExibirJanela(MoudlosTela.Vendas);
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            ExibirJanela(MoudlosTela.Clientes);
        }

        private void btnProdutos_Click(object sender, RoutedEventArgs e)
        {
            ExibirJanela(MoudlosTela.Produtos); 
        }

        private void btnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            ExibirJanela(MoudlosTela.Fornecedores);
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private static void ExibirJanela(MoudlosTela moduloTela)
        {
            switch (moduloTela)
            {
                case MoudlosTela.Vendas:
                    Vendas telaVendas = new();
                    telaVendas.Show();
                    break;
                case MoudlosTela.Clientes:
                    Clientes telaClientes = new();
                    telaClientes.Show();
                    break;
                case MoudlosTela.Produtos:
                    Produtos telaProdutos = new();
                    telaProdutos.Show();
                    break;
                case MoudlosTela.Fornecedores:
                    Fornecedores telaFornecedores = new();
                    telaFornecedores.Show();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(moduloTela), moduloTela, null);
            }
        }


    }
}