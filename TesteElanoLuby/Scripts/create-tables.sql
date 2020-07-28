create database TesteElano

use TesteElano

create table Projeto(
	ProjetoId int primary key identity(1,1),
	Nome varchar(55),
	DtInicio datetime,
	DtFim datetime,
) 

create table Desenvolvedor(
	DesenvolvedorId int primary key identity(1,1),
	Nome varchar(55),
	DtNascimento datetime,
	Cpf char(11),
	ProjetoId int foreign key references Projeto(ProjetoId)
) 

create table LancamentoHoras(
	LancamentoHorasDesenvolvedorId int primary key identity(1,1),
	DesenvolvedorId int foreign key references Desenvolvedor(DesenvolvedorId),
	ProjetoId int foreign key references Projeto(ProjetoId),
	DtInicio datetime,
	DtFim datetime,
) 


SELECT * FROM Projeto
SELECT * FROM Desenvolvedor
SELECT * FROM LancamentoHoras



