using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public static class Repositorio
    {
        public static List<Bolsista> Bolsistas { get; } = new List<Bolsista>
        {
            new Bolsista
            {
                Nome = "João Silva",
                CPF = "111.111.111-11",
                Matricula = "2024001",
                DataNascimento = new DateTime(2002, 5, 15),
                Sexo = "M"
            },
            new Bolsista
            {
                Nome = "Maria Oliveira",
                CPF = "222.222.222-22",
                Matricula = "2024002",
                DataNascimento = new DateTime(2001, 8, 20),
                Sexo = "F"
            },
            new Bolsista
            {
                Nome = "Pedro Santos",
                CPF = "333.333.333-33",
                Matricula = "2024003",
                DataNascimento = new DateTime(2003, 2, 10),
                Sexo = "M"
            },
            new Bolsista
            {
                Nome = "Ana Costa",
                CPF = "444.444.444-44",
                Matricula = "2024004",
                DataNascimento = new DateTime(2000, 11, 30),
                Sexo = "F"
            },
            new Bolsista
            {
                Nome = "Lucas Almeida",
                CPF = "555.555.555-55",
                Matricula = "2024005",
                DataNascimento = new DateTime(2002, 7, 5),
                Sexo = "M"
            }
        };

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