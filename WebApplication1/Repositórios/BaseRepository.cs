using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public abstract class BaseRepository
    {
        protected readonly string strConexao;

        public BaseRepository()
        {
            strConexao = ConfigurationManager.ConnectionStrings["Banco"].ConnectionString;

            if (string.IsNullOrEmpty(strConexao))
            {
                throw new Exception("String de conexão não encontrada.");
            }
        }

    }
}