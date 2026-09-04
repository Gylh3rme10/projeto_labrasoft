using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    // CADASTRO DOS COORDENADORES
    public class CoordenadoresRepository : BaseRepository
    {
        // INSERE COORDENADOR NO BANCO
        public void InserirCoordenador(Coordenador c)
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
        //LISTAR COORDENADORES - LISTA COMPLETA
        public List<Coordenador> ListarCoordenadores()
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
        //LISTAR COORDENADORES - LISTA RESUMIDA PARA GRID
        public List<CoordenadorGridDTO> ListarCoordenadoresGrid()
        {
            List<CoordenadorGridDTO> lista = new List<CoordenadorGridDTO>();

            using (SqlConnection conexao = new SqlConnection(strConexao))
            {
                conexao.Open();

                string sql = "SELECT ID, Nome, Email, Titulacao FROM Coordenador";

                SqlCommand cmd = new SqlCommand(sql, conexao);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    CoordenadorGridDTO c = new CoordenadorGridDTO
                    {
                        Id = Convert.ToInt32(dr["ID"]),
                        Nome = dr["Nome"].ToString(),
                        Email = dr["Email"].ToString(),
                        Titulacao = dr["Titulacao"].ToString()
                    };

                    lista.Add(c);
                }
            }

            return lista;
        }
        //VERIFICAR SE COORDENADOR ESTÁ CADASTRADO
        public bool CoordenadorJaCadastrado(string cpf)
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
    }
}