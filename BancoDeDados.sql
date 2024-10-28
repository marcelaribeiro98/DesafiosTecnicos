-- Criação do banco de dados
CREATE DATABASE [fiap_gestao_escolar];
GO

USE [fiap_gestao_escolar];
GO

-- Criação da tabela aluno
CREATE TABLE [dbo].[aluno] (
    [id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [nome] VARCHAR(255) NOT NULL,
    [usuario] VARCHAR(45) NOT NULL,
    [senha] CHAR(60) NULL,
    [ativo] BIT NOT NULL DEFAULT 1
) ON [PRIMARY];
GO

-- Criação da tabela turma
CREATE TABLE [dbo].[turma] (
    [id] INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [curso_id] INT NOT NULL,
    [turma] VARCHAR(45) NOT NULL,
    [ano] INT NOT NULL,
    [ativo] BIT NOT NULL DEFAULT 1
) ON [PRIMARY];
GO

-- Criação da tabela aluno_turma
CREATE TABLE [dbo].[aluno_turma] (
    [aluno_id] INT NOT NULL,
    [turma_id] INT NOT NULL,
    [ativo] BIT NOT NULL DEFAULT 1
    PRIMARY KEY ([aluno_id], [turma_id]),
    FOREIGN KEY ([aluno_id]) REFERENCES [dbo].[aluno] ([id]),
    FOREIGN KEY ([turma_id]) REFERENCES [dbo].[turma] ([id])
) ON [PRIMARY];
GO
