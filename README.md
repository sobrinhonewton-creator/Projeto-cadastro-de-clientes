# Sistema de Cadastro de Clientes

Aplicacao de console em C# para cadastro e listagem de clientes. O objetivo do projeto e demonstrar fundamentos de programacao, organizacao de classes, validacao basica de entrada e estrutura minima de um projeto .NET.

# Funcionalidades

- Cadastro de clientes com nome e email.
- Validacao simples de campos obrigatorios.
- Validacao basica de email.
- Listagem dos clientes cadastrados em memoria.

## Tecnologias

- C#
- .NET
- Console application

## Como executar

```powershell
dotnet run
```

## Estrutura

```text
Program.cs    Fluxo principal do menu e validacoes
Cliente.cs    Modelo de cliente
```

## Evolucoes planejadas

- Persistencia em arquivo JSON ou banco SQLite.
- Testes unitarios para validacao de cadastro.
- Busca de clientes por nome ou email.
- Separacao em camadas de dominio, servico e infraestrutura.
