using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class BolsistaService
    {
        private readonly BolsistasRepository repository;

        public BolsistaService()
        {
            repository = new BolsistasRepository();
        }

        public void CadastrarBolsista(Bolsista bolsista)
        {
            // VALIDAÇÕES

            //verifica se o nome foi preenchido
            if (string.IsNullOrWhiteSpace(bolsista.Nome))
            {
                throw new Exception("O nome é obrigatório.");
            }

            //verifica se o CPF foi preenchido
            if (string.IsNullOrWhiteSpace(bolsista.CPF))
            {
                throw new Exception("O CPF é obrigatório.");
            }

            //verifica se a matricula foi preenchida
            if (string.IsNullOrWhiteSpace(bolsista.Matricula))
            {
                throw new Exception("A matrícula é obrigatória.");
            }

            //verifica se o sexo foi selecionado
            if (string.IsNullOrWhiteSpace(bolsista.Sexo))
            {
                throw new Exception("Selecione o sexo.");
            }

            //verifica se já existe CPF cadastrado
            if (repository.BolsistaJaCadastrado(bolsista.CPF))
            {
                throw new Exception("Já existe um bolsista cadastrado com este CPF.");
            }

            // Se passou pelas validações,
            // pode inserir no banco.

            repository.InserirBolsista(bolsista);

        }
    }
}