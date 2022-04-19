USE [EcommerceProducts]
GO
/****** Object:  StoredProcedure [dbo].[SpGetProductProviders]    Script Date: 12/04/2022 23:28:44 ******/
DROP PROCEDURE [dbo].[SpGetProductProviders]
GO
ALTER TABLE [dbo].[ProductSpecification] DROP CONSTRAINT [FK_ProductEspecification_Product]
GO
ALTER TABLE [dbo].[ProductReview] DROP CONSTRAINT [FK_ProductReview_Product]
GO
ALTER TABLE [dbo].[ProductImage] DROP CONSTRAINT [FK_ProductImage_Product]
GO
ALTER TABLE [dbo].[ProductDetail] DROP CONSTRAINT [FK_ProductDetail_Product]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ProductProvider]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ProductCondition]
GO
ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_Product_ProductCategory]
GO
ALTER TABLE [dbo].[BestProductByReview] DROP CONSTRAINT [FK_BestProductByReview_Product]
GO
/****** Object:  Table [dbo].[ProductSpecification]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductSpecification]') AND type in (N'U'))
DROP TABLE [dbo].[ProductSpecification]
GO
/****** Object:  Table [dbo].[ProductReview]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductReview]') AND type in (N'U'))
DROP TABLE [dbo].[ProductReview]
GO
/****** Object:  Table [dbo].[ProductProvider]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductProvider]') AND type in (N'U'))
DROP TABLE [dbo].[ProductProvider]
GO
/****** Object:  Table [dbo].[ProductImage]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductImage]') AND type in (N'U'))
DROP TABLE [dbo].[ProductImage]
GO
/****** Object:  Table [dbo].[ProductDetail]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductDetail]') AND type in (N'U'))
DROP TABLE [dbo].[ProductDetail]
GO
/****** Object:  Table [dbo].[ProductCondition]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCondition]') AND type in (N'U'))
DROP TABLE [dbo].[ProductCondition]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductCategory]') AND type in (N'U'))
DROP TABLE [dbo].[ProductCategory]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 12/04/2022 23:28:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
DROP TABLE [dbo].[Product]
GO
/****** Object:  Table [dbo].[BestProductByReview]    Script Date: 12/04/2022 23:28:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BestProductByReview]') AND type in (N'U'))
DROP TABLE [dbo].[BestProductByReview]
GO
/****** Object:  UserDefinedFunction [dbo].[fnCalculateScoreByproduct]    Script Date: 12/04/2022 23:28:45 ******/
DROP FUNCTION [dbo].[fnCalculateScoreByproduct]
GO
