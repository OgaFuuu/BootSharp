USE [BootSharpSource]
GO

--DROPPING EXISTING

IF OBJECT_ID('BootSharpSource.dbo.BooksCategories', 'U') IS NOT NULL
  DROP TABLE [BootSharpSource].dbo.[BooksCategories];
GO

IF OBJECT_ID('BootSharpSource.dbo.Categories', 'U') IS NOT NULL
  DROP TABLE [BootSharpSource].dbo.[Categories]; 
GO

IF OBJECT_ID('BootSharpSource.dbo.Books', 'U') IS NOT NULL
  DROP TABLE [BootSharpSource].dbo.[Books]; 
GO

IF OBJECT_ID('BootSharpSource.dbo.Publishers', 'U') IS NOT NULL
  DROP TABLE [BootSharpSource].dbo.[Publishers]; 
GO

IF OBJECT_ID('BootSharpSource.dbo.Authors', 'U') IS NOT NULL
  DROP TABLE [BootSharpSource].dbo.[Authors];
GO


--CREATING TABLES
CREATE TABLE [BootSharpSource].dbo.[Categories]
(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	NAME VARCHAR(512) NOT NULL
)
GO

CREATE TABLE [BootSharpSource].dbo.[Publishers]
(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	NAME VARCHAR(512) NOT NULL
)
GO

CREATE TABLE [BootSharpSource].dbo.[Authors]
(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	NAME VARCHAR(512) NOT NULL
)
GO

CREATE TABLE [BootSharpSource].dbo.[Books]
(
	ID BIGINT IDENTITY(1,1) PRIMARY KEY,
	NAME VARCHAR(512) NOT NULL,
	Author_Id BIGINT NOT NULL,
	Publisher_Id BIGINT NOT NULL,
	FOREIGN KEY (Author_Id) REFERENCES Authors(Id),
	FOREIGN KEY (Publisher_Id) REFERENCES Publishers(Id)
)
GO

CREATE TABLE [BootSharpSource].dbo.[BooksCategories]
(
	Book_Id BIGINT NOT NULL,
	Category_Id BIGINT NOT NULL,
	FOREIGN KEY (Book_Id) REFERENCES Books(Id),
	FOREIGN KEY (Category_Id) REFERENCES Categories(Id)
)
GO



