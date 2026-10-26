


select * from usuarios;
delete from usuarios where id = 1;
create table usuarios(
	id integer primary key,
	nome varchar(254) not null,
	email varchar(254) not null unique,
	senha_hash varchar(255) not null,
	criado_em timestamp default now()
);

create table contas(
	id integer primary key,
	nome varchar(254) not null,
	saldo_inicial numeric(12,2),
	usuario_id integer not null default 0,
	foreign key(usuario_id) references usuarios(id)
);

create type tipo_categoria as enum('Receita', 'Despesa');
create table categorias(
	id integer primary key,
	nome varchar(254) not null,
	tipo tipo_categoria not null,
	usuario_id integer not null,
	foreign key(usuario_id) references usuarios(id)
);

create table transacoes(
	id integer primary key,
	descricao varchar(255) not null,
	valor numeric(12,2) not null,
	data timestamp default now(),
	conta_id integer not null,
	foreign key(conta_id) references contas(id),
	categoria_id integer not null,
	foreign key(categoria_id) references categorias(id),
	criado_em timestamp default now()
);

INSERT INTO usuarios (id, nome, email, senha_hash)
VALUES ( 1,'João Teste', 'joao@teste.com', 'hash_fake_123');

ALTER TYPE tipo_categoria RENAME TO tipo_categoria_old;
CREATE TYPE tipo_categoria AS ENUM ('receita', 'despesa');

ALTER TABLE categorias 
    ALTER COLUMN tipo TYPE tipo_categoria 
    USING tipo::text::tipo_categoria;

DROP TYPE tipo_categoria_old;

ALTER TABLE categorias ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY;

ALTER TABLE transacoes ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY;