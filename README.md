# 📋 ToDo API

API para gerenciamento de tarefas, com suporte à categorização. Ideal para uso em aplicações web ou mobile que requerem controle de tarefas simples e flexível.

## 🚀 Funcionalidades

- ✅ Adicionar tarefas com título e descrição
- 🗂️ Associar tarefas a categorias (ex: Trabalho, Estudo, Pessoal)
- 📋 Listar todas as tarefas
- ✏️ Editar tarefas existentes
- 🗑️ Excluir tarefas

## 🛠️ Tecnologias

- .NET
- ASP.NET Core Web API
- SQLite
- Entity Framework Core
- Docker
- Blazor

## 📦 Instalação

### Requer a ferrementa "Entity Framawork Core" instalada

1. Clone o repositório:

```bash
git clone https://github.com/rafaelgucky/ToDoApp.git
````
2. Rode os comandos na pasta do repositório:
```
cd ToDoApp/src
dotnet restore
dotnet ef database update
dotnet run
cd ../../
cd ToDoAppFront
dotnet restore
dotnet run
```



