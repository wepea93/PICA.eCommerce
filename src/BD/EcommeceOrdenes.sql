USE [Ecommerce]
GO
/****** Object:  Table [dbo].[E_order]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[E_order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ORDER_NUMBER] [int] NOT NULL,
	[ORDER_DATE] [datetime] NOT NULL,
	[DATE_REQUIRED] [datetime] NULL,
	[DATE_SHIPPED] [datetime] NULL,
	[STATUS_ID] [int] NULL,
	[COMMENT] [varchar](100) NULL,
	[CUSTOMER_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ORDER_NUMBER] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[E_order_detail]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[E_order_detail](
	[ORDER_ID] [int] NULL,
	[PRODUCT_ID] [int] NULL,
	[QUANTITY_ORDERED] [int] NULL,
	[PRICE_EACH] [money] NULL,
	[ORDER_LINE] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[E_order_state]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[E_order_state](
	[ID] [int] NOT NULL,
	[STATE] [varchar](20) NOT NULL,
	[DESCRIPTION] [varchar](100) NULL,
 CONSTRAINT [PK_E_order_state] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[E_order]  WITH CHECK ADD FOREIGN KEY([STATUS_ID])
REFERENCES [dbo].[E_order_state] ([ID])
GO
ALTER TABLE [dbo].[E_order_detail]  WITH CHECK ADD  CONSTRAINT [FK__E_order_d__ORDER__2A164134] FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[E_order] ([ORDER_NUMBER])
GO
ALTER TABLE [dbo].[E_order_detail] CHECK CONSTRAINT [FK__E_order_d__ORDER__2A164134]
GO
/****** Object:  StoredProcedure [dbo].[SpCreateDetailOrder]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jairo,Moreno
-- Create date: <Create Date,,>
-- Description:	Creación del detalle de orden
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateDetailOrder]
@ORDER_NUMBER  varchar(100)
,@PRODUCT_ID varchar(100)
,@QUANTITY_ORDERED int
,@PRICE_EACH  float
,@ORDER_LINE int
AS
BEGIN


INSERT INTO DBO.E_order_detail(
 ORDER_ID
,PRODUCT_ID
,QUANTITY_ORDERED
,PRICE_EACH
,ORDER_LINE)
VALUES(
 @ORDER_NUMBER
,@PRODUCT_ID
,@QUANTITY_ORDERED
,@PRICE_EACH
,@ORDER_LINE
)


END
GO
/****** Object:  StoredProcedure [dbo].[SpCreateOrder]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jairo,Moreno
-- Create date: <Create Date,,>
-- Description:	Creación de orden
-- =============================================
CREATE PROCEDURE [dbo].[SpCreateOrder]
 @ORDER_NUMBER  varchar(100)
,@DATE_REQUIRED date
,@ORDER_DATE date
,@STATUS_ID int
,@COMMENT varchar(250)
,@CUSTOMER_ID varchar(100)
AS

BEGIN

IF (@STATUS_ID IS NULL OR @STATUS_ID = 0)
BEGIN 
 SET @STATUS_ID= (SELECT ID FROM E_order_state WHERE STATE='REGISTRADO')
END

Insert into dbo.E_order (ORDER_NUMBER,
ORDER_DATE,
DATE_REQUIRED,
DATE_SHIPPED,
STATUS_ID,
COMMENT,
CUSTOMER_ID)
values(
@ORDER_NUMBER
,CASE WHEN @ORDER_DATE IS NULL THEN getdate() ELSE @ORDER_DATE END
,@DATE_REQUIRED
,NULL
,@STATUS_ID
,@COMMENT
,@CUSTOMER_ID
)



END
GO
/****** Object:  StoredProcedure [dbo].[SpGetDetailOrder]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jairo,Moreno
-- Create date: <Create Date,,>
-- Description:	obtener detalle de una orden
-- =============================================
CREATE PROCEDURE [dbo].[SpGetDetailOrder]
@ORDER_NUMBER  varchar(100)
AS
BEGIN
select CONVERT(INT,ROW_NUMBER() OVER (ORDER BY ORDER_LINE)) OrderDetailID, CONVERT(VARCHAR(100),PRODUCT_ID) ProductID,QUANTITY_ORDERED QuantityOrdered,CONVERT(numeric(36,2),PRICE_EACH) PriceEach,ORDER_LINE OrderLine from E_ORDER_DETAIL
WHERE Convert(varchar(100),ORDER_ID)=@ORDER_NUMBER

END
GO
/****** Object:  StoredProcedure [dbo].[SpGetOrder]    Script Date: 18/04/2022 7:04:13 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jairo,Moreno
-- Create date: <Create Date,,>
-- Description:	Obtener orden(s) por Id o cliente
-- =============================================

CREATE PROCEDURE [dbo].[SpGetOrder]
@ORDER_NUMBER  varchar(100)
,@CUSTOMER_ID varchar(100)
AS
BEGIN

SELECT Convert(varchar(100),ORDER_NUMBER) OrderID,ORDER_DATE OrderDate,DATE_REQUIRED DateRequiered,DATE_SHIPPED DateShipped,COMMENT Comment,B.STATE Status,Convert(varchar(100),CUSTOMER_ID) Customer FROM E_ORDER A
INNER JOIN E_order_state B ON A.STATUS_ID=B.ID
WHERE (ORDER_NUMBER=@ORDER_NUMBER)
OR (CUSTOMER_ID=@CUSTOMER_ID )

END
GO
