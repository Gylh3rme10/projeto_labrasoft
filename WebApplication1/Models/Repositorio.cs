using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public static class Repositorio
    {   
        //TESTAR CONEXAO
        private static string strConexao = ConfigurationManager.ConnectionStrings["Banco"].ConnectionString;
        public static string TestarConexao()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(strConexao))
                conexao.Open();
                return "Conexão realizada com sucesso";
            }
            catch (Exception ex)
            {
                return "Erro ao conectar: " + ex.Message;
            }
        }

        //CADASTRO DOS BOLSISTAS
        public static void InserirBolsista(Bolsista b)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = @"INSERT INTO Bolsista
                       (Nome, CPF, Matricula, DataNascimento, Sexo)
                       VALUES
                       (@Nome,@CPF,@Matricula,@DataNascimento,@Sexo)";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Nome", b.Nome);
                cmd.Parameters.AddWithValue("@CPF", b.CPF);
                cmd.Parameters.AddWithValue("@Matricula", b.Matricula);
                cmd.Parameters.AddWithValue("@DataNascimento", b.DataNascimento);
                cmd.Parameters.AddWithValue("@Sexo", b.Sexo);

                cmd.ExecuteNonQuery();
            }
        }
        public static List<Bolsista> ListarBolsistas()
        {
            List<Bolsista> lista = new List<Bolsista>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT * FROM Bolsista";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Bolsista b = new Bolsista
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Nome = dr["Nome"].ToString(),
                        CPF = dr["CPF"].ToString(),
                        Matricula = dr["Matricula"].ToString(),
                        DataNascimento = Convert.ToDateTime(dr["DataNascimento"]),
                        Sexo = dr["Sexo"].ToString()
                    };

                    lista.Add(b);
                }
            }

            return lista;
        }
        public static bool BolsistaExiste(string cpf)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT COUNT(*) FROM Bolsista WHERE CPF = @CPF";

                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@CPF", cpf);

                int quantidade = (int)cmd.ExecuteScalar();

                return quantidade > 0;
            }
        }
        public static void TestarPreencherBanco()
        {
            foreach (Bolsista b in ListarBolsistas())
            {
                if (!BolsistaExiste(b.CPF))
                {
                    InserirBolsista(b);
                }
            }
        }
        // CADASTRO DOS COORDENADORES
        public static void InserirCoordenador(Coordenador c)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = @"INSERT INTO Coordenador
                      (Nome, CPF, Titulacao, AreaAtuacao, Email)
                      VALUES
                      (@Nome,@CPF,@Titulacao,@Area,@Email)";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Nome", c.Nome);
                cmd.Parameters.AddWithValue("@CPF", c.CPF);
                cmd.Parameters.AddWithValue("@Titulacao", c.Titulacao);
                cmd.Parameters.AddWithValue("@Area", c.AreaAtuacao);
                cmd.Parameters.AddWithValue("@Email", c.Email);

                cmd.ExecuteNonQuery();
            }
        }
        public static List<Coordenador> ListarCoordenadores()
        {
            List<Coordenador> lista = new List<Coordenador>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT * FROM Coordenador";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Coordenador c = new Coordenador
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Nome = dr["Nome"].ToString(),
                        CPF = dr["CPF"].ToString(),
                        Titulacao = dr["Titulacao"].ToString(),
                        AreaAtuacao = dr["AreaAtuacao"].ToString(),
                        Email = dr["Email"].ToString()
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }
        public static bool CoordenadorExiste(string cpf)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT COUNT(*) FROM Coordenador WHERE CPF=@CPF";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@CPF", cpf);

                int quantidade = (int)cmd.ExecuteScalar();

                return quantidade > 0;
            }
        }
       //CADASTRO DOS PROJETOS
        public static List<Projeto> Projetos { get; } = new List<Projeto>();

        public static void InserirProjeto(Projeto p)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                SqlTransaction transacao = conexao.BeginTransaction();

                try
                {
                    // Insere o projeto
                    string sqlProjeto = @"
                        INSERT INTO Projeto
                        (Titulo, AreaConhecimento, VerbaAprovada, CPFCoordenador)

                        VALUES

                        (@Titulo, @AreaConhecimento, @VerbaAprovada, @CPFCoordenador);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdProjeto = new SqlCommand(sqlProjeto, conexao, transacao);

                    cmdProjeto.Parameters.AddWithValue("@Titulo", p.Titulo);
                    cmdProjeto.Parameters.AddWithValue("@AreaConhecimento", p.AreaConhecimento);
                    cmdProjeto.Parameters.AddWithValue("@VerbaAprovada", p.VerbaAprovada);
                    cmdProjeto.Parameters.AddWithValue("@CPFCoordenador", p.Coordenadores.CPF);

                    int idProjeto = Convert.ToInt32(cmdProjeto.ExecuteScalar());

                    // Insere cada bolsista
                    foreach (Bolsista b in p.Bolsistas)
                    {
                        string sqlBolsista = @"
                            INSERT INTO ProjetoBolsista
                            (IdProjeto, CPFBolsista)

                            VALUES

                            (@IdProjeto, @CPFBolsista)";

                        SqlCommand cmdBolsista =
                            new SqlCommand(sqlBolsista, conexao, transacao);

                        cmdBolsista.Parameters.AddWithValue("@IdProjeto", idProjeto);
                        cmdBolsista.Parameters.AddWithValue("@CPFBolsista", b.CPF);

                        cmdBolsista.ExecuteNonQuery();
                    }

                    transacao.Commit();
                }
                catch
                {
                    transacao.Rollback();
                    throw;
                }
            }
        }
        public static List<Projeto> ListarProjetos()
        {
            List<Projeto> lista = new List<Projeto>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT * FROM Projeto";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Projeto p = new Projeto();

                    p.Id = Convert.ToInt32(dr["Id"]);
                    p.Titulo = dr["Titulo"].ToString();
                    p.AreaConhecimento = dr["AreaConhecimento"].ToString();
                    p.VerbaAprovada = Convert.ToDecimal(dr["VerbaAprovada"]);

                    lista.Add(p);
                }

                dr.Close();
            }

            return lista;
        }
    }
}