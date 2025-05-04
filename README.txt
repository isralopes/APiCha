# Desafio API REST .NET 8 com React Frontend

Este projeto consiste em uma API RESTful construída com .NET 8 e um frontend em React para consumir essa API. A API fornece funcionalidades CRUD para gerenciar recursos (substitua "recursos" pelo nome da sua entidade principal, ex: "produtos").

## Estrutura do Projeto

C:\Projetos\desafio

├── Projeto-api (Backend .NET 8)
│   ├── Controllers
│   │   └── [NomeDoSeuController].cs (Ex: ProdutosController.cs)
│   ├── Models
│   │   └── [NomeDoSeuModelo].cs (Ex: Produto.cs)
│   ├── Data
│   │   └── AppDbContext.cs
│   │   └── ... (outros arquivos de banco de dados)
│   ├── Migrations
│   │   └── ...
│   ├── ApiProject.csproj
│   ├── Program.cs
│   ├── appsettings.json
│   └── ... (outros arquivos do backend)
├── Projeto-api.Tests (Projeto de Testes Unitários xUnit)
│   ├── Tests
│   │   └── [NomeDaSuaClasseDeTeste].cs (Ex: ProdutosControllerTests.cs)
│   ├── Projeto-api.Tests.csproj
│   └── ... (outros arquivos de teste)
└── frontend (Frontend React)
│   ├── public
│   │   └── index.html
│   │   └── ...
│   ├── src
│   │   ├── App.js
│   │   ├── components
│   │   │   └── ...
│   │   ├── services
│   │   │   └── api.js (Ex: para comunicação com a API)
│   │   ├── index.js
│   │   ├── ... (outros arquivos do frontend)
│   ├── package.json
│   ├── yarn.lock (ou package-lock.json)
│   └── ... (outros arquivos do frontend)
└── README.md (Este arquivo)


## Documentação da API REST (.NET 8)

### Endpoints da API

A API base URL é `[Defina a URL base da sua API, ex: http://localhost:5000/api]`.

#### [Nome do seu Recurso] (Ex: Produtos)

* **GET** `/api/[nome do seu recurso]`
    * Descrição: Retorna uma lista de todos os [recursos].
    * Resposta (JSON):
        ```json
        [
            {
                "id": 1,
                "nome": "Nome do Recurso 1",
                "preco": 10.99,
                "...": "..."
            },
            {
                "id": 2,
                "nome": "Nome do Recurso 2",
                "preco": 25.50,
                "...": "..."
            }
        ]
        ```
    * Códigos de Status:
        * `200 OK`: Sucesso ao retornar a lista.

* **GET** `/api/[nome do seu recurso]/{id}`
    * Descrição: Retorna um [recurso] específico pelo seu ID.
    * Parâmetros de Rota:
        * `id` (inteiro): O ID do [recurso] a ser retornado.
    * Resposta (JSON):
        ```json
        {
            "id": 1,
            "nome": "Nome do Recurso 1",
            "preco": 10.99,
            "...": "..."
        }
        ```
    * Códigos de Status:
        * `200 OK`: Sucesso ao retornar o [recurso].
        * `404 Not Found`: [Recurso] não encontrado para o ID fornecido.

* **POST** `/api/[nome do seu recurso]`
    * Descrição: Cria um novo [recurso].
    * Corpo da Requisição (JSON):
        ```json
        {
            "nome": "Novo Nome do Recurso",
            "preco": 19.99,
            "...": "..."
        }
        ```
    * Resposta (JSON - Retorna o recurso criado):
        ```json
        {
            "id": 3,
            "nome": "Novo Nome do Recurso",
            "preco": 19.99,
            "...": "..."
        }
        ```
    * Códigos de Status:
        * `201 Created`: Sucesso ao criar o [recurso]. A resposta contém o [recurso] criado e o header `Location` com a URL do novo recurso.
        * `400 Bad Request`: A requisição é inválida (ex: dados faltando ou incorretos).

* **PUT** `/api/[nome do seu recurso]/{id}`
    * Descrição: Atualiza um [recurso] existente pelo seu ID.
    * Parâmetros de Rota:
        * `id` (inteiro): O ID do [recurso] a ser atualizado.
    * Corpo da Requisição (JSON):
        ```json
        {
            "nome": "Nome Atualizado do Recurso",
            "preco": 30.50,
            "...": "..."
        }
        ```
    * Códigos de Status:
        * `204 No Content`: Sucesso ao atualizar o [recurso].
        * `400 Bad Request`: A requisição é inválida.
        * `404 Not Found`: [Recurso] não encontrado para o ID fornecido.

