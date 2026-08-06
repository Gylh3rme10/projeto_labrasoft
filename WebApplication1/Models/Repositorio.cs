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
                        (Titulo, AreaConhecimento, VerbaAprovada, ValorBolsaIndividual, CoordenadorID)

                        VALUES

                        (@Titulo, @AreaConhecimento, @VerbaAprovada,@ValorBolsaIndividual, @CoordenadorID);

                        SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdProjeto = new SqlCommand(sqlProjeto, conexao, transacao);

                    cmdProjeto.Parameters.AddWithValue("@Titulo", p.Titulo);
                    cmdProjeto.Parameters.AddWithValue("@AreaConhecimento", p.AreaConhecimento);
                    cmdProjeto.Parameters.AddWithValue("@VerbaAprovada", p.VerbaAprovada);
                    cmdProjeto.Parameters.AddWithValue("@ValorBolsaIndividual", p.ValorBolsaIndividual);
                    cmdProjeto.Parameters.AddWithValue("@CoordenadorID", p.Coordenadores.Id);

                    int idProjeto = Convert.ToInt32(cmdProjeto.ExecuteScalar());

                    // Insere cada bolsista
                    foreach (Bolsista b in p.Bolsistas)
                    {
                        string sqlBolsista = @"
                            INSERT INTO ProjetoBolsista
                            (ProjetoID, BolsistaID)

                            VALUES

                            (@ProjetoID, @BolsistaID)";

                        SqlCommand cmdBolsista =
                            new SqlCommand(sqlBolsista, conexao, transacao);

                        cmdBolsista.Parameters.AddWithValue("@ProjetoID", idProjeto);
                        cmdBolsista.Parameters.AddWithValue("@BolsistaID", b.Id);

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

                string sql = @"
            SELECT
                P.ID,
                P.Titulo,
                P.AreaConhecimento,
                P.VerbaAprovada,
                P.CoordenadorID,

                C.ID AS CoordID,
                C.Nome,
                C.CPF,
                C.Titulacao,
                C.AreaAtuacao,
                C.Email

            FROM Projeto P

            INNER JOIN Coordenador C
                ON P.CoordenadorID = C.ID";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Projeto projeto = new Projeto();

                    projeto.Id = Convert.ToInt32(dr["ID"]);
                    projeto.Titulo = dr["Titulo"].ToString();
                    projeto.AreaConhecimento = dr["AreaConhecimento"].ToString();
                    projeto.VerbaAprovada = Convert.ToDecimal(dr["VerbaAprovada"]);

                    projeto.Coordenadores = new Coordenador
                    {
                        Id = Convert.ToInt32(dr["CoordID"]),
                        Nome = dr["Nome"].ToString(),
                        CPF = dr["CPF"].ToString(),
                        Titulacao = dr["Titulacao"].ToString(),
                        AreaAtuacao = dr["AreaAtuacao"].ToString(),
                        Email = dr["Email"].ToString()
                    };

                    lista.Add(projeto);
                }

                dr.Close();

                //-------------------------------------------------
                // Agora busca os bolsistas de cada projeto
                //-------------------------------------------------

                foreach (Projeto projeto in lista)
                {
                    string sqlBolsistas = @"

                SELECT
                    B.ID,
                    B.Nome,
                    B.CPF,
                    B.Matricula,
                    B.DataNascimento,
                    B.Sexo

                FROM ProjetoBolsista PB

                INNER JOIN Bolsista B
                    ON PB.BolsistaID = B.ID

                WHERE PB.ProjetoID = @ProjetoID";

                    SqlCommand cmdBolsistas =
                        new SqlCommand(sqlBolsistas, conexao);

                    cmdBolsistas.Parameters.AddWithValue("@ProjetoID", projeto.Id);

                    SqlDataReader drBolsistas =
                        cmdBolsistas.ExecuteReader();

                    while (drBolsistas.Read())
                    {
                        projeto.Bolsistas.Add(new Bolsista
                        {
                            Id = Convert.ToInt32(drBolsistas["ID"]),
                            Nome = drBolsistas["Nome"].ToString(),
                            CPF = drBolsistas["CPF"].ToString(),
                            Matricula = drBolsistas["Matricula"].ToString(),
                            DataNascimento = Convert.ToDateTime(drBolsistas["DataNascimento"]),
                            Sexo = drBolsistas["Sexo"].ToString()
                        });
                    }

                    drBolsistas.Close();
                }
            }

            return lista;
        }
        //DESPESAS
        public static void InserirDespesa(Despesas d)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = @"INSERT INTO Despesa
                      (Valor, Descricao, Categoria, Data, ProjetoId)
                      VALUES
                      (@Valor,@Descricao,@Categoria, @Data, @ProjetoId)";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@Valor", d.Valor);
                cmd.Parameters.AddWithValue("@Descricao", d.Descricao);
                cmd.Parameters.AddWithValue("@Categoria", d.Categoria);
                cmd.Parameters.AddWithValue("@Data", d.Data);
                cmd.Parameters.AddWithValue("@ProjetoId", d.IdProjeto);

                cmd.ExecuteNonQuery();
            }
        }
        public static decimal TotalDespesasProjeto(int idProjeto)
        {
            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = @"SELECT ISNULL(SUM(Valor), 0)
                       FROM Despesa
                       WHERE ProjetoId = @ProjetoId";

                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@ProjetoId", idProjeto);

                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
        public static List<Despesas> ListarDespesasPorProjeto(int idProjeto)
        {
            List<Despesas> lista = new List<Despesas>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = @"SELECT Id, Valor, Descricao, Categoria, Data
                       FROM Despesa
                       WHERE ProjetoId = @ProjetoId";

                SqlCommand cmd = new SqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@ProjetoId", idProjeto);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Despesas d = new Despesas
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Valor = Convert.ToDecimal(dr["Valor"]),
                        Descricao = dr["Descricao"].ToString(),
                        Categoria = dr["Categoria"].ToString(),
                        Data = Convert.ToDateTime(dr["Data"])
                    };

                    lista.Add(d);
                }
            }

            return lista;
        }
    }
}