USE [EcommerceCore]
GO
/****** Object:  User [usr_shoppincart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
CREATE USER [usr_shoppincart] FOR LOGIN [usr_shoppincart] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [usr_shoppincart]
GO
ALTER ROLE [db_datareader] ADD MEMBER [usr_shoppincart]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [usr_shoppincart]
GO
/****** Object:  UserDefinedTableType [dbo].[T_ShoppingCart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
CREATE TYPE [dbo].[T_ShoppingCart] AS TABLE(
	[Id] [bigint] NOT NULL,
	[CustomerId] [nvarchar](50) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[InitialPrice] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[ShoppingCart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCart](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [nvarchar](50) NOT NULL,
	[ProductId] [bigint] NOT NULL,
	[InitialPrice] [decimal](18, 0) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[SpCreateShoppingCart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 10/04/2022
-- Description:	Crea un carrito de compras de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateShoppingCart]
	@dataTable T_ShoppingCart READONLY
AS
BEGIN

	INSERT INTO [dbo].[ShoppingCart]
			   ([CustomerId]
			   ,[ProductId]
			   ,[InitialPrice]
			   ,[Quantity]
			   ,[CreatedAt])
	SELECT t.CustomerId,
		   t.ProductId,
		   t.InitialPrice,
		   t.Quantity,
		   GETDATE()
    FROM @dataTable t
	 WHERE NOT EXISTS (SELECT 1 
						FROM [dbo].[ShoppingCart] 
						WHERE [CustomerId] = t.CustomerId
						  AND [ProductId] = t.ProductId);

END
GO
/****** Object:  StoredProcedure [dbo].[SpDeleteShoppingCartByUser]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 10/04/2022
-- Description:	Elimina todo el carrito de compras de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[SpDeleteShoppingCartByUser]
	@customerId nvarchar(50)
AS
BEGIN
	DELETE FROM [dbo].[ShoppingCart]
      WHERE CustomerId = @customerId
END
GO
/****** Object:  StoredProcedure [dbo].[SpDeleteShoppingCartItem]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 10/04/2022
-- Description:	Elimina un producto del carrito de compras de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[SpDeleteShoppingCartItem]
	@customerId nvarchar(50),
	@productId bigint
AS
BEGIN
	DELETE FROM [dbo].[ShoppingCart]
      WHERE CustomerId = @customerId
	  AND ProductId = @productId
END
GO
/****** Object:  StoredProcedure [dbo].[SpGetShoppingCart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 10/04/2022
-- Description:	Obtiene el carrito de compras de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[SpGetShoppingCart]
	@customerId nvarchar(50)
AS
BEGIN
	SELECT [Id]
		  ,[CustomerId]
		  ,[ProductId]
		  ,[InitialPrice]
		  ,[Quantity]
		  ,[CreatedAt]
	  FROM [dbo].[ShoppingCart]
	  WHERE [CustomerId] = @customerId	
END
GO
/****** Object:  StoredProcedure [dbo].[SpUpdateShoppingCart]    Script Date: 11/04/2022 1:22:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 10/04/2022
-- Description:	Actualiza un carrito de compras de un usuario
-- =============================================
CREATE PROCEDURE [dbo].[SpUpdateShoppingCart]
	@customerId nvarchar(50),
	@productId bigint,
	@initialPrice decimal,
	@quantity int
AS
BEGIN
	UPDATE [dbo].[ShoppingCart]
	SET Quantity = @quantity
    WHERE CustomerId = @customerId
	AND ProductId = @productId
END
GO
