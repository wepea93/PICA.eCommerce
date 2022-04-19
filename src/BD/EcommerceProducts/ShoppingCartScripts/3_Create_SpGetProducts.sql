USE [EcommerceProducts]
GO

/****** Object:  StoredProcedure [dbo].[SpGetProducts]    Script Date: 11/04/2022 1:24:41 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Create date: 11/04/2022
-- Description:	Obtiene un lista de productos según una lista de ids
-- =============================================
CREATE PROCEDURE [dbo].[SpGetProducts]
		@dataTable T_Product READONLY
AS
BEGIN
	SELECT P.[Id]
		  ,P.[Name]
		  ,P.[Image]
		  ,P.[Price]
		  ,P.[ProductCategoryId]
		  ,P.[ProductProviderId]
		  ,P.[Status]
		  ,P.[CreatedAt]
		  ,P.[Score]
		  ,P.[ProductConditionId]
		  ,P.[Stock]
	  FROM [EcommerceProducts].[dbo].[Product] P
	  INNER JOIN @dataTable t ON p.Id = t.Id 
END
GO


