using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    //CADASTRO DOS BOLSISTAS
    public class BolsistasRepository : BaseRepository
    {
        //INSERIR BOLSISTA NO BANCO
        public void InserirBolsista(Bolsista b)
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
        //LISTAR BOLSISTAS - LISTA COMPLETA
        public List<Bolsista> ListarBolsistas()
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
        //LISTAR BOLSISTAS - LISTA RESUMIDA PARA GRID
        public List<BolsistaGridDTO> ListarBolsistasGrid()
        {
            List<BolsistaGridDTO> lista = new List<BolsistaGridDTO>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT ID, Nome, Matricula FROM Bolsista";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    BolsistaGridDTO b = new BolsistaGridDTO
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Nome = dr["Nome"].ToString(),
                        Matricula = dr["Matricula"].ToString()
                    };

                    lista.Add(b);
                }
            }

            return lista;
        }
        //VERIFICAR SE BOLSISTA ESTÁ CADASTRADO
        public bool BolsistaJaCadastrado(string cpf)
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
        //TESTE
        public void TestarPreencherBanco()
        {
            foreach (Bolsista b in ListarBolsistas())
            {
                if (!BolsistaJaCadastrado(b.CPF))
                {
                    InserirBolsista(b);
                }
            }
        }
    }
}