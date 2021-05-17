create table Genero(
	GeneroId int primary key identity(1,1),
	Descricao varchar(30)
)

create table Filme(
	FilmeId int primary key identity(1,1),
	Titulo varchar(80),
	Sinopse varchar(500),
	Imagem varchar(100),
	--GeneroId int foreign key references Genero(GeneroId),
	Genero varchar(20),
	Alugado bit,
	DataAluguel datetime,
	DateEntrega datetime
)