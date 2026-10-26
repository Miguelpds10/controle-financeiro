💰 Controle Financeiro

Aplicação web para controle de finanças pessoais, desenvolvida com ASP.NET Core MVC, Entity Framework Core e PostgreSQL.

Projeto de estudos baseado na apostila Desenvolvimento Web com ASP.NET MVC 4 (K19 Treinamentos), adaptado para a stack atual do .NET e para o PostgreSQL.

📋 Funcionalidades
 CRUD de Contas (Index, Create, Edit, Delete)
 CRUD de Categorias (Receita / Despesa)
 Validação de modelos com Data Annotations
 Mapeamento de enum nativo do PostgreSQL
 CRUD de Transações
 CRUD de Usuários
 Autenticação e autorização (login, filtros)
 Tratamento de erros e páginas customizadas
 API REST (JSON)
 Dashboard / relatórios por período e categoria
🛠️ Tecnologias
Camada	Tecnologia
Framework	ASP.NET Core MVC (.NET 8+)
ORM	Entity Framework Core
Banco de dados	PostgreSQL
Driver	Npgsql.EntityFrameworkCore.PostgreSQL
Views	Razor + Bootstrap
IDE	Visual Studio 2026
🗂️ Estrutura do projeto
ControleFinanceiro/
├── Controllers/
│   ├── ContasController.cs
│   ├── CategoriasController.cs
│   └── UsuariosController.cs
├── Data/
│   └── FinancasContext.cs
├── Models/
│   ├── Usuario.cs
│   ├── Conta.cs
│   ├── Categoria.cs
│   └── Transacao.cs
├── Views/
│   ├── Contas/
│   ├── Categorias/
│   └── Usuarios/
├── appsettings.json
└── Program.cs
🗃️ Modelo de dados
usuarios 1 ──── N contas
usuarios 1 ──── N categorias
contas 1 ──── N transacoes
categorias 1 ──── N transacoes
Tabela	Principais campos
usuarios	id, nome, email (único), senha_hash, criado_em
contas	id, nome, saldo_inicial, usuario_id
categorias	id, nome, tipo (Receita/Despesa), usuario_id
transacoes	id, descricao, valor, data, conta_id, categoria_id, criado_em
Script SQL
sql
create table usuarios(
    id integer generated always as identity primary key,
    nome varchar(254) not null,
    email varchar(254) not null unique,
    senha_hash varchar(255) not null,
    criado_em timestamp default now()
);

create table contas(
    id integer generated always as identity primary key,
    nome varchar(254) not null,
    saldo_inicial numeric(12,2) not null default 0,
    usuario_id integer not null,
    foreign key(usuario_id) references usuarios(id) on delete cascade
);

create type tipo_categoria as enum('Receita', 'Despesa');

create table categorias(
    id integer generated always as identity primary key,
    nome varchar(254) not null,
    tipo tipo_categoria not null,
    usuario_id integer not null,
    foreign key(usuario_id) references usuarios(id) on delete cascade
);

create table transacoes(
    id integer generated always as identity primary key,
    descricao varchar(255) not null,
    valor numeric(12,2) not null,
    data date not null,
    conta_id integer not null,
    foreign key(conta_id) references contas(id) on delete cascade,
    categoria_id integer not null,
    foreign key(categoria_id) references categorias(id) on delete restrict,
    criado_em timestamp default now()
);
🚀 Como executar
Pré-requisitos
.NET SDK 8+
PostgreSQL
Visual Studio (ou outro editor de sua preferência)
Passo a passo
Clone o repositório
bash
   git clone https://github.com/SEU-USUARIO/ControleFinanceiro.git
   cd ControleFinanceiro/ControleFinanceiro
Crie o banco de dados financas_db no PostgreSQL e execute o script SQL acima.
Configure a connection string com a senha do seu banco em appsettings.json:
json
   {
     "ConnectionStrings": {
       "FinancasConnection": "Host=localhost;Port=5432;Database=financas_db;Username=postgres;Password=SUA_SENHA"
     }
   }
Restaure os pacotes e execute
bash
   dotnet restore
   dotnet run
Acesse http://localhost:5260/Contas (a porta pode variar; veja a saída do terminal).
📚 Aprendizados

Principais conceitos praticados (referência aos capítulos da apostila K19):

Padrão MVC — Models, Views e Controllers (cap. 4–6)
Entity Framework — mapeamento, relacionamentos One-to-Many / Many-to-One (cap. 3)
Validação com Data Annotations (cap. 7)
Mapeamento explícito de nomes de tabelas/colunas via Fluent API (OnModelCreating) para convivência com o padrão snake_case do PostgreSQL
Enum nativo do PostgreSQL mapeado para enum C#
Padrão GET/POST em ações de exclusão ([ActionName("Delete")])
