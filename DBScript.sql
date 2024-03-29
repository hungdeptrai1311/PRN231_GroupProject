CREATE DATABASE [GroupProject]
GO
USE [GroupProject]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 3/12/2024 12:27:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](150) NULL,
	[BrandImage] [varchar](max) NULL,
	[Description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 3/12/2024 12:27:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK__Cart__51BCD7B73C719FD3] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/12/2024 12:27:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[CategoryImage] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 3/12/2024 12:27:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Date] [datetime] NOT NULL,
	[Status] [nvarchar](20) NULL,
 CONSTRAINT [PK__Order__C3905BCF83ADBA1F] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 3/12/2024 12:27:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[OrderId] [int] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/12/2024 12:27:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[ProductImage] [varchar](max) NULL,
	[BrandId] [int] NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/12/2024 12:27:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/12/2024 12:27:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](10) NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (1, N'MSI', N'data:image/jpeg;base64,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', N'MSI is a world leader in AI PC, gaming, content creation, business & productivity and AIoT solutions. Bolstered by its cutting-edge R&D capabilities and customer-driven innovation, MSI has a wide-ranging global presence spanning over 120 countries.')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (2, N'DELL', N'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAM4AAADOCAMAAABBwc32AAAAdVBMVEX///8AfbgAe7cAebYAd7UAdbS80+UAcrPg6/MAcLLy9vr3+/1UjsBoo8tbm8djoMnX5/EAbLBelcRBkMEAaa/p8fd2rNAvgrvR4u6Lt9bH3esohLuawdwsiL2jxd4SgbqDsNJpnMeuzOJDir91pc0AYqxQh72ALaoBAAAQDUlEQVR4nNVd2bqqOgyWDkxdIvOkIIL7vP8jHnBoA9S5KORqf3th6U/SJE2TdLWailiU6mUWr4Og2O81bb8vimATZ6WeRmyyl6onZkeunjmaF3qWZVCCNaydqP0HIdSwvDBEQVynkT13WLbp6lVAQ8sgCGm3CZEWlRVUdWrav57zDWJm2kIxPEruAYGEqeUVcZ2bs+MSc/Vyo3kUP4mEs6mFRJyd7s4JkdmuFWQ8zZURlwzSyp37axQX0qvD/n0sFyYZml/Vv19H0e7QkM+wXBARuvUz86dgzPhv//J6uYMINcf8d2COCbqrj1vChFJqnYnSR3xEiGBf/wmYyLmzYNDJsoQhLZy4ynYnqqrYKVAYPrBKyGi+z6HI+ScVMtQiaTlCirjMbywEMy/jonUQMLrBWxxu828qbmZXIZVhwURLGr/Sn1jRrXL3m6QVLhkiHDrfs61RSY3xJFqLuP07ZPkL2pbl2eFvK3UjiFd9B5CtBxIwnZqN6zf0rFnHfotoDMjYl9OrbZbGZPTu1gg62TtYzmTWlaONPxGmjj4xg6JdQ4fvJUbxsYti6lVhjD4TTapJGZRv8OCdiHpOmSrwTlhaO9bwU2Hql58PfYuyhg7BhM4tffw6mbkTDgGRZD2RJxf5eGBqqLdOI6WvSNfe8IuRaayqngwUKvGcVPlSZa7jDeQZ40z1W1pBG7iayJrKF0kbq//dEHUUC1zkWP034KRW+wZI9VAQaJOqHD/9MwYLtJrUIrAq6S8hqvLr6X2NRjRf6ceSUeprvSWEcanoA7KyPzL926kZ+D7t/nrfEBmZEiXKdgQqAYKPk7PmTOkRwc+IrKMKPFXPm6LJ7msBCrvsC7lx+Nhis2MfjfPNjRXLnT4e/0PXkB3heMiolDoBjykayMZneNgBSi+26q8HK1nds6nk7wM87NhDE/4kUOl6PU30AX96kkboj+LIrLdXpf6b+oBVgM+IBmon+QoFcNtgHN5av2wHBkFk/cMYP1vDeI/xlv0pARqM4p+Gw+0KgQVkveEw6hoGaLIfB/ftDOBB1steVtqQGaEZ4sEv+tcR8P/QDNCc8ADh117bOjpif4PIb9fNlewK6AOavDKnDOw96VShlFfJXgMzSJ3nf6gDpUaC2ZzCsgDYU/p0PCRKgFKjU07wVYLhF/zs8vGBUgtnw5uOmAfUQfOcNc3EkkPWXI7HL+QCv4usn/lF3giO0gmjT+9RDQ3IE/HraCN+QKcNP71DrBLTe2Lzw3aAN86X957PUOTAz/3IhgDnhqgNPaqiXMwQaQ8O7aMYyKaqQJ1aYiXQBs79vZwORO04E29gSDbYJNPdvU8eCbtL/mYpah2lf3yWGN1hDytBZP0rkdv3aCdmeW8rZwtfjfhfnN7LBPwW77ayrjhzUDJbUesoTcR3vxmSiULxUPXFyb1BlWBPeMsVFQYKJbPU0YKYYA/eyh+J/okFNjtfbUi12GCGclvqcJtDmi9P7g0SvgGWztYVStqbtR44U+rx6Roy9mw43Ff24b8jsNIlRsXkiwstgTkde7g2SMbKLeZ/pL8MRz9PTAR20HH4x6jhzAkXwZyWPdxM4mboGuz2YuXMcNMmI7GRQ2gYpTpwzlm/S8x+kXJue4jfZ4HO4x2PdkQzItO5KmO07dv9jGtpY8L0PtVUc1NpVPD/TX5CjYqFKIKO0oIrAwdOW99f/9/IFqGlz8SyK3uQVsv+W3sUG5kX6cLRAbtS11mgIuhIKAMcCGnT+cEWrX43t3co46aHcLESEY+hwps91dvxoje5M03eTan4FZk8CCKWSapxWYt/Ork3SERtydWb0b2lyhqUNusSg7b5GQP+Uy1rLJ/YnzX/MJescxDaDLiaPih+mb2zthOvRu7P4OL8Jh4kGPvZK1NCz1eym+0OiyK5h87EgDeGi8Qb77yDZ09cN9F86Ug2qUdnRJtjtaufkSGm+12QGNNSdhRhNnxEOf/szL8+cO/MPU+usz9H08TxHGpG7/2PkhFRahjFsXzkqrLd9iwHmMiKitx/+DwcvnGcbG+M6wvvhWLsRqjljs02XzqSiMifvGQXIUqC7O7JZBTznCCEnbHAueF1MHoDzpp7+Zt7L/K5V1103Ij4sYGksOQGnJPeQH93judymKOOjfHpygtw8F04GZ+iFfXGJWNv+g6c9qOjm+k6uwTkmxAsqXVXBkfnbzrpAhHtJWMRvwenA1TIVcIa5vnTrWy+yuCYHM5JF4gtEB4/ex9OV5Uo4Y/Zq1705AtZGZwVT1IxOhdNxA+Ku3DQhfp4sDf6TY3hsgGJjhFcm+rg8B326eSK+59E4n8KOAknrVf6RLZ9fTBI8N1yfc5yKwTxVXVwYs4PBJOQZDGcKxxc1PmF6uqwhenifS/chSUYGG342rK74jlDmEx1cMT5tMdWER9WFi+8wkEBVBP6cQ8UFwYKUffB2TdJxKm/GZ+YRoJrhYk6OCJ6GEbgnCSU2G45nHZ9NGLaOLiqA1ZqgG8kEDhz5yKCJLm4POrgmBxBq6l1gU3y6C04qxyy4SKlZgWyATGJ+TQZqCsiybloRh2clZAvfVW+B6eXi3QO0reCBrJlcCmWTazBnO7NdHBKYHbQS3BWOtf3aN/Zr3IL1pOxFQW/JixuxvRS0aQQjohDZWKzjSVm5x4cJrKTadVVCQD9HIJDFR36O5jslKuCVXF9sFWyPIojdcPvwGnFjVsspz5AaxOCPKYMWiki9nKROjjcESCbVSD22i/CAaFgDOdMQUow69Xi0ULAFDmaH8MRAhasAj5q9SIcECEGhODpnQsrJREV02JIpDl8DEekhwar4sod62U4rj92UDESRxCshpXTrVHlf0hhfvfncK6fpl3+/ExUWhRzF47dq427PCcidXavHBh0UbDrXs+Ij+HsOKf3n8ABXL6i2QpHwI2BGUJowweIql7Vs1o4fFRLdor4Ihxtu7sunIH3JkqA3PWwGcGncIQnoCmGg3F8fnAHvW7aiN117g+7kyiF84GwsTGcdkvbSVVfnqgj9d6UwVG0dqKxKuheHphpjwNWLDR33KvZnwDOB4o6lSjqjj9NgqGLAAq1A1kHM6WKuuCjvgynBhDgNOGcCYhxmtB5wOq8AiHzhRInBwUbbSxE3R8sUGqmG9AM7fkWX6mT874LmosQysHOG0mXNURBlkIGgwitv8P1kVIXlH+kDzYIJOv0wrAPHoJGNVr3gwjtAr7+h7oNAonF9g2/un3jSwQVp2ln+77AIZBYkjqCN4gEeRfAUgaHT8TIBM/Ri5vrQGz/N2cjCeMhHXNEIgZstIPRsRtLIRy4uX4z9JEKH0bkw6RHaG+Qdil+YjAAT7fnVi6TwNHfDUwFoIrZ4VyIMtiOrvV4OgNqVyJWgGhwafCnDk4vMPVO2DCvwDLBCEyH9QWuy11wgYuA6Po6DoCTshH14KDNSvYAnw0MG7Lw1aBuduidEAwS/FOn31IDOmnIEo61gIP8Ef1nQzjadvQArJwWQd2uZpfrhedD7tC38Ya/suE+p7WuIMTmgZ5BAo6Gh6ev+F/Ug6ONHjAAnF7IfcXrw549EOmZF9npTWn0FAJnowZFBMAZUziAMyLYFUakHHYHIp8cV6ERb07kDvtTdo9a/UfVweHm++SmfXKYSG4VyAUDlweN+lkogzM4TBSa+sWjXoyC2ynXVQJ/iWAyoGI44Ki3m07EufPCQTzCFP/t7qV+1KD3CTo7AtPAEQfxxsnK3U2TkCwChAg1isPuQe6Qu7laT7KVAGc7S7KVu7ziDEe21708AOAM0iRgEsso7UECR9sGTlXqj5Nyouzs29BAlv3LdtZ46Cud4GyeggOTWE5GjesCNE4xyvUx5c/elGHXnQn1jnInxpUMfaVuP85k7+YkZghSjE4YeV2PJAHsQ2r3BV45bYKeaJ9zSQAzefRDeXreKqqmbiktyg0uiYB2PF3y5GrqqnqQPLm+iIFYPEtObTWu+Vu5OLRdcOKxcdVjpihBWHBaeHBVoGDrsV1UgUg/aZ8vU507jIstqYAtfVyRSb3UghcEfFwm+i4stRyJgmOKVa0JEVxksZhGYKDD9cUOe5mlfP2tlzhTMBZkSUWhJT32hEoovAUpA6EItH0/qGaLVi3LLFIe8ED42QssIdfw0DkzF13gLyltEb0xFtd+AY/3abmI8t/pczQjEs0xpD1peURkca1L8Dhk07JnsY1lqLQngeg4uay2P0iTrvWcH1wtqynTre7UW6ENFtQyS7NuPAPYs6CGZt4tSWLCB1pOuzmMbgqSOANeUDPA2/tNuCtdSKvGu9damXuuDfAiGmlq9N4sWQk6iC6hzalxv6uuCZLsl9CEdtQra0B6AnyDWW7kYIvgh/dzwezbWW7keg2cH98mYgIlOPP22vgZ8SmB/yD3VX9JsPn5c3cpggPwebem959SvXYDs4JmJW69iwMkDRqllMPLR+Z6rcOTotZRBk7053rpBnkhAgAr1uZ5JQqRxQdu/hLWcZCH7fm/Qr0LaxB+yaNME5iXN7vrhNCrBwM1+BSzu+xJs15OTymNOeEZoHld/Bm8IQmj366f/kVpb11kFx3B1nRO19i9ea1ldID5gLO5ZJC+u28x/RleAUnez+RwfTgQ/s3JQv+CTvxJXorbS55E1ve324PrU/FnOV1uT94Q/fHltuTTDDXX7+UH//bqYZp8nJNiHvp4GmkvvEloeDE0bRRk2ETH/uW/6EfXdmuGrySyFFX9BO7fXKquWQdFipXtcC93/xdX3iMrVpeNUmv9L0WTaUNWrEr6L0SGUic4Hw5PkgnPG+tkUC9DXr3+9RHZzqBrArKmuhYubQbFFmiKG+gyPCh+IZ7jKhc5ljreoIwDY2cKyR5XiVNvnSq1QlG69gZ1Ia1Yq66MuL5sPewFgWi4zpVpHDN3wmEjEIzVWBsplX/D1yFqOXWqQBhYWjvWePRm0m29W437LBCjqPQPWeTqWWGMR8bHiY+YbN2ho3o4ZGhO9US91Q0y68rRDDQcVTOaiWt/Ti/fIUmZF6HbQ1y/wSOzjv0tldSkUVp9ZbvIzGqoS08sImT7d8jyF2Sd5dnhr8UiqenDnqNWad6bhhuE8p4yBCWNnz2zkkw985sEkWFLjTN5SP9qJCzfSgFpqCvgJQQXcXlLg5t5GRfdM8P6bT6GZdVf38XrjWT18hkhYlheGKLCiatqtyvL3a6qYqdAYRhaBpG1/7lymP4o4UT3kxuyIibX9Uq3LkQpkS0TSBjj5nfpDPmxQY9m+Dy12JPD98UMkpv5W+MBi56jVsj8+PeJGdHJCD5ow/2QMZQEx3Im1Q9pXQXkfUTt8mqxfDPi9YhYqmcOscb+z2O2GEZQ1ens0mWYmddx4b0ACVMr3K7L3P352d4Nss1W7grLu2tZtJNxaR9qTW1qRjOSMRkxO0rrLGjtpXcyNfjaWgZ3HWTo2boGrdMQ2XPlioxYlOplFm+CoNjvNW2/L4pgE2elnk7IkP8Bl4n/yR3AlSQAAAAASUVORK5CYII=', N'Dell Inc. is an American technology company that develops, sells, repairs, and supports computers and related products and services. Dell is owned by its parent company, Dell Technologies.')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartId], [UserId], [ProductId], [Quantity]) VALUES (2, 1, 3, 3)
