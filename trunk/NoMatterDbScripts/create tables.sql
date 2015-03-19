-- TABLES

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CartProduct](
	[CartProductId] [smallint] IDENTITY(1,1) NOT NULL,
	[CartId] [varchar](50) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [smallint] NOT NULL,
	[DateAdded] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[SectionId] [int] NOT NULL,
	[CategoryUUID] [uniqueidentifier] NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[CategoryDescription] [varchar](max) NULL,
	[CategoryOrder] [smallint] NOT NULL,
	[ActionName] [varchar](20) NULL,
	[Hidden] [bit] NOT NULL,
	[Conditional] [bit] NOT NULL,
	[Picture] [varchar](50) NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[ClientId] [int] IDENTITY(1,1) NOT NULL,
	[ClientUUID] [uniqueidentifier] NOT NULL,
	[ClientName] [varchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Logo] [varchar](50) NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ClientDeliveryOption](
	[ClientDeliveryOptionId] [smallint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[DeliveryAmount] [decimal](18, 2) NOT NULL,
	[OptionOrder] [tinyint] NOT NULL,
	[Enabled] [bit] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ClientPaymentType](
	[ClientPaymentTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[PaymentTypeId] [smallint] NOT NULL,
	[PaymentTypeOrder] [tinyint] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Discount](
	[DiscountId] [smallint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[DiscountTypeId] [smallint] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[DiscountAmount] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DiscountProduct](
	[DiscountProductId] [smallint] IDENTITY(1,1) NOT NULL,
	[DiscountId] [smallint] NOT NULL,
	[ProductId] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DiscountType](
	[DiscountTypeId] [smallint] NOT NULL,
	[Description] [varchar](50) NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Enquiry](
	[EnquiryId] [smallint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[ContactNumber] [varchar](50) NULL,
	[Message] [varchar](500) NOT NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GlobalSetting](
	[GlobalSettingId] [tinyint] IDENTITY(1,1) NOT NULL,
	[SettingName] [varchar](50) NOT NULL,
	[SettingType] [tinyint] NOT NULL,
	[StringValue] [varchar](200) NULL,
	[IntValue] [smallint] NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Message] [varchar](300) NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[DeliveryAmount] [decimal](18, 2) NOT NULL,
	[ClientDeliveryOptionId] [smallint] NOT NULL,
	[CourierName] [varchar](50) NULL,
	[TrackingNumber] [varchar](50) NULL,
	[DateCreated] [datetime] NOT NULL,
	[PaymentType] [varchar](20) NULL,
	[Paid] [bit] NOT NULL,
	[OrderStatusId] [smallint] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderProduct](
	[OrderProductId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PaymentType](
	[PaymentTypeId] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Details] [varchar](200) NULL,
	[Picture] [varchar](50) NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductUUID] [uniqueidentifier] NOT NULL,
	[Title] [varchar](200) NOT NULL,
	[Description] [varchar](max) NULL,
	[AdminNotes] [varchar](200) NULL,
	[Size] [varchar](20) NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Hidden] [bit] NOT NULL,
	[Reserved] [bit] NOT NULL,
	[Sold] [bit] NOT NULL,
	[DateSold] [datetime] NULL,
	[Picture1] [varchar](50) NULL,
	[Picture2] [varchar](50) NULL,
	[Picture3] [varchar](50) NULL,
	[Picture4] [varchar](50) NULL,
	[Picture5] [varchar](50) NULL,
	[PictureOther] [varchar](50) NULL,
	[FacebookPostId] [varchar](20) NULL,
	[TwitterPostId] [varchar](20) NULL,
	[ProductShortUrl] [varchar](50) NULL,
	[ReleaseDate] [date] NOT NULL,
	[DateCreated] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductKeyword](
	[ProductId] [int] NOT NULL,
	[Keyword] [varchar](50) NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[RoleId] [smallint] NOT NULL,
	[RoleName] [varchar](50) NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Section](
	[SectionId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[SectionUUID] [uniqueidentifier] NOT NULL,
	[SectionName] [varchar](50) NOT NULL,
	[ControllerName] [varchar](50) NULL,
	[SectionDescription] [varchar](max) NULL,
	[Hidden] [bit] NOT NULL,
	[Picture] [varchar](50) NULL,
	[SectionOrder] [smallint] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Setting](
	[SettingId] [smallint] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[SettingName] [varchar](50) NOT NULL,
	[SettingType] [tinyint] NOT NULL,
	[StringValue] [varchar](200) NULL,
	[IntValue] [smallint] NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NULL,
	[UserUUID] [uniqueidentifier] NOT NULL,
	[CredentialTypeId] [tinyint] NOT NULL,
	[Identifier] [varchar](100) NOT NULL,
	[Password] [binary](200) NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[Email] [varchar](100) NULL,
	[FullName] [varchar](100) NULL,
	[ContactNumber] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[Suburb] [varchar](20) NULL,
	[City] [varchar](50) NULL,
	[Province] [varchar](20) NULL,
	[Country] [varchar](20) NULL,
	[PostalCode] [varchar](10) NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [smallint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [smallint] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClientDeliveryOption] ADD  CONSTRAINT [DF_ClientDeliveryOption_Enabled]  DEFAULT ((1)) FOR [Enabled]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Hidden]  DEFAULT ((0)) FOR [Hidden]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Reserved]  DEFAULT ((0)) FOR [Reserved]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Sold]  DEFAULT ((0)) FOR [Sold]
