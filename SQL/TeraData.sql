USE [TeraData]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 6/6/2023 08:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[orderUserId] [int] NULL,
	[orderDate] [date] NULL,
	[orderTotal] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 6/6/2023 08:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](25) NULL,
	[productCost] [int] NULL,
	[productMeasurementUnit] [varchar](15) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  Table [dbo].[Sites]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  Table [dbo].[Users]    Script Date: 6/6/2023 08:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [varchar](50) NULL,
	[userFirstSurname] [varchar](50) NULL,
	[userSecondSurname] [varchar](50) NULL,
	[userGovId] [varchar](9) NULL,
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
/****** Object:  StoredProcedure [dbo].[DeleteOrder]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteOrderDetails]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteSite]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[EditOrder]    Script Date: 6/6/2023 08:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[EditOrder]
    @orderId INT,
    @orderUser VARCHAR(50),
    @orderDate DATE,
    @orderTotal INT
AS
BEGIN
    UPDATE [Order]
    SET orderUser = @orderUser,
        orderDate = @orderDate,
        orderTotal = @orderTotal
    WHERE orderId = @orderId;
END;

GO
/****** Object:  StoredProcedure [dbo].[EditOrderDetails]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[EditProduct]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[EditRole]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[EditSite]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[EditUser]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[GetUsersWithRoleAndSiteNames]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertOrder]    Script Date: 6/6/2023 08:40:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[InsertOrder]
    @orderUser VARCHAR(50),
    @orderDate DATE,
    @orderTotal INT
AS
BEGIN
    INSERT INTO [Order] (orderUser, orderDate, orderTotal)
    VALUES (@orderUser, @orderDate, @orderTotal);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertOrderDetails]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertRole]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertSite]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[ResetPassword]    Script Date: 6/6/2023 08:40:21 ******/
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
/****** Object:  StoredProcedure [dbo].[ValidateCredentials]    Script Date: 6/6/2023 08:40:21 ******/
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
