# OrganizaCaixas API

A **OrganizaCaixas API** é um microserviço web desenvolvido em **.NET Core** que automatiza o processo de embalagem de pedidos. Dada uma lista de pedidos, cada um com produtos e suas dimensões, a API determina quais caixas pré-fabricadas devem ser usadas para cada pedido e quais produtos devem ser alocados em cada caixa, visando a otimização do espaço. Os dados de histórico são persistidos em um banco de dados **SQL Server**.

## Demonstração

Veja o vídeo abaixo para ver o funcionamento da OrganizaCaixas API:

[![Demonstração do Organiza Caixas API](https://img.youtube.com/vi/gSxnRsJxzc0/0.jpg)](https://www.youtube.com/watch?v=gSxnRsJxzc0&ab_channel=GuilhermeAugusto)

## Funcionalidades

- **Otimização de Empacotamento:** Determina a melhor forma de empacotar produtos em caixas, minimizando o número de caixas utilizadas.
- **Processamento em Lote:** Aceita e processa múltiplos pedidos simultaneamente.
- **Histórico de Pedidos:** Armazena os resultados de empacotamento em um banco de dados **SQL Server** para consulta futura.
- **API RESTful:** Expõe endpoints HTTP para processar pedidos e consultar o histórico.
- **Tratamento de Produtos Grandes:** Identifica e sinaliza produtos que não cabem em nenhuma das caixas disponíveis.

## Estrutura do Projeto

- **OrganizaCaixas.API**: Contém a lógica principal da API, controladores, serviços, DTOs e entidades de banco de dados.

## Tecnologias

- **.NET 9.0** (para o microserviço back-end)
- **C#** (para a lógica de programação)
- **Entity Framework Core** (para interação com o banco de dados)
- **SQL Server** (como sistema de gerenciamento de banco de dados)
- **Swagger/OpenAPI** (para documentação interativa da API)
- **Docker e Docker Compose** (para containerização)

---

## Como Rodar o Projeto Localmente

Você pode rodar a **OrganizaCaixas API** de duas formas:

- **Modo tradicional:** rodando o projeto localmente com .NET SDK e SQL Server instalado.
- **Modo Docker:** rodando a API e o banco SQL Server via containers Docker usando `docker-compose`.

---

### Pré-requisitos

- **Docker** instalado e funcionando (recomendado para modo Docker)
- **.NET SDK 9.0** ou superior (para rodar localmente sem Docker)
- **SQL Server** (para rodar localmente sem Docker)
- Opcional: ferramenta para gerenciar banco (SSMS, Azure Data Studio, etc.)

---

### Rodando com Docker (API + SQL Server em containers)

Este é o modo recomendado para simplicidade, evitando instalar banco e SDK localmente.

1. Clone o repositório:

    ```bash
    git clone [LINK_DO_SEU_REPOSITORIO]
    cd OrganizaCaixas
    ```

2. Certifique-se que o arquivo `docker-compose.yml` está presente na raiz do projeto com o seguinte conteúdo (ajuste a senha conforme sua preferência):

    ```yaml
    services:
      api:
        build:
          context: ./src/OrganizaCaixas.API
          dockerfile: Dockerfile
        ports:
          - "5000:80"
        environment:
          - ASPNETCORE_ENVIRONMENT=Development
          - ConnectionStrings__DefaultConnection=Server=db;Database=OrganizaCaixasDb;User=sa;Password=Your_password123;
        depends_on:
          - db

      db:
        image: mcr.microsoft.com/mssql/server:2022-latest
        environment:
          - ACCEPT_EULA=Y
          - SA_PASSWORD=Your_password123
        ports:
          - "1433:1433"
        volumes:
          - sqlserverdata:/var/opt/mssql

    volumes:
      sqlserverdata:
    ```

3. Suba os containers:

    ```bash
    docker-compose up --build
    ```

4. Aguarde o banco e a API iniciarem. A API estará disponível em:

    ```
    http://localhost:5000
    ```

5. Acesse a documentação interativa no navegador:

    ```
    http://localhost:5000/swagger
    ```

---

### Rodando localmente sem Docker

Caso prefira rodar o projeto e banco localmente:

1. Clone o repositório e entre na pasta do projeto API:

    ```bash
    git clone [LINK_DO_SEU_REPOSITORIO]
    cd OrganizaCaixas/src/OrganizaCaixas.API
    ```

2. Ajuste o arquivo `appsettings.Development.json` com sua string de conexão local para o SQL Server.

3. (Opcional) Remova migrações antigas e crie uma nova migração para SQL Server:

    ```bash
    dotnet ef migrations add InitialSqlServerCreate -c ApplicationDbContext
    ```

4. Atualize o banco:

    ```bash
    dotnet ef database update
    ```

5. Execute a aplicação:

    ```bash
    dotnet run
    ```

6. Acesse a API no endereço exibido no console, geralmente:

    ```
    http://localhost:5000/swagger
    ```

---

## JSON Exemplo para Testar o Endpoint `/api/Packaging/pack-orders`

```json
{
  "pedidos": [
    {
      "pedido_id": 1,
      "produtos": [
        {"produto_id": "PS5", "dimensoes": {"altura": 40, "largura": 10, "comprimento": 25}},
        {"produto_id": "Volante", "dimensoes": {"altura": 40, "largura": 30, "comprimento": 30}}
      ]
    }
    // ... outros pedidos conforme modelo anterior
  ]
}
