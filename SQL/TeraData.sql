USE [TeraData]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[orderUserId] [int] NULL,
	[orderDate] [date] NULL,
	[orderTotal] [bigint] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[orderDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[orderId] [int] NOT NULL,
	[productId] [int] NOT NULL,
	[orderDetailsQuantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[orderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](25) NULL,
	[productCost] [int] NULL,
	[productMeasurementUnit] [varchar](15) NULL,
	[categoryId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL,
	[roleDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[roleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sites]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[siteId] [int] IDENTITY(1,1) NOT NULL,
	[siteName] [varchar](50) NOT NULL,
	[siteUbication] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[siteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[userFirstSurname] [varchar](50) NULL,
	[userSecondSurname] [varchar](50) NULL,
	[userGovId] [varchar](15) NULL,
	[userEmail] [varchar](50) NOT NULL,
	[userPassword] [varchar](50) NOT NULL,
	[userActive] [bit] NULL,
	[userRoleId] [int] NULL,
	[userSiteId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Products] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Products] ([productId])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Products]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Users] FOREIGN KEY([orderUserId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Users]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([orderId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([productId])
REFERENCES [dbo].[Products] ([productId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([userRoleId])
REFERENCES [dbo].[Roles] ([roleId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Sites] FOREIGN KEY([userSiteId])
REFERENCES [dbo].[Sites] ([siteId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Sites]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create     PROCEDURE [dbo].[DeleteCategory]
@categoryId int



AS


BEGIN

DELETE FROM Categories WHERE categoryId=@categoryId; 

END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteOrder]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteOrder]
    @orderId INT
AS
BEGIN
    DELETE FROM [Order]
    WHERE orderId = @orderId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteOrderDetails]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteOrderDetails]
    @orderDetailsId INT
AS
BEGIN
    DELETE FROM OrderDetails
    WHERE orderDetailsId = @orderDetailsId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[DeleteProduct]
    @productId INT
AS
BEGIN
    DELETE FROM Products
    WHERE productId = @productId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[DeleteRole]
@roleId int



AS


BEGIN

DELETE FROM Roles WHERE roleId=@roleId; 

END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteSite]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteSite]
    @siteId INT
AS
BEGIN
    DELETE FROM Sites
    WHERE siteId = @siteId;
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE     PROCEDURE [dbo].[DeleteUser]
@userId int



AS


BEGIN

DELETE FROM Users WHERE userId=@userId; 

END;

GO
/****** Object:  StoredProcedure [dbo].[EditCategory]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[EditCategory]
@categoryId int,
@CategoryName varchar(50)

AS


BEGIN

UPDATE Categories SET CategoryName = @CategoryName WHERE categoryId = @categoryId;

END;


GO
/****** Object:  StoredProcedure [dbo].[EditOrder]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EditOrder]
    @orderId INT,
    @orderUserId VARCHAR(50),
    @orderDate DATE,
    @orderTotal INT
AS
BEGIN
    UPDATE [Order]
    SET orderUserId = @orderUserId,
        orderDate = @orderDate,
        orderTotal = @orderTotal
    WHERE orderId = @orderId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EditOrderDetails]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EditOrderDetails]
    @orderDetailsId INT,
    @orderId INT,
    @productId INT,
    @orderDetailsQuantity INT
AS
BEGIN
    UPDATE OrderDetails
    SET orderId = @orderId,
        productId = @productId,
        orderDetailsQuantity = @orderDetailsQuantity
    WHERE orderDetailsId = @orderDetailsId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EditProduct]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EditProduct]
    @productId INT,
    @productName VARCHAR(25),
    @productCost INT,
    @productMeasurementUnit VARCHAR(15)
AS
BEGIN
    UPDATE Products
    SET productName = @productName,
        productCost = @productCost,
        productMeasurementUnit = @productMeasurementUnit
    WHERE productId = @productId;
END;


GO
/****** Object:  StoredProcedure [dbo].[EditRole]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[EditRole]
@roleId int,
@roleName varchar(50),
@roleDescription varchar(MAX)

AS


BEGIN

UPDATE Roles SET roleName = @roleName, roleDescription = @roleDescription WHERE roleId = @roleId;

END;


GO
/****** Object:  StoredProcedure [dbo].[EditSite]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditSite]
    @siteId INT,
    @siteName VARCHAR(50),
    @siteUbication VARCHAR(MAX)
AS
BEGIN
    UPDATE Sites
    SET siteName = @siteName, siteUbication = @siteUbication
    WHERE siteId = @siteId;
END;
GO
/****** Object:  StoredProcedure [dbo].[EditUser]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EditUser]
@userId int,
@userGovId varchar(9),
@userName varchar(50),
@userFirstSurname varchar(50),
@userSecondSurname varchar(50),
@userEmail varchar(50),
@userPassword varchar(50),
@userActive bit,
@userRoleId int,
@userSiteId int

AS


BEGIN
UPDATE Users SET 
userGovId = @userGovId,
userName = @userName,
userFirstSurname = @userFirstSurname,
userSecondSurname = @userSecondSurname,
userEmail = @useremail,
userPassword = @userPassword,
userActive=@userActive,
userRoleId=@userRoleId,
userSiteId =@userSiteId
 
WHERE userId=@userId;


END;
GO
/****** Object:  StoredProcedure [dbo].[GetLastOrderId]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetLastOrderId]
	@orderUserId int
AS
BEGIN
	select top 1 orderId from [Order] where orderUserId = @orderUserId Order by orderId desc
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetailsWithProductNames]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetOrderDetailsWithProductNames]
	
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT OD.[orderDetailsId], OD.[orderId], P.[productName], OD.[orderDetailsQuantity]
    FROM [dbo].[OrderDetails] AS OD
    INNER JOIN [dbo].[Products] AS P ON OD.[productId] = P.[productId]
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrderDetailsWithProductNamesByUser]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetOrderDetailsWithProductNamesByUser]
	@orderOd int
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT OD.[orderDetailsId], OD.[orderId], P.[productName], OD.[orderDetailsQuantity]
    FROM [dbo].[OrderDetails] AS OD
    INNER JOIN [dbo].[Products] AS P ON OD.[productId] = P.[productId]
	Where OD.orderId = @orderOd
    
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersWithUserNames]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create   PROCEDURE [dbo].[GetOrdersWithUserNames]
	
AS
BEGIN
	SET NOCOUNT ON;
    SELECT 
	O.[orderId],CONCAT(U.[userName], ' ', U.[userFirstSurname], ' ', U.[userSecondSurname]) 
		AS 
		FullName,
	O.orderDate,
	O.[orderTotal]  
    FROM 
	[dbo].[Order] 
	AS O
    INNER JOIN [dbo].[Users] AS U ON O.[orderUserId] = U.[userId]
END
GO
/****** Object:  StoredProcedure [dbo].[GetOrdersWithUserNamesByUser]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[GetOrdersWithUserNamesByUser]
	@orderUserId int
AS
BEGIN
	SET NOCOUNT ON;
    SELECT 
	O.[orderId],CONCAT(U.[userName], ' ', U.[userFirstSurname], ' ', U.[userSecondSurname]) 
		AS 
		FullName,
	O.orderDate,
	O.[orderTotal]  
    FROM 
	[dbo].[Order] 
	AS O
    INNER JOIN [dbo].[Users] AS U ON O.[orderUserId] = U.[userId]
	WHERE U.[userId] = @orderUserId
END
GO
/****** Object:  StoredProcedure [dbo].[GetProductsWithCategoryNames]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetProductsWithCategoryNames]

AS
BEGIN

	SET NOCOUNT ON;

    SELECT p.productId, p.productName, p.productCost, p.productMeasurementUnit, c.categoryName
FROM Products p
INNER JOIN Categories c ON p.categoryId = c.categoryId;

END
GO
/****** Object:  StoredProcedure [dbo].[GetUsersWithRoleAndSiteNames]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsersWithRoleAndSiteNames]
AS
BEGIN
   

     SELECT
        U.userId,
        U.userName,
        U.userFirstSurname,
        U.userSecondSurname,
        U.userGovId,
        U.userEmail,
        U.userPassword,
        U.userActive,
        R.roleName,
        S.siteName
    FROM
        Users U
        INNER JOIN Roles R ON U.userRoleId = R.roleId
        INNER JOIN Sites S ON U.userSiteId = S.siteId
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[InsertCategory]
@CategoryName varchar(50)

AS
BEGIN


INSERT INTO Categories (CategoryName) VALUES (@CategoryName);


END;

GO
/****** Object:  StoredProcedure [dbo].[InsertOrder]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[InsertOrder]
    @orderUserId VARCHAR(50),
    @orderDate DATE,
    @orderTotal INT
AS
BEGIN
    INSERT INTO [Order] (orderUserId, orderDate, orderTotal)
    VALUES (@orderUserId, @orderDate, @orderTotal);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderDetails]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[InsertOrderDetails]
    @orderId INT,
    @productId INT,
    @orderDetailsQuantity INT
AS
BEGIN
    INSERT INTO OrderDetails (orderId, productId, orderDetailsQuantity)
    VALUES (@orderId, @productId, @orderDetailsQuantity);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[InsertProduct]
    @productName VARCHAR(25),
    @productCost INT,
    @productMeasurementUnit VARCHAR(15)
AS
BEGIN
    INSERT INTO Products (productName, productCost, productMeasurementUnit)
    VALUES (@productName, @productCost, @productMeasurementUnit);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertRole]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[InsertRole]
@roleName varchar(50),
@roleDescription varchar(MAX)

AS
BEGIN


INSERT INTO Roles(roleName,roleDescription) VALUES (@roleName,@roleDescription);


END;

GO
/****** Object:  StoredProcedure [dbo].[InsertSite]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertSite]
    @siteName VARCHAR(50),
    @siteUbication VARCHAR(MAX)
AS
BEGIN
    INSERT INTO Sites (siteName, siteUbication)
    VALUES (@siteName, @siteUbication);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[InsertUser]
@userEmail varchar(60),
@userPassword varchar(max),
@userRoleId int,
@userSiteId int


AS
BEGIN


INSERT INTO Users(userEmail,userPassword,userActive ,userRoleId, userSiteId) VALUES (@userEmail,@userPassword, 1 ,@userRoleId,@userSiteId);


END;



/****** Object:  StoredProcedure [dbo].[Editar_Rol]    Script Date: 17/11/2022 00:02:30 ******/
SET ANSI_NULLS ON
GO
/****** Object:  StoredProcedure [dbo].[OrdersReportBySite]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE  [dbo].[OrdersReportBySite]
@Site varchar(50)
AS
BEGIN
	
	SET NOCOUNT ON;
	SELECT
  products.productName as 'Product',
  products.productMeasurementUnit,
  SUM(orderDetails.orderDetailsQuantity) AS totalQuantity,
  Products.productCost,
  Sum(OrderDetails.orderDetailsQuantity) * Products.productCost as 'Total',
  [Order].orderDate,
  Roles.roleName,
  Sites.siteName
FROM
  orderDetails
  JOIN [Order] ON orderDetails.orderID = [Order].orderID
  INNER JOIN products ON orderDetails.productID = products.productID
  INNER JOIN users ON [Order].orderUserId = users.userID
  INNER JOIN Roles ON users.userRoleId = Roles.roleId
  INNER JOIN sites ON users.userSiteId = sites.siteID
  where 
  [Order].orderDate < GETDATE() and [Order].orderDate > GETDATE()-7 and 
  Sites.siteName = @Site


GROUP BY
  products.productName,
  products.productMeasurementUnit,
  Products.productCost,
  [Order].orderDate,
  Roles.roleName,
  Sites.siteName;
   
END
GO
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE     PROCEDURE [dbo].[ResetPassword]
@userId int,
@userPassword varchar(50)

AS


BEGIN
UPDATE Users SET 
userPassword = @userPassword 
WHERE userId=@userId;


END;
GO
/****** Object:  StoredProcedure [dbo].[SelectRawData]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SelectRawData] 
	
AS
BEGIN
	
	SET NOCOUNT ON;
SELECT
  products.productName,
  products.productMeasurementUnit,
  categories.categoryName,
  products.productCost,
  Sites.siteName,
  Roles.roleName,
  orderDetails.orderDetailsQuantity,
  [Order].orderDate
  
FROM
  orderDetails
  INNER JOIN [Order] ON orderDetails.orderID = [Order].orderID
  INNER JOIN products ON orderDetails.productID = products.productID
  INNER JOIN users ON [Order].orderUserId = users.userID
  INNER JOIN Roles ON users.userRoleId = Roles.roleId
  INNER JOIN sites ON users.userSiteId = sites.siteID
  INNER JOIN categories ON products.categoryId = categories.categoryId
GROUP BY
  products.productName,
  products.productMeasurementUnit,
  categories.categoryName,
  products.productCost,
  Sites.siteName,
  Roles.roleName,
  orderDetails.orderDetailsQuantity,
  [Order].orderDate;

END
GO
/****** Object:  StoredProcedure [dbo].[SelectRawDataFiltered]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create   PROCEDURE [dbo].[SelectRawDataFiltered] 
	
AS
BEGIN
	
	SET NOCOUNT ON;
SELECT
  products.productName,
  products.productMeasurementUnit,
  categories.categoryName,
  products.productCost,
  Sites.siteName,
  Roles.roleName,
  orderDetails.orderDetailsQuantity,
  [Order].orderDate
  
FROM
  orderDetails
  INNER JOIN [Order] ON orderDetails.orderID = [Order].orderID
  INNER JOIN products ON orderDetails.productID = products.productID
  INNER JOIN users ON [Order].orderUserId = users.userID
  INNER JOIN Roles ON users.userRoleId = Roles.roleId
  INNER JOIN sites ON users.userSiteId = sites.siteID
  INNER JOIN categories ON products.categoryId = categories.categoryId

WHERE
  [Order].orderDate >= DATEADD(DAY, -31, GETDATE())

GROUP BY
  products.productName,
  products.productMeasurementUnit,
  categories.categoryName,
  products.productCost,
  Sites.siteName,
  Roles.roleName,
  orderDetails.orderDetailsQuantity,
  [Order].orderDate;




END
GO
/****** Object:  StoredProcedure [dbo].[ValidateCredentials]    Script Date: 15/6/2023 12:35:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ValidateCredentials]
    @userEmail varchar(50),
    @userPassword varchar(max)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[Users]
    WHERE userEmail = @userEmail
    AND userPassword = @userPassword;
END
GO
