using Gerenciador_de_Vendas.Class;
using System.Windows;
using System.Windows.Controls;
using static Gerenciador_de_Vendas.Class.ContextoDados;
namespace Gerenciador_de_Vendas
{
   
    public partial class Produtos : Window
    {
        private DataeHora dataeHora = new();
        private AcessoDados acessoDados = new();

        private enum TabModulos
        {
            tabProdutos = 0,
            tabIncluirProdutos,
            tabCategoria,
            tabFornecedor,
        }
        public enum BtnAcao
        {
            Salvar = 0,
            Cancelar,
            Limpar,
            Sair,
            CalMarkup,
            CatSalvar,
            
        }
        public Produtos()
        {
            InitializeComponent();
            dataeHora.atualizarDataHora(lblDataHora);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                switch (button.TabIndex)
                {
                    case (int)BtnAcao.Salvar:
                        if(!ValidarCampos()) break;
                        GerarId();
                        //PASSARDADOS();
                        LimparCampos();
                        break;
                        
                    case (int)BtnAcao.Cancelar:
                        tabControlPrincipal.SelectedIndex = 0;
                        LimparCampos();
                        break;
                    case (int)BtnAcao.Limpar:
                        LimparCampos();
                        break;
                    case (int)BtnAcao.Sair:
                        this.Close();
                        break;
                    case (int)BtnAcao.CalMarkup:
                        CalcularMarkup();
                        break;
                    case (int)BtnAcao.CatSalvar:
                        if (!ValidarCampos()) break;
                        IncluirCategoria();
                        CarregarGridCategoria();
                        break;

                }
            }

        }

        private void tabControlPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl? tabControl = sender as TabControl;

            switch (tabControl?.SelectedIndex)
            {
                case (int)TabModulos.tabProdutos:
                    break;
                case (int)TabModulos.tabIncluirProdutos:
                    break;
                case (int)TabModulos.tabCategoria:
                    CarregarGridCategoria();
                    break;
                case (int)TabModulos.tabFornecedor:
                    Fornecedores fornecedores = new();
                    fornecedores.Show();
                    this.Close();
                    break;
            }
        }

        private void CarregarGridCategoria()
        {
            try
            {
                List<Categoria> categoria = acessoDados.ObterCategorias();

                gridCategoria.ItemsSource = categoria;

            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void IncluirCategoria()
        {
            try
            {
                Categoria categoria = new()
                {
                    NomeCategoria = txtCategoria.Text,
                    DataCadastro = DateTime.Now,
                };

                acessoDados.IncluirCategoria(categoria);
                MessageBox.Show("Categoria salva com sucesso!", "Salvar", MessageBoxButton.OK, MessageBoxImage.Information );
                txtCategoria.Text = "";
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }

        //gridCategoria
        private void GerarId()
        {
            throw new NotImplementedException();
        }

        private Boolean ValidarCampos()
        {
            
            if (tabControlPrincipal.SelectedIndex == (int)TabModulos.tabCategoria)
            {
                if (txtCategoria.Text == "")
                {
                    MessageBox.Show("Necessário preencher a Categoria!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtCategoria.Focus();
                    return false;
                }
                return true;
            }
            if (txtCdBarras.Text == "")
            {
                MessageBox.Show("Necessário preencher o Codigo de Barras!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCdBarras.Focus();
                return false;
            }
            if (txtProduto.Text == "")
            {
                MessageBox.Show("Necessário preencher o Nome do Produto!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtProduto.Focus();
                return false;
            }
            if (txtPrecoVenda.Text == "")
            {
                MessageBox.Show("Necessário preencher o valor do Produto!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPrecoVenda.Focus();
                return false;
            }
            //if (cmbCategoria.Text == "")
            //{
            //    MessageBox.Show("necessário selecionar uma Categoria!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    cmbCategoria.Focus();
            //    return false;
            //}
            //if (cmbFornecedor.Text == "")
            //{
            //    MessageBox.Show("necessário selecionar uma Fornecedor!", "alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    cmbFornecedor.Focus();
            //    return false;
            //}
            
            return true;

        }
            
        private void LimparCampos()
        {
            txtCdBarras.Text = "";
            txtProduto.Text = "";
            txtPrecoFornecedor.Text = "";
            txtPrecoVenda.Text = "";
            txtMarkup.Text = "";
            txtDescricao.Text = "";
            cmbCategoria.Text = "";
            cmbFornecedor.Text = "";
        }

        private void CalcularMarkup()
        {
            if (float.TryParse(txtPrecoFornecedor.Text, out float vlFornecedor) &&
                float.TryParse(txtMarkup.Text, out float vlMarkup))
            {
                float vlAtualizado = vlFornecedor * (1 + (vlMarkup / 100));
                txtPrecoVenda.Text = vlAtualizado.ToString("F2");
            }
            else
            {
                MessageBox.Show("Por favor, insira valores numéricos válidos para Preço Fornecedor e Markup.");
            }
            
        }
    }
}
