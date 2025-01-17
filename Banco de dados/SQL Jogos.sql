use Jogo
go

--Tabela de Categorias
create table categorias(
	id int not null primary key,
	nome varchar(100)  not null
)

go

--Tabela de jogos
CREATE TABLE jogos( 
 [id] [int] NOT NULL  primary key, 
 [descricao] [varchar](50) NULL, 
 [valor_locacao] [decimal](18, 2) NULL, 
 [data_aquisicao] [datetime] NULL, 
 [categoriaID] [int] NULL
 foreign key ([categoriaID]) references categorias(id)
 )

 go

 ---Cadastros de categoria
 insert into categorias (id,nome) values (1,'Plataforma')
 go
 insert into categorias (id,nome) values (2,'A��o')
  go
insert into categorias (id,nome) values (3,'Puzzle')
go
 insert into categorias (id,nome) values (4,'FPS')
 go

--SPs

-----------------------------------------------
--Sp Genericas

create or alter procedure spDelete(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'delete from ' + @tabela + ' where id = ' +  cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure spConsulta(
	@id int,
	@tabela varchar(max)
)
as
begin
	declare @sql varchar(max)
	set @sql = 'select * from ' + @tabela + ' where id = ' + cast( @id as varchar(max))
	exec(@sql)
end
go

create or alter procedure [dbo].[spLista]
	@tabela varchar(max)
as
begin
	select Jogos.*, Categorias.descricao as Categoria
	from jogos
	left join Categorias on jogos.categoriaID = Categorias.id
end
go

create or alter procedure spProximoId(
	@tabela varchar(max)
)
as
begin
	exec('select isnull(max(id)+1,1) as MAIOR from ' + @tabela) 
end
go


-------------------------------------------------
create or alter procedure spIncluiJogos(
	@id int,
	@descricao varchar(50),
	@valor_locacao decimal(18,2),
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	insert into jogos(id,descricao,valor_locacao,data_aquisicao,categoriaID)
	values (@id,@descricao,@valor_locacao,@data_aquisicao,@categoriaID)
end

go


create or alter procedure spAlteraJogos(
	@id int,
	@descricao varchar(50),
	@valor_locacao decimal(18,2),
	@data_aquisicao datetime,
	@categoriaID int
)
as
begin
	update jogos set descricao = @descricao, valor_locacao =	@valor_locacao,
	data_aquisicao = @data_aquisicao, categoriaID = @categoriaID where id = @id
end
go
