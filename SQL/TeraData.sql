USE [TeraData]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[orderUser] [varchar](50) NULL,
	[orderDate] [date] NULL,
	[orderTotal] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](25) NULL,
	[productCost] [int] NULL,
	[productMeasurementUnit] [varchar](2) NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  Table [dbo].[Sites]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sites](
	[siteId] [int] IDENTITY(1,1) NOT NULL,
	[siteName] [varchar](50) NULL,
	[siteUbication] [varchar](max) NULL,
 CONSTRAINT [PK_Sites] PRIMARY KEY CLUSTERED 
(
	[siteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteProduct]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE     PROCEDURE [dbo].[DeleteProduct]
@productId int



AS


BEGIN

DELETE FROM Products WHERE productId=@productId; 

END;

GO
/****** Object:  StoredProcedure [dbo].[DeleteRole]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[EditProduct]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[EditProduct]
@productId int,
@productName varchar(25),
@productCost int,
@productMeasurementUnit varchar(2)


AS


BEGIN

UPDATE Products SET 
productName=@productName,
productCost =@productCost,
productMeasurementUnit=@productMeasurementUnit

WHERE productId=@productId;

END;

GO
/****** Object:  StoredProcedure [dbo].[EditRole]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[EditUser]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[InsertProduct]
@productName varchar(50),
@productCost int,
@productMeasurementUnit varchar(2)

AS


BEGIN

INSERT INTO Products(productName,productCost, productMeasurementUnit) VALUES (@productName,@productCost,@productMeasurementUnit );

END;



GO
/****** Object:  StoredProcedure [dbo].[InsertRole]    Script Date: 15/5/2023 21:30:23 ******/
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
/****** Object:  StoredProcedure [dbo].[InsertUser]    Script Date: 15/5/2023 21:30:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE     PROCEDURE [dbo].[InsertUser]
@userEmail varchar(60),
@userPassword varchar(max)


AS
BEGIN


INSERT INTO Users(userEmail,userPassword) VALUES (@userEmail,@userPassword);


END;



/****** Object:  StoredProcedure [dbo].[Editar_Rol]    Script Date: 17/11/2022 00:02:30 ******/
SET ANSI_NULLS ON
GO