INSERT [dbo].[Cart] ([CartId], [UserId], [ProductId], [Quantity]) VALUES (3, 1, 2, 2)
INSERT [dbo].[Cart] ([CartId], [UserId], [ProductId], [Quantity]) VALUES (4, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryImage]) VALUES (1, N'IPhone', N'data:image/jpeg;base64,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')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryImage]) VALUES (2, N'Samsung', N's')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

INSERT [dbo].[Order] ([OrderId], [UserId], [Date], [Status]) VALUES (1, 1, CAST(N'2024-03-08T08:40:06.693' AS DateTime), N'Pending Approval')
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([OrderDetailId], [ProductId], [OrderId], [Quantity]) VALUES (1, 3, 1, 2)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ProductId], [OrderId], [Quantity]) VALUES (2, 2, 1, 1)
INSERT [dbo].[OrderDetail] ([OrderDetailId], [ProductId], [OrderId], [Quantity]) VALUES (3, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (1, N'iPhone 15 Pro Max 512GB', N'Chip Apple A17 Pro 6 nhân

RAM: 8 GB

Dung lượng: 512 GB

Camera sau: Chính 48 MP & Phụ 12 MP, 12 MP

Camera trước: 12 MP

Pin 4422 mAh, Sạc 20 W', 37990000, 30, N'https://cdn.tgdd.vn/Products/Images/42/305659/iphone-15-pro-max-black-thumbnew-600x600.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (2, N'iPhone 15 Pro Max 1T', N'Chip Apple A17 Pro 6 nhân', 47990000, 22, N'https://cdn.tgdd.vn/Products/Images/42/305659/iphone-15-pro-max-black-thumbnew-600x600.jpg', 2, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (3, N'iPhone 15 Pro Max 2T', N'Chip Apple A17 Pro 6 nhân', 57990000, 1, N'https://cdn.tgdd.vn/Products/Images/42/305659/iphone-15-pro-max-black-thumbnew-600x600.jpg', 1, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Email], [Password], [Name], [Address], [Phone], [RoleId]) VALUES (1, N'admin@gmail.com', N'1', N'ad', N'hn', N'012569856', 1)
INSERT [dbo].[User] ([UserId], [Email], [Password], [Name], [Address], [Phone], [RoleId]) VALUES (2, N'user@gmail.com', N'1', N'us', N'nh', N'0568569563', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([OrderId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brand] ([BrandId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
