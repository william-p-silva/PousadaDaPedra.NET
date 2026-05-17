# 🏨 Pousada Pedra Furada — Backend API

> Backend desenvolvido com foco em aprendizado de **Clean Architecture**, **ASP.NET Core**, **Entity Framework Core** e **Testes Unitários** utilizando C#.

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-Latest-239120?style=flat-square&logo=csharp)
![EF Core](https://img.shields.io/badge/EF_Core-Latest-512BD4?style=flat-square&logo=dotnet)
![xUnit](https://img.shields.io/badge/xUnit-Tests-green?style=flat-square)
![Status](https://img.shields.io/badge/Status-Em%20Desenvolvimento-yellow?style=flat-square)

---

## 📚 Sobre o Projeto

Este projeto foi criado com o objetivo de estudar arquitetura de software moderna, separação de responsabilidades, princípios SOLID e construção de APIs desacopladas e testáveis.

O domínio escolhido simula o backend de uma pousada — **Pousada Pedra Furada** — com gestão de usuários, tarefas e relatórios.

---

## 🎯 Objetivos de Aprendizado

- Aplicar **Clean Architecture** na prática
- Seguir boas práticas no ecossistema **.NET**
- Implementar princípios **SOLID**
- Trabalhar com **Injeção de Dependência**
- Desenvolver uma API REST organizada e escalável
- Implementar **Testes Unitários** com xUnit e Moq
- Criar um backend desacoplado do front-end

---

## 🧠 Conceitos Aplicados

| Categoria | Conceitos |
|-----------|-----------|
| Arquitetura | Clean Architecture, Arquitetura em Camadas, Separation of Concerns |
| Princípios | SOLID, Dependency Inversion, Encapsulation |
| Padrões | Repository Pattern, Use Cases, DTOs |
| Infraestrutura | Entity Framework Core, Persistência desacoplada |
| Testes | xUnit, Moq, Testes Unitários |
| API | ASP.NET Core, APIs RESTful, Dependency Injection |

---

## 🏗️ Estrutura do Projeto

O projeto segue a estrutura da **Clean Architecture**, separando responsabilidades em camadas distintas.

```
PousadaDaPedra/
│
├── PousadaDaPedra.Domain/          # Núcleo da aplicação
│   ├── Entities/
│   ├── Enums/
│   
│
├── PousadaDaPedra.Application/     # Casos de uso e regras de negócio
│   ├── DTOs/
│   ├── Interfaces/
│   └── UseCases/
│
├── PousadaDaPedra.Infrastructure/  # Implementações técnicas
│   └── Data/
|       ├── Configurations/
|       ├── Context/
|       ├── Security/
│       └──Repositories/
│
├── PousadaDaPedra.WebApi/          # Camada de apresentação
│   ├── Controllers/
│   ├── Middlewares/
│   └── Program.cs
│
└── PousadaDaPedraApi.Tests/        # Testes unitários
    ├── TarefasTests/
    └── UsuariosTests/
```

---

## 🧩 Camadas da Arquitetura

### 🔹 Domain
Camada central da aplicação — **não depende de nenhuma outra camada**.

Responsável por entidades, regras de negócio e enums.

```csharp
public class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
}
```

---

### 🔹 Application
Responsável pelos casos de uso da aplicação. Contém DTOs, interfaces, services e use cases — aqui fica a lógica da aplicação.

```csharp
public class CriarTarefa
{
    private readonly ITarefaRepository _repository;

    public CriarTarefa(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task Execute(CriarTarefaDTO dto)
    {
        // regra de aplicação
    }
}
```

---

### 🔹 Infrastructure
Responsável pela implementação técnica: Entity Framework Core, repositórios, banco de dados e serviços externos.

---

### 🔹 WebApi
Camada de apresentação — controllers, rotas, middlewares e configuração da aplicação.

```csharp
[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok();
    }
}
```

---

## 🔄 Fluxo da Aplicação

```
Controller  →  Use Case  →  Repository Interface  →  Repository Implementation  →  Database
```

---

## 🚀 Funcionalidades Atuais

### 👤 Usuários
- Cadastro de usuários
- Login
- Autenticação

### ✅ Tarefas
- Criar tarefas
- Atualizar tarefas
- Listar tarefas
- Controle de status e prioridades
- Atribuição de responsáveis

### 📊 Relatórios
- Estrutura inicial em desenvolvimento

---

## 🧪 Testes Unitários

Testes implementados com **xUnit** e **Moq**, validando regras de negócio sem dependência de banco de dados ou infraestrutura.

```csharp
[Fact]
public async Task Deve_Criar_Tarefa_Com_Sucesso()
{
    // Arrange
    // Act
    // Assert
}
```

---

## 🛠️ Tecnologias

| Categoria | Tecnologia |
|-----------|------------|
| Linguagem | C# |
| Framework | .NET 9 / ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Banco de Dados | PostgreSQL |
| Testes | xUnit + Moq |
| IDEs | Rider / Visual Studio |
| Versionamento | Git + GitHub |

---

## ▶️ Como Executar

### Pré-requisitos
- .NET 9 SDK
- PostgreSQL

### Passo a passo

```bash
# 1. Clonar o repositório
git clone https://github.com/william-p-silva/PousadaDaPedra.NET.git

# 2. Entrar na pasta
cd PousadaDaPedra

# 3. Restaurar dependências
dotnet restore

# 4. Executar migrations
dotnet ef database update

# 5. Rodar o projeto
dotnet run
```

### Executar testes

```bash
dotnet test
```

---

## 🎯 Próximos Passos

- [ ] Melhorar validações
- [ ] Adicionar testes de integração
- [ ] Criar documentação Swagger completa
- [ ] Desenvolver front-end (SPA)
- [ ] Dockerizar a aplicação
- [ ] Implementar CI/CD
- [ ] Deploy em nuvem

---

## 📖 Referências

Este projeto está sendo desenvolvido com base em práticas recomendadas pela Microsoft e pela comunidade .NET.

Principal referência:

> **"Architecting Modern Web Applications with ASP.NET Core and Azure"** — Steve "ardalis" Smith

O material aborda Clean Architecture, Dependency Injection, Arquitetura em Camadas, Testabilidade, SOLID, ASP.NET Core e EF Core — com ênfase em aplicações modulares, pouco acopladas e facilmente testáveis.

---

## 👨‍💻 Autor

**William José Pereira**

Estudante de Desenvolvimento de Software Multiplataforma (DSM) na **Fatec**.

Atualmente estudando ASP.NET Core, Clean Architecture, Entity Framework Core, Testes Unitários, APIs REST e Arquitetura de Software.

---

## ⭐ Considerações

Este projeto faz parte da minha jornada de aprendizado em desenvolvimento backend com .NET.

O objetivo principal não é apenas construir uma API funcional, mas desenvolver uma base sólida em arquitetura de software, código limpo e testes automatizados — entendendo **por que** cada camada existe, **por que** cada dependência está em determinado lugar, e **como** reduzir acoplamento para tornar o sistema escalável.

---

<p align="center">🚧 Projeto em desenvolvimento 🚧</p>
