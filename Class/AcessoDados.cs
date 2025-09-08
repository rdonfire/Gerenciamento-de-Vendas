using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Gerenciador_de_Vendas.Class.ContextoDados;

namespace Gerenciador_de_Vendas.Class
{
    internal class AcessoDados
    {
        public void CarregarUFs(ComboBox comboBox)
        {
            // Crie a conexão e o comando dentro do bloco 'using' para garantir o fechamento.
            using (SqlConnection conexao = Conexao.Instancia.CriarConexao())
            {
                string sql = "SELECT Id_UF, Sigla_UF FROM UF ORDER BY Sigla_UF";

                using (SqlCommand cmd = new SqlCommand(sql, conexao))
                {
                    try
                    {
                        // O SqlDataAdapter abre e fecha a conexão automaticamente.
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();

                        // da.Fill() abre a conexão para executar a query e a fecha em seguida.
                        da.Fill(dt);

                        // Vinculação de dados no WPF
                        comboBox.ItemsSource = dt.DefaultView;
                        comboBox.DisplayMemberPath = "Sigla_UF";
                        comboBox.SelectedValuePath = "Id_UF";
                    }
                    catch (SqlException)
                    {
                        // Mensagem de erro.
                    }
                }
            } // O 'using' garante que a conexão é fechada ao sair do bloco.
        }

        public object PegarCampo(string parSql)
        {
            object sCampo;
            try
            {
                Conexao conexao = Conexao.Instancia;
                using (SqlConnection conn = conexao.CriarConexao())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(parSql, conn);
                    sCampo = cmd.ExecuteScalar();
                }
            }
            catch
            {
                throw;
            }

            return sCampo;
        }

        public void IncluirFornecedor(Fornecedor Fornecedor)
        {
           
            using (SqlConnection conn = Conexao.Instancia.CriarConexao())
            {
                string sSql = "INSERT INTO FORNECEDOR (ID_FORNECEDOR, CNPJ, NOME, RUA, COMPLEMENTO, BAIRRO, ID_UF, DATACADASTRO) " +
                                "VALUES(@id, @cnpj, @nome, @rua, @complemento, @bairro, @iduf, @dataCadastro)";

                using (SqlCommand cmd = new SqlCommand(sSql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", Fornecedor.ID);
                    cmd.Parameters.AddWithValue("@cnpj", Fornecedor.Cnpj);
                    cmd.Parameters.AddWithValue("@nome", Fornecedor.Nome);
                    cmd.Parameters.AddWithValue("@rua", Fornecedor.Rua);
                    cmd.Parameters.AddWithValue("@complemento", Fornecedor.Complemento);
                    cmd.Parameters.AddWithValue("@bairro", Fornecedor.Bairro);
                    cmd.Parameters.AddWithValue("@iduf", Fornecedor.IDUF);
                    cmd.Parameters.AddWithValue("@dataCadastro", Fornecedor.DataCadastro);
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                        {
                            conn.Open();
                        }
                        cmd.ExecuteNonQuery();
                    }
                    catch (Microsoft.Data.SqlClient.SqlException ex)
                    {
                        throw new ApplicationException("Erro ao salvar o fornecedor. Verifique os dados e tente novamente.", ex);
                    }
                
                }
            }
            
        }

        public List<Fornecedor> ObterTodosFornecedores()
        {
            List<Fornecedor> fornecedores = new ();

            using (SqlConnection conexao = Conexao.Instancia.CriarConexao())
            {
                string sSql = "SELECT ID_FORNECEDOR, CNPJ, NOME, RUA, COMPLEMENTO, BAIRRO, ID_UF, DATACADASTRO FROM Fornecedor";

                using (SqlCommand cmd = new SqlCommand(sSql, conexao))
                {
                    try
                    {
                        if (conexao.State != System.Data.ConnectionState.Open)
                            conexao.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Fornecedor fornecedor = new()
                            {
                                ID = reader.GetInt32(0),
                                Cnpj = reader.GetString(1),
                                Nome = reader.GetString(2),
                                Rua = reader.GetString(3),
                                Complemento = reader.GetString(4),
                                Bairro = reader.GetString(5),
                                IDUF = reader.GetInt32(6),
                                DataCadastro = reader.GetDateTime(7)
                            };
                            fornecedores.Add(fornecedor);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new ApplicationException("Erro ao buscar fornecedores. " + ex.Message, ex);
                    }
                }
            }

            return fornecedores;
        }
        

        //Incluir Produtos
        //ObterProdutos

        public void IncluirCategoria(Categoria Categoria)
        {
            using (SqlConnection conn = Conexao.Instancia.CriarConexao())
            {
                string sSql = "INSERT INTO CATEGORIA ( NOME_CAT, DATACADASTRO) VALUES (@NOME_CAT, @DATACADASTRO)";
                using  (SqlCommand cmd = new (sSql, conn))
                {
                    cmd.Parameters.AddWithValue("@NOME_CAT", Categoria.NomeCategoria);
                    cmd.Parameters.AddWithValue("@DATACADASTRO", Categoria.DataCadastro);

                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        cmd.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        public List<Categoria> ObterCategorias()
        {
            List<Categoria> categorias = new();

            using (SqlConnection conn = Conexao.Instancia.CriarConexao())
            {
                string sSql = "SELECT * FROM CATEGORIA ORDER BY DATACADASTRO DESC";

                using (SqlCommand cmd = new(sSql, conn))
                {
                    try
                    {
                        if(conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Categoria categoria = new()
                            {
                                IDCat = reader.GetInt32(0),
                                NomeCategoria = reader.GetString(1),
                                DataCadastro = reader.GetDateTime(2)
                            };
                            categorias.Add(categoria);
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new ApplicationException("Erro ao buscar categorias! " + ex.Message, ex);
                    }
                }
            }

            return categorias;
        }
    }
}