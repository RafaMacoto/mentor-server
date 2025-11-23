
# Mentor API

API RESTful em .NET voltada ao tema **“O Futuro do Trabalho”**, permitindo gerenciar usuários, suas skills e objetivos de carreira.

---

## Integrantes

- Rafael Macoto
- Gabrielly Macedo
- Fernando Henrique Aguiar

---

## Tecnologias

- **Back-end:** .NET 9
- **Banco de dados:** Oracle (ou outro relacional)
- **ORM:** Entity Framework Core
- **Testes:** xUnit + FluentAssertions
- **Mock de DB para testes:** InMemory Database
- **Controle de versão da API:** /api/v1
- **Logging e observabilidade:** Health Check e logging básico
- **Boas práticas REST:** HATEOAS, status codes apropriados, verbos HTTP corretos

---

## Estrutura do Projeto

Mentor/
├─ Data/ -> DbContext e configurações do banco
├─ DTOs/ -> Data Transfer Objects
├─ Models/ -> Classes do domínio
├─ Services/ -> Lógica de negócios
├─ Migrations/ -> Migrations do Entity Framework
├─ Mentor.Tests/ -> Testes unitários
├─ Program.cs -> Inicialização da aplicação
└─ README.md -> Este arquivo

yaml
Copiar código

---

## Requisitos

- .NET 9 SDK
- Oracle Client (ou outro banco configurado)
- Visual Studio 2022 ou VS Code
- NuGet para restaurar pacotes

---

## Instalação

1. Clone o repositório:

```bash
git clone https://github.com/RafaMacoto/mentor-server.git
cd mentor-server
```

Executando a API

dotnet run 
A API ficará disponível em:

http://localhost:5268


Testes
Os testes utilizam InMemory Database para não afetar o banco real.

Navegue até a pasta de testes:


cd Mentor.Tests
Execute os testes:

dotnet test


## Boas práticas REST implementadas
Verbos HTTP corretos

Status codes adequados

Paginação

HATEOAS

Health Check e logging básico

Versionamento da API
API estruturada em /api/v1

Futuras versões podem ser expostas em /api/v2

Observabilidade
Health check: /health

Logging: Logs de operações e erros

Tracing: Possível integração futura com OpenTelemetry


Logging: Logs de operações e erros

Tracing: Possível integração futura com OpenTelemetry
