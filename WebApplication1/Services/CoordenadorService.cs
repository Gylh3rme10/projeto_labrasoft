using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class CoordenadorService
    {
        private readonly CoordenadoresRepository repository;

        public CoordenadorService()
        {
            repository = new CoordenadoresRepository();
        }

        public void CadastrarCoordenador(Coordenador coordenador)
        {
            // VALIDAÇÕES

            //verifica se o nome foi preenchido
            if (string.IsNullOrWhiteSpace(coordenador.Nome))
            {
                throw new Exception("O nome é obrigatório.");
            }

            //verifica se o CPF foi preenchido
            if (string.IsNullOrWhiteSpace(coordenador.CPF))
            {
                throw new Exception("O CPF é obrigatório.");
            }

            //verifica se o email foi preenchido
            if (string.IsNullOrWhiteSpace(coordenador.Email))
            {
                throw new Exception("O email é obrigatória.");
            }

            //verifica se a titulação foi preenchida
            if (string.IsNullOrWhiteSpace(coordenador.Titulacao))
            {
                throw new Exception("A titulação é obrigatória.");
            }

            //verifica se a area de atuação foi preenchida
            if (string.IsNullOrWhiteSpace(coordenador.AreaAtuacao))
            {
                throw new Exception("A area de atuação é obrigatória.");
            }
            //verifica se já existe CPF cadastrado
            if (repository.CoordenadorJaCadastrado(coordenador.CPF))
            {
                throw new Exception("Já existe um coordenador cadastrado com este CPF.");
            }

            // Se passou pelas validações,
            // pode inserir no banco.

            repository.InserirCoordenador(coordenador);

        }
    }
}