GO

ALTER TABLE [dbo].[Section] ADD  CONSTRAINT [DF_Section_Hidden]  DEFAULT ((0)) FOR [Hidden]
GO

ALTER TABLE [dbo].[Section] ADD  CONSTRAINT [DF_Section_SectionOrder]  DEFAULT ((0)) FOR [SectionOrder]
GO


-- INDEXES

ALTER TABLE [dbo].[CartProduct] ADD  CONSTRAINT [PK_CartProduct] PRIMARY KEY CLUSTERED 
(
	[CartProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClientDeliveryOption] ADD  CONSTRAINT [PK_ClientDeliveryOption] PRIMARY KEY CLUSTERED 
(
	[ClientDeliveryOptionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Enquiry] ADD  CONSTRAINT [PK_ClientEnquiry] PRIMARY KEY CLUSTERED 
(
	[EnquiryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ClientPaymentType] ADD  CONSTRAINT [PK_ClientPaymentType] PRIMARY KEY CLUSTERED 
(
	[ClientPaymentTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Setting] ADD  CONSTRAINT [PK_ClientSetting] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Discount] ADD  CONSTRAINT [PK_Discount] PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DiscountProduct] ADD  CONSTRAINT [PK_DiscountProduct] PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DiscountType] ADD  CONSTRAINT [PK_DiscountType] PRIMARY KEY CLUSTERED 
(
	[DiscountTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GlobalSetting] ADD  CONSTRAINT [PK_GlobalSetting] PRIMARY KEY CLUSTERED 
(
	[GlobalSettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PaymentType] ADD  CONSTRAINT [PK_PaymentType] PRIMARY KEY CLUSTERED 
(
	[PaymentTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProductKeyword] ADD  CONSTRAINT [PK_ProductKeyword] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC,
	[Keyword] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Section] ADD  CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
(
	[SectionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Category] ADD  CONSTRAINT [PK_SectionCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[User] ADD  CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [PK_UserOrder] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderProduct] ADD  CONSTRAINT [PK_UserOrderProduct] PRIMARY KEY CLUSTERED 
(
	[OrderProductId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserRole] ADD  CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


-- FOREIGN KEYS

ALTER TABLE [dbo].[CartProduct]  WITH CHECK ADD  CONSTRAINT [FK_CartProduct_Product] FOREIGN KEY([ProductId])
REFERENCES [Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[CartProduct] CHECK CONSTRAINT [FK_CartProduct_Product]
GO

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Section] FOREIGN KEY([SectionId])
REFERENCES [Section] ([SectionId])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Section]
GO

ALTER TABLE [dbo].[ClientDeliveryOption]  WITH CHECK ADD  CONSTRAINT [FK_ClientDeliveryOption_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[ClientDeliveryOption] CHECK CONSTRAINT [FK_ClientDeliveryOption_Client]
GO

ALTER TABLE [dbo].[Enquiry]  WITH CHECK ADD  CONSTRAINT [FK_ClientEnquiry_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[Enquiry] CHECK CONSTRAINT [FK_ClientEnquiry_Client]
GO

ALTER TABLE [dbo].[ClientPaymentType]  WITH CHECK ADD  CONSTRAINT [FK_ClientPaymentType_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[ClientPaymentType] CHECK CONSTRAINT [FK_ClientPaymentType_Client]
GO

ALTER TABLE [dbo].[ClientPaymentType]  WITH CHECK ADD  CONSTRAINT [FK_ClientPaymentType_PaymentType] FOREIGN KEY([PaymentTypeId])
REFERENCES [PaymentType] ([PaymentTypeId])
GO

ALTER TABLE [dbo].[ClientPaymentType] CHECK CONSTRAINT [FK_ClientPaymentType_PaymentType]
GO

ALTER TABLE [dbo].[Setting]  WITH CHECK ADD  CONSTRAINT [FK_ClientSetting_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[Setting] CHECK CONSTRAINT [FK_ClientSetting_Client]
GO

ALTER TABLE [dbo].[Discount]  WITH CHECK ADD  CONSTRAINT [FK_Discount_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[Discount] CHECK CONSTRAINT [FK_Discount_Client]
GO

ALTER TABLE [dbo].[Discount]  WITH CHECK ADD  CONSTRAINT [FK_Discount_DiscountType] FOREIGN KEY([DiscountTypeId])
REFERENCES [DiscountType] ([DiscountTypeId])
GO

ALTER TABLE [dbo].[Discount] CHECK CONSTRAINT [FK_Discount_DiscountType]
GO

ALTER TABLE [dbo].[DiscountProduct]  WITH CHECK ADD  CONSTRAINT [FK_DiscountProduct_Discount] FOREIGN KEY([DiscountId])
REFERENCES [Discount] ([DiscountId])
GO

ALTER TABLE [dbo].[DiscountProduct] CHECK CONSTRAINT [FK_DiscountProduct_Discount]
GO

ALTER TABLE [dbo].[DiscountProduct]  WITH CHECK ADD  CONSTRAINT [FK_DiscountProduct_Product] FOREIGN KEY([ProductId])
REFERENCES [Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[DiscountProduct] CHECK CONSTRAINT [FK_DiscountProduct_Product]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_ClientDeliveryOption] FOREIGN KEY([ClientDeliveryOptionId])
REFERENCES [ClientDeliveryOption] ([ClientDeliveryOptionId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_ClientDeliveryOption]
GO

ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Order] FOREIGN KEY([OrderId])
REFERENCES [Order] ([OrderId])
GO

ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Order]
GO

ALTER TABLE [dbo].[OrderProduct]  WITH CHECK ADD  CONSTRAINT [FK_OrderProduct_Product] FOREIGN KEY([ProductId])
REFERENCES [Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[OrderProduct] CHECK CONSTRAINT [FK_OrderProduct_Product]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [Category] ([CategoryId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO

ALTER TABLE [dbo].[ProductKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ProductKeyword_Product] FOREIGN KEY([ProductId])
REFERENCES [Product] ([ProductId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductKeyword] CHECK CONSTRAINT [FK_ProductKeyword_Product]
GO

ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Section_Section] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK_Section_Section]
GO

ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_UserOrder_User] FOREIGN KEY([UserId])
REFERENCES [User] ([UserId])
GO

ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_UserOrder_User]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [Role] ([RoleId])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [User] ([UserId])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO


-- CHECKS

