  --Add reviews dummy para productos
  Insert  into [EcommerceProducts].[dbo].[ProductReview] (
       [ProductId]
      ,[UserId]
      ,[UserName]
      ,[Review]
      ,[Value]
      ,[Status]
      ,[CreatedAt])
  select 
       [Id]
      ,1
      ,'wepea93'
      ,'Lorem ipsum dolor sit amet consectetur adipiscing elit, tortor aliquam felis sagittis natoque urna, velit vitae etiam gravida ridiculus scelerisque. Eu augue malesuada tincidunt himenaeos scelerisque feugiat nulla aenean quisque in torquent, ultrices quis erat massa consequat enim vivamus risus fermentum faucibus sapien, justo fusce nullam magna libero tempus lacus conubia vulputate leo. Placerat tortor mollis nisl scelerisque habitant augue mattis est, phasellus cum nec gravida cras arcu.'
	  ,FLOOR(rand() * 5 + 1)
      ,1
      ,GETDATE()
  FROM [EcommerceProducts].[dbo].[Product];

    --Add review dummy para producto 1000 -> se usa para probar paginador
    Insert  into [EcommerceProducts].[dbo].[ProductReview] (
       [ProductId]
      ,[UserId]
      ,[UserName]
      ,[Review]
      ,[Value]
      ,[Status]
      ,[CreatedAt])
  select 
       1000
      ,1
      ,'wepea93'
      ,'Lorem ipsum dolor sit amet consectetur adipiscing elit, tortor aliquam felis sagittis natoque urna, velit vitae etiam gravida ridiculus scelerisque. Eu augue malesuada tincidunt himenaeos scelerisque feugiat nulla aenean quisque in torquent, ultrices quis erat massa consequat enim vivamus risus fermentum faucibus sapien, justo fusce nullam magna libero tempus lacus conubia vulputate leo. Placerat tortor mollis nisl scelerisque habitant augue mattis est, phasellus cum nec gravida cras arcu.'
	  ,FLOOR(rand() * 5 + 1)
      ,1
      ,GETDATE();

	  --Eliminacion review producto 1001 -> probar no se obtengan reviews
	  delete from [EcommerceProducts].[dbo].[ProductReview]  where  [ProductId] = 1001;