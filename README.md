# Sistema de Gestão de Bolsas

## 📌 Sobre o Projeto

O **Sistema de Gestão de Bolsas** foi desenvolvido como projeto prático do programa de formação **LabraSoft**, voltado à capacitação de estudantes de Análise e Desenvolvimento de Sistemas (ADS) para o ambiente corporativo.

A aplicação simula o fluxo acadêmico-financeiro de uma fundação responsável pela intermediação de bolsas e financiamentos de pesquisa em universidades públicas. 

O sistema é construído progressivamente ao longo de oito semanas. **Atualmente, o projeto concluiu a Semana 1 (O Conceito de Objeto - POO)**, consolidando a introdução à Programação Orientada a Objetos e a ativação da lógica de back-end integrada às páginas dinâmicas.

-----

## 🎯 Objetivos do Projeto

- Aplicar conceitos de Programação Orientada a Objetos em C#;
- Desenvolver aplicações Web com ASP.NET WebForms;
- Implementar persistência de dados utilizando SQL Server;
- Trabalhar autenticação e segurança de aplicações;
- Utilizar padrões arquiteturais e boas práticas de desenvolvimento;
- Simular um ambiente corporativo real de desenvolvimento.

-----

## 🛠 Tecnologias Utilizadas (Estágio Atual)

### Frontend & Visual
- HTML5 / CSS3
- Bootstrap 5.3 (Integrado globalmente via CDN no `Site.Master`)

### Backend
- C#
- .NET Framework / ASP.NET WebForms (Páginas `.aspx`)

### Ferramentas
- Visual Studio / VS Code
- Git / GitLab

> 🚀 **Nota de Evolução:** Ao longo das próximas semanas, o projeto integrará *SQL Server*, *ADO.NET*, *BCrypt.Net-Next*, *JWT (JSON Web Token)* e *Gmail API*.

-----

## 📚 Funcionalidades Implementadas (Semana 1)

### 👨‍🏫 Gestão Acadêmica & POO
- **A Planta Baixa (Classes e Propriedades):** Criação da classe base `Bolsista.cs` dentro da pasta `Models`, definindo a estrutura de dados (molde) com propriedades como nome, datas e identificadores.
- **A Inteligência do Objeto (Métodos e Instância):** Implementação de lógica interna no objeto através do método `CalcularIdade()` e instanciação dinâmica via código utilizando o operador `new` no evento `Page_Load` da página de testes.
- **Interface Visual & Master Page:** Integração do layout visual acoplado à página mestre (`Site.Master`) utilizando Bootstrap 5, garantindo que a exibição dinâmica do objeto instanciado ocorra sob uma identidade visual unificada, responsiva e moderna.

-----
## 📁 Estrutura Atual do Projeto

```

labrasoft/
│
├── WebApplication1/
│   ├── Models/
│   │   └── Bolsista.cs               # Definição, propriedades e métodos da classe Bolsista
│   │
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   │
│   ├── Web/
│   │   └── Formulario.html           # Interface web estática legada de testes estruturais
│   │
│   ├── BolsistaExemplo.aspx          # Página de teste para instanciação e exibição do objeto
│   ├── BolsistaExemplo.aspx.cs       # Code-behind (C#) com a lógica de instanciação e uso de métodos
│   ├── BolsistaExemplo.aspx.designer.cs
│   │
│   ├── Site.Master                   # Página Mestre global com a integração do Bootstrap 5
│   ├── Site.Master.cs
│   ├── Site.Master.designer.cs
│   │
│   ├── packages.config
│   ├── Web.config
│   ├── Web.Debug.config
│   └── Web.Release.config
│
├── WebApplication1.sln               # Arquivo de solução do Visual Studio
├── .gitignore                        # Filtro de arquivos locais e binários para o Git
└── README.md                         # Documentação do projeto

```

## 📈 Cronograma de Aprendizado (Semana 1)

### Aula 1: A Planta Baixa (Classes e Propriedades)
* **Escopo:** Criação do projeto e mapeamento da pasta `Models`. Desenvolvimento do arquivo `Bolsista.cs`.
* **Conceitos:** Entendimento de Classes como moldes do mundo real, propriedades (`get; set;`) como características do objeto e tipagem de dados (`string`, `DateTime`, `int`).
* **Estado:** Definição estrutural da entidade de dados (back-end abstrato).

### Aula 2: A Inteligência do Objeto (Métodos e Instância)
* **Escopo:** Criação de métodos internos de processamento (ex: cálculo de idade baseado no ano corrente) e ativação do objeto em memória (`new`) através do ciclo de vida da página (`Page_Load`) na `BolsistaExemplo.aspx`.
* **Conceitos:** Manipulação de escopo, passagem de parâmetros, retorno de métodos e comportamento de objetos instanciados.
* **Estado:** Renderização dinâmica em tela de dados processados programaticamente pelo C# integrados ao layout padrão.

-----

## 🚀 Competências Desenvolvidas nesta Etapa

- Estruturação de projetos ASP.NET WebForms e arquiteturas base;
- Criação de classes, propriedades, métodos e instanciação de objetos em C# (POO);
- Customização de `Master Pages` corporativas utilizando layouts responsivos do Bootstrap 5;
- Controle de versão isolado por ambiente com arquivo `.gitignore` adequado;
- Versionamento de código e fluxos de ramificação (`branch`) com Git.

-----

## ▶ Como Executar o Projeto Atual

### Pré-requisitos
- Visual Studio (com a carga de trabalho para desenvolvimento Web ASP.NET e .NET Framework instalada)

### Passos
1. Clone o repositório da formação:
   git clone [https://gitlab.com/labrasoft.ifba/labrasoft.git](https://gitlab.com/labrasoft.ifba/labrasoft.git)

2. Abra o arquivo `WebApplication1.sln` no seu Visual Studio;
3. No Gerenciador de Soluções, clique com o botão direito sobre a página `BolsistaExemplo.aspx` e selecione **Definir como Página Inicial**;
4. Clique no botão de execução (IIS Express / Microsoft Edge ou Chrome) na barra superior para rodar o servidor local.

-----

## 👨‍💻 Equipe

Projeto desenvolvido durante o programa de formação LabraSoft por estudantes do curso de Análise e Desenvolvimento de Sistemas (ADS).