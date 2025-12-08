# SME - EMFORPEF - Acessos (API)

![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)
![.NET Version](https://img.shields.io/badge/.NET-10.0-blueviolet)
![Docker](https://img.shields.io/badge/Docker-Ready-blue)
![Coverage](https://img.shields.io/badge/Coverage-80%25-green)

##  1. Visão Geral

Este repositório contém a API de **Controle de Acessos e Identidade** do ecossistema da **EMFORPEF (Escola Municipal de Formação de Profissionais da Educação do Futuro)** da Secretaria Municipal de Educação (SME-SP).

### Objetivo
Gerenciar o ciclo de vida de autenticação e autorização dos servidores da rede municipal, garantindo que o acesso aos cursos, certificações e trilhas formativas respeite os perfis de cargo e lotação (RF).

### Principais Funcionalidades
* **Autenticação:** Login centralizado (Integração com SGP/LDAP legado ou IdentityServer).
* **Gestão de Perfis:** Mapeamento de Claims e Roles baseados nos cargos da SME (CP, AD, Diretor, Professor).
* **Auditoria:** Log de acessos e tentativas de invasão.

---

## 2. Tecnologias Utilizadas

A solução foi construída visando alta performance e manutenibilidade, utilizando as versões mais recentes do ecossistema .NET.

* **Linguagem:** C# 14
* **Framework:** .NET 10 (ASP.NET Core Web API)
* **Banco de Dados:** PostgreSQL (Principal) / SQL Server (Legado/Leitura)
* **ORM:** Entity Framework Core & Dapper (para consultas de alta performance)
* **Gerenciamento de Banco:** Flyway / EF Core Migrations
* **Mapeamento:** AutoMapper
* **Containerização:** Docker & Kubernetes
* **Testes:** xUnit, Moq, Bogus, FluentAssertions

---

## 3. Arquitetura

O projeto segue estritamente os princípios da **Clean Architecture** e **Domain-Driven Design (DDD)**, organizado na seguinte estrutura de pastas:

```text
src/
src/
├── SME.EMFORPEF.Acessos.Api/             # Apresentação (Controllers, Swagger, Middlewares)
├── SME.EMFORPEF.Acessos.Application/     # Casos de Uso (CQRS), DTOs, Mappers e Orquestração
├── SME.EMFORPEF.Acessos.Infra.Domain/    # Núcleo do Domínio (Entidades, Interfaces, Enums, Regras de Negócio)
├── SME.EMFORPEF.Acessos.Infra.Dados/     # Persistência (EF Core, Dapper, Repositórios, Mapeamentos de Banco)
├── SME.EMFORPEF.Acessos.Infra.Servicos/  # Integrações (HttpClients para API EOL, Telemetria, Filas)
└── SME.EMFORPEF.Acessos.IoC/             # Injeção de Dependência (Composição dos módulos)---

## 4. Configuração e Execução

### Pré-requisitos
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop)

### Executando com Docker (Recomendado)

1.  Clone o repositório:
    ```bash
    git clone [https://github.com/prefeiturasp/SME-EMFORPEF-Acessos.git](https://github.com/prefeiturasp/SME-EMFORPEF-Acessos.git)
    ```
2.  Navegue até a raiz e execute o compose:
    ```bash
    docker-compose up -d --build
    ```
3.  Acesse a documentação da API:
    * Swagger: `http://localhost:5000/swagger`

### Executando Manualmente (Local)

1.  Configure a Connection String no `appsettings.Development.json` dentro de `SME.EMFORPEF.Acessos.Api`.
2.  Execute a aplicação:
    ```bash
    cd SME.EMFORPEF.Acessos.Api
    dotnet run
    ```

---

## 5. Testes

Seguimos o padrão **Given-When-Then** para nomenclatura e **Arrange-Act-Assert** para estrutura.

```bash
# Executar todos os testes unitários
dotnet test

---

## 6. Colaboração e Código Aberto 🤝🇧🇷
Este é um projeto Open Source mantido pela Prefeitura de São Paulo.

O desenvolvimento deste software tem um impacto direto na qualidade da educação pública da cidade. Ao colaborar com melhorias, correções de bugs ou novas funcionalidades, você está contribuindo diretamente para o benefício de milhões de cidadãos paulistanos, otimizando o trabalho dos nossos educadores.

### Como Contribuir
1.  Faça um Fork do projeto.
2.  Crie uma Branch para sua feature (git checkout -b feature/nova-feature).
3.  Siga as diretrizes de código (Clean Code, SOLID, DRY).
4.  Abra um Pull Request.

"A tecnologia a serviço da educação pública de qualidade."