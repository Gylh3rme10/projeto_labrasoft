using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Projeto
    {   public int Id { get; set; }
        public string Titulo { get; set; }

        public string AreaConhecimento { get; set; }

        public decimal VerbaAprovada { get; set; }

        public decimal ValorBolsaIndividual { get; set; }

        public Coordenador Coordenadores { get; set; }

        public List<Bolsista> Bolsistas { get; set; }

        public Projeto()
        {
            Bolsistas = new List<Bolsista>();
        }
    }
}