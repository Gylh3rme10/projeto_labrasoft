# Sistema de Gestão de Bolsas

## 📌 Sobre o Projeto

O **Sistema de Gestão de Bolsas** foi desenvolvido como projeto prático do programa de formação **LabraSoft**, voltado à capacitação de estudantes de Análise e Desenvolvimento de Sistemas (ADS) para o ambiente corporativo.

A aplicação simula o fluxo acadêmico-financeiro de uma fundação responsável pela intermediação de bolsas e financiamentos de pesquisa em universidades públicas. 

O sistema será construído progressivamente ao longo de oito semanas. **Atualmente, o projeto encontra-se na Semana 1 (Aula Inicial)**, focada na estrutura visual inicial e na criação do modelo de dados base.

-----

## 🎯 Objetivos do Projeto

- Aplicar conceitos de Programação Orientada a Objetos em C#;
- Desenvolver aplicações Web com ASP.NET;
- Implementar persistência de dados utilizando SQL Server;
- Trabalhar autenticação e segurança de aplicações;
- Utilizar padrões arquiteturais e boas práticas de desenvolvimento;
- Simular um ambiente corporativo real de desenvolvimento.

-----

## 🛠 Tecnologias Utilizadas (Estágio Atual)

### Frontend & Visual
- HTML5 / CSS3
- Bootstrap 5.3 (via CDN)

### Backend
- C#
- .NET Framework / ASP.NET

### Ferramentas
- Visual Studio
- Git / GitHub / GitLab

> 🚀 **Nota de Evolução:** Ao longo das próximas semanas, o projeto integrará *SQL Server*, *ADO.NET*, *BCrypt.Net-Next*, *JWT (JSON Web Token)* e *Gmail API*.

-----

## 📚 Funcionalidades Implementadas (Semana 1)

### 👨‍🏫 Gestão Acadêmica
- **Estrutura do Modelo:** Criação da classe base do Bolsista (Bolsista.cs).
- **Interface Visual:** Formulário HTML inicial para o cadastro de bolsistas (Formulario.html).

-----

## 📁 Estrutura Atual do Projeto

```
labrasoft/
│
├── WebApplication1/
│   ├── Models/
│   │   └── Bolsista.cs          # Modelo inicial da entidade Bolsista
│   │
│   ├── Properties/
│   │
│   ├── Web/
│   │   └── Formulario.html      # Formulário de cadastro de bolsista (Bootstrap 5)
│   │
│   ├── packages.config
│   └── Web.config
│
├── WebApplication1.sln          # Solução do Visual Studio
└── README.md

```

## 📈 Próximos Passos & Cronograma de Evolução

O projeto foi desenhado sob a metodologia **Project-Based Learning (PBL)** e evoluirá nas próximas semanas para conter:

* **Arquitetura em Camadas:** Migração para o padrão MVC (Separando Models, Repositories e Services).
* **Banco de Dados:** Modelagem relacional no SQL Server e integração via ADO.NET.
* **Segurança:** Sistema de Login com criptografia de senhas (BCrypt) e autenticação via JWT.
* **Integrações:** Envio automático de e-mails via Gmail API.

-----

## 🚀 Competências Desenvolvidas nesta Etapa

- Estruturação inicial de projetos ASP.NET;
- Criação de classes e propriedades em C# (POO);
- Construção de interfaces responsivas com Bootstrap 5;
- Versionamento de código e fluxo de trabalho com Git.

-----

## ▶ Como Executar o Projeto Atual

### Pré-requisitos

- Visual Studio (com suporte a desenvolvimento Web/.NET Framework)

### Passos

1. Clone o repositório:
   git clone [https://gitlab.com/labrasoft.ifba/labrasoft.git](https://gitlab.com/labrasoft.ifba/labrasoft.git)
   
2. Abra o arquivo WebApplication1.sln no Visual Studio;
3. Navegue até a pasta Web e abra o arquivo Formulario.html;
4. Execute o projeto (utilizando o IIS Express / Microsoft Edge/Chrome).

-----

## 👨‍💻 Equipe

Projeto desenvolvido durante o programa de formação LabraSoft por estudantes do curso de Análise e Desenvolvimento de Sistemas (ADS).