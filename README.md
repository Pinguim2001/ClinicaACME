# 🏥 Clínica ACME - Registro de Atendimentos

Este projeto é uma aplicação web desenvolvida com **ASP.NET Core MVC**, com objetivo de atender à demanda da Clínica ACME de cadastrar pacientes e registrar atendimentos médicos, com filtros, validações e organização profissional.

---

## 📌 Funcionalidades

### 🧑‍⚕️ Cadastro de Pacientes
- [x] Cadastrar novos pacientes
- [x] Editar dados dos pacientes
- [x] Inativar pacientes (status lógico)
- [x] Filtros por **nome**, **CPF** e **status**
- [x] Validação para evitar **CPF duplicado**

### 📋 Registro de Atendimentos
- [x] Registrar atendimentos para pacientes ativos
- [x] Editar registros de atendimento
- [x] Inativar atendimentos (status lógico)
- [x] Filtros por **data (período)**, **paciente** e **status**
- [x] Validação para impedir **data/hora no futuro**
- [x] Descrição com suporte a **texto grande com quebras de linha**

---

## 🧱 Tecnologias Utilizadas

- ASP.NET Core MVC
- C# com Entity Framework Core
- Razor Pages
- Bootstrap (estilização via CDN)
- Dados salvos em memória (Como seria um projeto simples preferi tratar com dados salvos em memória)
- LINQ para consultas e filtros

---

## 📁 Estrutura de Pastas

├── Controllers
│ ├── PacientesController.cs
│ └── AtendimentosController.cs
├── Models
│ ├── Paciente.cs
│ └── Atendimento.cs
├── Views
│ ├── Pacientes (Index, Create, Edit)
│ └── Atendimentos (Index, Create, Edit)
├── Data
│ └── ApplicationDbContext.cs
├── wwwroot
│ └── (CSS, JS, Bootstrap via CDN)


---

## 🧪 Validações Implementadas

- ✅ CPF válido e único
- ✅ Data de atendimento **não pode ser futura**
- ✅ Somente pacientes **ativos** podem receber atendimentos
- ✅ Descrição de atendimento permite **textos grandes**


