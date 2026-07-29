using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{
    public class Despesas
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public string Categoria { get; set; }
        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public int IdProjeto { get; set; }

    }
}