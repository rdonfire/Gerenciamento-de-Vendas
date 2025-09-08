using Gerenciador_de_Vendas.Class;
using System.Windows;
using System.Windows.Controls;
using static Gerenciador_de_Vendas.Class.ContextoDados;

namespace Gerenciador_de_Vendas
{
    public partial class Fornecedores : Window
    {
        DataeHora dataeHora = new();
        AcessoDados acessoDados = new();


        public Fornecedores()
        {
            InitializeComponent();

            dataeHora.atualizarDataHora(lblDataHora);

        }
        private enum TabModulos : int
        {
            tabFornecedores = 0,
            tabIncluirFornecedor,
            tabItemFornecedores,
        }

        private enum BtnAcao
        {
            Salvar = 0,
            Cancelar,
            Limpar,
            Sair,
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                switch (button.TabIndex) 
                {
                    case (int)BtnAcao.Salvar:
                        if (!ValidarCampos()) break;
                        GerarID();
                        PassarDados();
                        limparCampos();
                        break;
                    case (int)BtnAcao.Cancelar:
                        tabControlPrincipal.SelectedIndex = 0;
                        limparCampos();
                        break;
                    case (int)BtnAcao.Limpar:
                        if (string.IsNullOrEmpty(txtID.Text))
                            limparCampos();
                        break;
                    case (int)BtnAcao.Sair:
                        this.Close();
                        break; 
                }
            }
        }

        private void tabControlPrincipal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl? tabControl = sender as TabControl;

            switch (tabControl?.SelectedIndex)
            {
                case (int)TabModulos.tabFornecedores:
                        PreencherGrid();
                        limparCampos();
                        break;
                    
                case (int)TabModulos.tabIncluirFornecedor:
                    if (e.Source is TabControl && tabControl.SelectedIndex == (int)TabModulos.tabIncluirFornecedor)
                    {
                        if (!string.IsNullOrEmpty(txtID.Text))
                            btnLimpar.IsEnabled = false;
                        if (cmbUF.Items.Count == 0)
                        {
                            acessoDados.CarregarUFs(cmbUF);
                            if (cmbUF.Items.Count > 0)
                                cmbUF.SelectedIndex = 0;
                        }
                    }
                    break;
                case (int)TabModulos.tabItemFornecedores:
                    Produtos produtos = new();
                    produtos.Show();
                    this.Close();
                    break;
            }
        }

        private void PreencherGrid()
        {
            try
            {
                // Obtém a lista de fornecedores
                List<Fornecedor> fornecedores = acessoDados.ObterTodosFornecedores();

                // Vincula a lista ao DataGrid
                GridPrincipal.ItemsSource = fornecedores;
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PassarDados()
        {
            try
            {
                Fornecedor fornecedor = new()
                {
                    ID = int.Parse(txtID.Text),
                    Cnpj = txtCnpj.Text,
                    Nome = txtNomeFornecedor.Text,
                    Rua = txtEndereco.Text,
                    Complemento = txtComplemento.Text,
                    Bairro = txtBairro.Text,
                    IDUF = (int)cmbUF.SelectedValue,
                    DataCadastro = DateTime.Now,
                };

                acessoDados.IncluirFornecedor(fornecedor);
                MessageBox.Show("Fornecedor incluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro inesperado: " + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void GerarID()
        {
            string sSql = "SELECT TOP 1 ID_FORNECEDOR FROM FORNECEDOR ORDER BY DATACADASTRO DESC";
                        
            try
            {
                object UltimoID = acessoDados.PegarCampo(sSql);
                if (txtID.Text == "")
                {
                    if (UltimoID == null)
                        UltimoID = 0;
                    int NovoID = (int)UltimoID + 1;
                    txtID.Text = NovoID.ToString();
                }

            }
            catch (ApplicationException)
            {
                MessageBox.Show("Erro ao Gerar ID", "Error", MessageBoxButton.OK);
            }

        }

        private void limparCampos()
        {
            txtID.Text = string.Empty;
            txtCnpj.Text = string.Empty;
            txtBairro.Text = string.Empty;
            txtComplemento.Text = string.Empty;
            txtEndereco.Text = string.Empty;
            txtEstado.Text = string.Empty;
            txtNomeFornecedor.Text = string.Empty;
            cmbUF.SelectedIndex = 0;
        }

        private Boolean IsNumeric(string str)
        {
            
            Boolean ehNumerico = double.TryParse(str, out double numeroZero);

            if (ehNumerico)
                return true;

            return false;
        }
        private Boolean ValidarCampos()
        {
            //txtID
            if (txtCnpj.Text == "")
            {
                MessageBox.Show("Necessário preencher o CNPJ!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCnpj.Focus();
                return false;
            }
            if (IsNumeric(txtCnpj.Text) == false)
            {
                MessageBox.Show("Verifique o CNPJ, valido apenas Numeros!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtCnpj.Focus();
                return false;
            }
            if (txtNomeFornecedor.Text == "")
            {
                MessageBox.Show("Necessário informar a Razão Social!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNomeFornecedor.Focus();
                return false;
            }
            if (txtEndereco.Text == "")
            {
                MessageBox.Show("Necessário informar o Endereço!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEndereco.Focus();
                return false;
            }
            if (txtBairro.Text == "")
            {
                MessageBox.Show("Necessário informar o Bairro!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtBairro.Focus();
                return false;
            }
            if (txtEstado.Text == "")
            {
                MessageBox.Show("Necessário informar o Estado!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEstado.Focus();
                return false;
            }
            if (cmbUF.TabIndex < 0)
            {
                MessageBox.Show("Necessário informar o UF de estado!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbUF.Focus();
                return false;
            }

            return true;
        }

        
    }
}
