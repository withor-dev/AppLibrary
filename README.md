# 📚 Sistema de Gestão de Biblioteca (Library Management System)

Este projeto é uma aplicação Full-Stack (SPA) desenvolvida como parte de um desafio técnico. O objetivo do sistema é gerenciar uma biblioteca, permitindo o cadastro, edição, exclusão e listagem de **Livros**, **Autores** e **Gêneros**, mantendo os relacionamentos e a integridade do banco de dados.

## 🚀 Tecnologias e Arquitetura

O projeto foi construído focando em escalabilidade, manutenibilidade e nas melhores práticas de mercado.

### 🔙 Back-end (.NET C#)
* **Framework:** .NET Core (Web API)
* **Linguagem:** C#
* **Banco de Dados:** PostgreSQL
* **ORM:** Entity Framework Core (Code-First & Migrations)
* **Arquitetura:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Testes Unitários:** xUnit + Moq (Padrão AAA)
* **Padrões Adotados:** Repository Pattern, Dependency Injection, DTOs (Data Transfer Objects).

### 🎨 Front-end (React + TypeScript)
* **Framework:** React.js (com Vite para alta performance)
* **Linguagem:** TypeScript (Garantindo tipagem forte e espelhando os DTOs da API)
* **Roteamento:** React Router Dom (SPA)
* **Gerenciamento de Estado Global:** Zustand (Store leve e performática)
* **Cliente HTTP:** Axios (com Interceptors para tratamento global de erros)
* **Estilização:** CSS Vanilla Customizado (Variáveis CSS, CSS Semântico, sem dependências externas pesadas).

---

## ⚙️ Diferenciais Implementados

Conforme os requisitos do desafio técnico, foram aplicados os seguintes diferenciais:
* **Models e Interfaces:** Tipagem estrita no Front-end com TypeScript.
* **Environments:** Separação de URLs da API em arquivos `.env` e `appsettings.json`.
* **Services e Interceptors:** Interceptadores configurados no Axios para capturar erros 400/500 da API e tratá-los de forma amigável ao usuário.
* **Store (Gerenciamento de Estado):** Utilização do Zustand para manter o estado global da aplicação (Autores, Gêneros e Livros) sem a necessidade de _prop drilling_ ou re-fetches desnecessários.
* **Ciclo de Vida:** Uso inteligente do `useEffect` para buscar dados apenas quando necessário.
* **Testes:** Cobertura de testes unitários para a camada de Application Services no Back-end garantindo os caminhos felizes e tristes.

---

## Pré-requisitos
* [.NET SDK](https://dotnet.microsoft.com/download) (versão 8.0 ou superior)
* [Node.js](https://nodejs.org/) (versão 18.0 ou superior)
* [PostgreSQL](https://www.postgresql.org/download/) instalado e rodando.

##Clonando o Repositório
```bash
git clone [https://github.com/withor-dev/AppLibrary.git](https://github.com/withor-dev/AppLibrary.git)
cd AppLibrary
