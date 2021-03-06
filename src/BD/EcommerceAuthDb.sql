USE [EcommerceAuth]
GO
/****** Object:  User [usr_auth]    Script Date: 1/04/2022 8:40:54 p. m. ******/
CREATE USER [usr_auth] FOR LOGIN [usr_auth] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [usr_auth]
GO
/****** Object:  Table [dbo].[AccessToken]    Script Date: 1/04/2022 8:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccessToken](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AppCode] [nvarchar](6) NOT NULL,
	[Token] [nvarchar](1000) NOT NULL,
	[ExpiresAt] [datetime] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AccessToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 1/04/2022 8:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[AppCode] [nvarchar](6) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[ApiUser] [nvarchar](50) NOT NULL,
	[ApiKey] [nvarchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[AppCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUserRelationships]    Script Date: 1/04/2022 8:40:54 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUserRelationships](
	[AppUserAppCode1] [nvarchar](6) NOT NULL,
	[AppUserAppCode2] [nvarchar](6) NOT NULL,
	[Status] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_AppUserRelationships] PRIMARY KEY CLUSTERED 
(
	[AppUserAppCode1] ASC,
	[AppUserAppCode2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccessToken] ON 

INSERT [dbo].[AccessToken] ([Id], [AppCode], [Token], [ExpiresAt], [CreatedAt]) VALUES (1, N'APPTES', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBDb2RlIjoiQVBQVEVTIiwibmJmIjoxNjQ4NjY0MDA4LCJleHAiOjE2NDg2Njc2MDgsImlhdCI6MTY0ODY2NDAwOH0.sVNqJXYTA6rqkbGCpX5P1lMtpMMHgk2ALG_IyAlegKY', CAST(N'2022-03-30T18:14:38.467' AS DateTime), CAST(N'2022-03-30T13:13:28.940' AS DateTime))
INSERT [dbo].[AccessToken] ([Id], [AppCode], [Token], [ExpiresAt], [CreatedAt]) VALUES (2, N'APPTES', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBDb2RlIjoiQVBQVEVTIiwibmJmIjoxNjQ4NjY4MjM1LCJleHAiOjE2NDg3NTQ2MzUsImlhdCI6MTY0ODY2ODIzNX0.sd0YLqw0uavqB3n6TbOntfgGIo4rYW4DwggaaXYkJko', CAST(N'2022-03-30T19:25:05.483' AS DateTime), CAST(N'2022-03-30T14:23:55.613' AS DateTime))
INSERT [dbo].[AccessToken] ([Id], [AppCode], [Token], [ExpiresAt], [CreatedAt]) VALUES (3, N'APPTES', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBDb2RlIjoiQVBQVEVTIiwibmJmIjoxNjQ4Nzg0MTA4LCJleHAiOjE2NDg4NzA1MDgsImlhdCI6MTY0ODc4NDEwOH0.HmEG1vkFcdzJP0WPIPTj6sk8tvWX_A9jUiYHw6R2kOY', CAST(N'2022-04-01T03:36:18.010' AS DateTime), CAST(N'2022-03-31T22:35:08.493' AS DateTime))
INSERT [dbo].[AccessToken] ([Id], [AppCode], [Token], [ExpiresAt], [CreatedAt]) VALUES (4, N'APPTES', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhcHBDb2RlIjoiQVBQVEVTIiwibmJmIjoxNjQ4Nzg1ODgxLCJleHAiOjE2NDg4NzIyODEsImlhdCI6MTY0ODc4NTg4MX0.Tz1WjzwfSqr6DoslHcUm7uKbIup8yvV0gsaOLulFimY', CAST(N'2022-04-01T04:05:51.830' AS DateTime), CAST(N'2022-03-31T23:04:41.830' AS DateTime))
SET IDENTITY_INSERT [dbo].[AccessToken] OFF
GO
INSERT [dbo].[AppUser] ([AppCode], [Name], [Description], [ApiUser], [ApiKey], [Status], [CreatedAt]) VALUES (N'APPROD', N'API DE PRODUCTOS', N'API PARA LA CONSULTA DEL CATÁLOGO DE PRODUCTOS', N'USR_PRODUCTS', N'44E84F41-5521-408C-9E0A-684220C3ED0C', 1, CAST(N'2022-03-30T11:43:56.707' AS DateTime))
INSERT [dbo].[AppUser] ([AppCode], [Name], [Description], [ApiUser], [ApiKey], [Status], [CreatedAt]) VALUES (N'APPTES', N'TEST', N'USUARIO DE PRUEBAS', N'USR_TEST', N'5AC14519-4345-4857-8721-8BC9A1E200F3', 1, CAST(N'2022-03-30T10:43:51.183' AS DateTime))
INSERT [dbo].[AppUser] ([AppCode], [Name], [Description], [ApiUser], [ApiKey], [Status], [CreatedAt]) VALUES (N'ECOMME', N'ECOMMERCE', N'ECOMMERCE DE PRODUCTOS ELECTRÓNICOS', N'USR_ECOMMERCE', N'7A4C577E-ED93-44FD-9D07-CF28A5A2D8A8', 1, CAST(N'2022-03-30T10:42:36.883' AS DateTime))
GO
INSERT [dbo].[AppUserRelationships] ([AppUserAppCode1], [AppUserAppCode2], [Status], [CreatedAt]) VALUES (N'APPTES', N'APPROD', 1, CAST(N'2022-03-30T11:47:10.197' AS DateTime))
INSERT [dbo].[AppUserRelationships] ([AppUserAppCode1], [AppUserAppCode2], [Status], [CreatedAt]) VALUES (N'ECOMME', N'APPROD', 1, CAST(N'2022-03-30T11:47:04.027' AS DateTime))
GO
ALTER TABLE [dbo].[AccessToken]  WITH CHECK ADD  CONSTRAINT [FK_AccessToken_AppUser] FOREIGN KEY([AppCode])
REFERENCES [dbo].[AppUser] ([AppCode])
GO
ALTER TABLE [dbo].[AccessToken] CHECK CONSTRAINT [FK_AccessToken_AppUser]
GO
ALTER TABLE [dbo].[AppUserRelationships]  WITH CHECK ADD  CONSTRAINT [FK_AppUserRelationships_AppUser] FOREIGN KEY([AppUserAppCode1])
REFERENCES [dbo].[AppUser] ([AppCode])
GO
ALTER TABLE [dbo].[AppUserRelationships] CHECK CONSTRAINT [FK_AppUserRelationships_AppUser]
GO
ALTER TABLE [dbo].[AppUserRelationships]  WITH CHECK ADD  CONSTRAINT [FK_AppUserRelationships_AppUser1] FOREIGN KEY([AppUserAppCode2])
REFERENCES [dbo].[AppUser] ([AppCode])
GO
ALTER TABLE [dbo].[AppUserRelationships] CHECK CONSTRAINT [FK_AppUserRelationships_AppUser1]
GO
/****** Object:  StoredProcedure [dbo].[SpCreateAccessToken]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 24/03/2022
-- Description:	Registra un nuevo accesToken 
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateAccessToken] 
	@appCode nvarchar(50),
	@token nvarchar(1000),
	@expiresAt datetime
AS
BEGIN
	INSERT INTO [dbo].[AccessToken]
			   ([AppCode]
			   ,[Token]
			   ,[ExpiresAt]
			   ,[CreatedAt])
		 VALUES
			   (@appCode
			   ,@token
			   ,@expiresAt
			   ,getdate())
END
GO
/****** Object:  StoredProcedure [dbo].[SpCreateAppUser]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 30/03/2022
-- Description:	Creación de usuarios para appis
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateAppUser] 
	@appCode nvarchar(6),
	@appName nvarchar(50),
	@appiUser nvarchar(50),
	@appDescription nvarchar(100)
AS
BEGIN
 
	INSERT INTO [dbo].[AppUser]
			   ([AppCode]
			   ,[Name]
			   ,[Description]
			   ,[ApiUser]
			   ,[ApiKey]
			   ,[Status]
			   ,[CreatedAt])
		 VALUES
			   (UPPER(@appCode)
			   ,UPPER(@appName)
			   ,UPPER(@appDescription)
			   ,RTRIM(LTRIM(UPPER(@appiUser)))
			   ,NEWID()
			   ,1
			   ,GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[SpCreateAppUserRelationships]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 30/03/2022
-- Description:	Creación de relaciones
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateAppUserRelationships] 
	@appCode1 nvarchar(6),
	@appCode2 nvarchar(6)
AS
BEGIN
 
	INSERT INTO [dbo].[AppUserRelationships]
			   ([AppUserAppCode1]
			   ,[AppUserAppCode2]
			   ,[Status]
			   ,[CreatedAt])
		 VALUES
			   (@appCode1
			   ,@appCode2
			   ,1
			   ,GETDATE())
END
GO
/****** Object:  StoredProcedure [dbo].[SpDeleteExpiredAccessTokens]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 24/03/2022
-- Description:	Elimina los accesTokens vencidos 
-- =============================================
CREATE PROCEDURE [dbo].[SpDeleteExpiredAccessTokens] 
AS
BEGIN
	DELETE FROM [dbo].[AccessToken]
	WHERE [ExpiresAt] < GETDATE();
END
GO
/****** Object:  StoredProcedure [dbo].[SpGetAccessToken]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 24/03/2022
-- Description:	Obtiene un accesToken 
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAccessToken] 
	@token nvarchar(1000)
AS
BEGIN
	SELECT TOP (1) [Id]
		  ,[AppCode]
		  ,[Token]
		  ,[ExpiresAt]
		  ,[CreatedAt]
	  FROM [EcommerceAuth].[dbo].[AccessToken]
	  Where Token = @token
END
GO
/****** Object:  StoredProcedure [dbo].[SpGetAppUser]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 25/03/2022
-- Description:	Obtener usuario api
-- =============================================
CREATE PROCEDURE [dbo].[SpGetAppUser]
	@apiUser nvarchar(50),
	@apiKey nvarchar(100)
AS
BEGIN
	
	SELECT [AppCode],
		   [ApiUser]
	FROM AppUser
	WHERE [ApiUser] = @apiUser
	  AND [ApiKey] = @apiKey
	  AND Status = 1

END
GO
/****** Object:  StoredProcedure [dbo].[SpValidateAccessToken]    Script Date: 1/04/2022 8:40:55 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 24/03/2022
-- Description:	Valida un accesToken 
-- =============================================
CREATE PROCEDURE [dbo].[SpValidateAccessToken] 
	@token nvarchar(1000),
	@appCodeOrigen nvarchar(6),
	@appCodeDestiny nvarchar(6)
AS
BEGIN
	DECLARE 
		@existToken int,
	    @existRelationships int,
	    @tokenIsValid bit

    SET @tokenIsValid = 0;
	SET @existRelationships = 0;

	SET @existToken = ( SELECT Count([Id])
						  FROM [EcommerceAuth].[dbo].[AccessToken]
						  Where Token = @token
						  AND [AppCode] = @appCodeOrigen
						  AND [ExpiresAt] >= GETDATE())

	IF (@existToken > 0)
	BEGIN
   	SET @existRelationships = (	SELECT count (*)
									FROM [EcommerceAuth].[dbo].[AppUserRelationships]
									WHERE ([AppUserAppCode1] = @appCodeOrigen and [AppUserAppCode2] = @appCodeDestiny)
									OR ([AppUserAppCode1] = @appCodeDestiny and [AppUserAppCode2] = @appCodeOrigen)
									AND [STATUS] = 1
									GROUP BY [AppUserAppCode1], [AppUserAppCode2]);
	END

	IF(@existToken > 0 and @existRelationships >0)
	BEGIN
		SET @tokenIsValid = 1;
	END
	ELSE
	BEGIN
		SET @tokenIsValid = 0;
	END

	SELECT @tokenIsValid as 'IsValid'
END
GO
