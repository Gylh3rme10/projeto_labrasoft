using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace WebApplication1.Models
{
    public static class Repositorio
    {
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

        public static List<Coordenador> Coordenadores { get; } = new List<Coordenador>
        {
            new Coordenador
            {
                Nome = "Carlos Henrique",
                CPF = "666.666.666-66",
                Titulacao = "Doutor",
                AreaDeAtuacao = "Inteligência Artificial",
                Email = "carlos@universidade.br"
            },
            new Coordenador
            {
                Nome = "Fernanda Lima",
                CPF = "777.777.777-77",
                Titulacao = "Mestre",
                AreaDeAtuacao = "Banco de Dados",
                Email = "fernanda@universidade.br"
            },
            new Coordenador
            {
                Nome = "Ricardo Souza",
                CPF = "888.888.888-88",
                Titulacao = "Doutor",
                AreaDeAtuacao = "Engenharia de Software",
                Email = "ricardo@universidade.br"
            },
            new Coordenador
            {
                Nome = "Patrícia Gomes",
                CPF = "999.999.999-99",
                Titulacao = "Especialista",
                AreaDeAtuacao = "Redes de Computadores",
                Email = "patricia@universidade.br"
            },
            new Coordenador
            {
                Nome = "Marcos Pereira",
                CPF = "000.000.000-00",
                Titulacao = "Doutor",
                AreaDeAtuacao = "Segurança da Informação",
                Email = "marcos@universidade.br"
            }
        };
        public static List<Projeto> Projetos { get; } = new List<Projeto>();

    }
}