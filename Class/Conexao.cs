using Microsoft.Data.SqlClient;

public class Conexao
{
    private static Conexao _instancia;
    private readonly string _stringConexao;

    private Conexao()
    {
        _stringConexao = @"Data Source=SRVRJ01;Initial Catalog=POS;User ID=sa;Password=inter#system;TrustServerCertificate=True";
    }

    public static Conexao Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = new Conexao();
            }
            return _instancia;
        }
    }

    public SqlConnection CriarConexao()
    {
        return new SqlConnection(_stringConexao);
    }
}