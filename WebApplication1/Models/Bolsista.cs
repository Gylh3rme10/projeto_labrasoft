using System;
using System.Runtime.Remoting.Messaging;

namespace WebApplication1.Models
{
    public class Bolsista
    {
        // EXERCÍCIO POO:
        // Com base no formulário que vocês criaram, definam as propriedades abaixo.
        // Lembrem-se de usar 'public', o tipo de dado (string, int, etc) e o { get; set; }

        public string Nome { get; set; }

        // TODO: Criar a propriedade para o CPF
        public string CPF { get; set; }

        // TODO: Criar a propriedade para a Matrícula
        public string Matricula { get; set; }

        // TODO: Criar a propriedade para a Data de Nascimento
        public DateTime Data_Nascimento { get; set; }

        // TODO: Criar a propriedade para o Sexo
        public char Sexo { get; set; }

        // TODO: Criar método com o resumo das informações contendo nome e matrícula
        public string RetornarResumo()
        {
            return $"Nome: {Nome} + Matricula: {Matricula}";
        }
        //TODO: Criar método que calcúla a idade do bolsista      
        public int CalcularIdade()
        {
            int idade = DateTime.Now.Year - Data_Nascimento.Year;

            return idade;
        }
    }
}