* **DELETE** `/api/[nome do seu recurso]/{id}`
    * Descrição: Deleta um [recurso] existente pelo seu ID.
    * Parâmetros de Rota:
        * `id` (inteiro): O ID do [recurso] a ser deletado.
    * Códigos de Status:
        * `204 No Content`: Sucesso ao deletar o [recurso].
        * `404 Not Found`: [Recurso] não encontrado para o ID fornecido.

### Banco de Dados

O backend utiliza um banco de dados `[Nome do seu banco de dados, ex: SQLite]` para armazenar os dados. A configuração da conexão pode ser encontrada no arquivo `Projeto-api/appsettings.json`. As migrações do Entity Framework Core são usadas para gerenciar o schema do banco de dados.

### Testes Unitários

Os testes unitários para o backend estão localizados na pasta `Projeto-api/Projeto-api.Tests/Tests`. Eles utilizam o framework xUnit e simulam as dependências da controller (como o contexto do banco de dados e o logger) usando a biblioteca Moq.

**Exemplo de Teste em Projeto Separado:**

Para demonstrar o teste de uma controller de forma isolada, foi criado um projeto separado em `ProjetoTestesController` (dentro da pasta raiz do projeto). Este projeto contém um exemplo de como configurar um teste unitário para uma `ProdutosController`. O teste utiliza o framework xUnit para definir os testes e a biblioteca Moq para simular as dependências da controller, permitindo verificar o comportamento da lógica da controller sem depender de implementações concretas das suas dependências (como acesso real ao banco de dados).

## Documentação do Frontend (React)

### Visão Geral

O frontend é construído usando a biblioteca React e fornece uma interface de usuário para interagir com a API de [recursos]. Ele permite aos usuários visualizar, criar, editar e deletar [recursos].

### Estrutura de Pastas Principais

* `frontend/public/`: Contém arquivos estáticos como o `index.html`.
* `frontend/src/`: Contém o código-fonte da aplicação React.
    * `frontend/src/App.js`: O componente raiz da aplicação.
    * `frontend/src/components/`: Contém componentes reutilizáveis da interface do usuário (ex: formulários, listas, etc.).
    * `frontend/src/services/`: Contém módulos para interagir com a API (ex: `api.js` com funções para fazer chamadas HTTP).

### Comunicação com a API

O frontend se comunica com a API REST através de requisições HTTP (GET, POST, PUT, DELETE) utilizando a função `fetch` ou uma biblioteca como `axios` (se utilizada). As chamadas são geralmente feitas para a URL base da API definida anteriormente.

### Componentes Principais (Exemplos)

* `frontend/src/components/[NomeDoSeuRecurso]List.js`: Componente para exibir a lista de [recursos] obtidos da API.
* `frontend/src/components/[NomeDoSeuRecurso]Form.js`: Componente para criar e editar [recursos], enviando dados para a API.
* `frontend/src/components/[NomeDoSeuRecurso]Item.js`: Componente para exibir detalhes de um único [recurso].

### Gerenciamento de Estado (Se aplicável)

O gerenciamento de estado da aplicação pode ser feito utilizando o `useState` e `useEffect` do React, ou uma biblioteca de gerenciamento de estado como `Redux` ou `Context API` (se utilizada).

### Para Executar o Frontend

1.  Navegue até a pasta `frontend` no terminal.
2.  Execute `npm install` ou `yarn install` para instalar as dependências.
3.  Execute `npm start` ou `yarn start` para iniciar o servidor de desenvolvimento do React. A aplicação geralmente estará disponível em `http://localhost:3000`.

## Como Enviar o Código para o GitHub

1.  Crie um repositório no GitHub.
2.  Inicialize um repositório Git na raiz do seu projeto local (`C:\Projetos\desafio`):
    ```bash
    git init
    ```
3.  Adicione os arquivos ao repositório:
    ```bash
    git add .
    ```
4.  Faça o commit inicial:
    ```bash
    git commit -m "Initial commit - API .NET 8 e Frontend React com CRUD"
    ```
5.  Adicione o repositório remoto:
    ```bash
    git remote add origin [https://docs.github.com/articles/referencing-and-citing-content](https://docs.github.com/articles/referencing-and-citing-content)
    ```
6.  Envie o código para o GitHub:
    ```bash
    git push -u origin main
    ```

**Substitua os placeholders** (como `[Nome do seu Recurso]`, URL da API, nome do banco de dados, nomes de arquivos e componentes) com as informações reais do seu projeto.

**Para enviar todo o projeto para o GitHub, siga os passos em "Como Enviar o Código para o GitHub" acima.** Certifique-se de estar no diretório raiz do seu projeto (`C:\Projetos\desafio\`) ao executar os comandos `git add .`, `git commit` e `git push`.

Este arquivo `README.md` agora contém a documentação básica da sua API e do seu frontend, além das instruções para enviar o código para o GitHub. Coloque este arquivo na raiz do seu projeto.