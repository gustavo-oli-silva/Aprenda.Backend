# Aprenda.Backend API

Uma API RESTful desenvolvida em ASP.NET Core para gerenciar um ambiente de aprendizado online, similar a uma sala de aula virtual. A plataforma permite que professores criem turmas, postem materiais, atribuam tarefas e avaliem os envios dos alunos.

## Stack Utilizada

<span>
<img src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET">
<img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="C#">
<img src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" alt="SQL Server">
<img src="https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white" alt="Docker">
</span>

## Rodando Localmente 🖥️

Para executar o projeto em seu ambiente local, siga os passos abaixo.

### Pré-requisitos

  - .NET 8 SDK
  - Docker e Docker Compose
  - Um cliente SQL Server (opcional, para visualização do banco)

### Passos

1.  **Clone o repositório:**

    ```sh
    git clone https://github.com/gustavo-oli-silva/aprenda.backend
    ```

2.  **Entre no diretório do repositório:**

    ```sh
    cd aprenda.backend
    ```

3.  **Inicie o container do banco de dados com Docker:**

    Crie um arquivo `docker-compose.yml` na raiz do projeto com o seguinte conteúdo:

    ```yml
    version: '3.8'
    services:
      sqlserver:
        image: mcr.microsoft.com/mssql/server:2019-latest
        container_name: aprenda-db
        environment:
          SA_PASSWORD: "SenhaForte2025"
          ACCEPT_EULA: "Y"
        ports:
          - "1433:1433"
    ```

    Em seguida, suba o container:

    ```sh
    docker-compose up -d
    ```

4.  **Aplique as Migrations do Entity Framework:**

    Navegue até a pasta do projeto e execute o comando para aplicar as migrations, que criarão o schema do banco de dados.

    ```sh
    cd Aprenda.Backend
    dotnet ef database update
    ```

5.  **Execute a Aplicação:**

    Ainda no diretório `Aprenda.Backend`, execute o projeto:

    ```sh
    dotnet run
    ```

6.  A aplicação estará disponível em `http://localhost:5183`. A documentação da API, gerada pelo Swagger, pode ser acessada em `http://localhost:5183/swagger`.

## Estrutura de Pastas

A estrutura do projeto foi organizada para manter uma clara separação de responsabilidades (Separation of Concerns), facilitando a manutenção e escalabilidade.

```
/Aprenda.Backend
    /Controllers    # Define os endpoints da API (rotas)
    /Data           # Contém o AppDbContext para configuração do Entity Framework
    /Dtos           # Data Transfer Objects para a comunicação com o cliente
    /Mappers        # Conversores entre Models e DTOs
    /Migrations     # Migrations do Entity Framework para o banco de dados
    /Models         # Classes que representam as entidades do banco de dados
    /Repositories   # Camada de acesso aos dados (interação com o DbContext)
    /Services       # Contém a lógica de negócio da aplicação
    /Utils          # Funções utilitárias (ex: conversor de data)
    Program.cs      # Ponto de entrada e configuração da aplicação
```
