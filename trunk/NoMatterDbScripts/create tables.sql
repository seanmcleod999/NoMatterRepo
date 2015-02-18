-- TABLES

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
	[Enabled] [bit] NOT NULL
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
	[ReleaseDate] [date] NULL,
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

CREATE TABLE [dbo].[Section](
	[SectionId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[SectionUUID] [uniqueidentifier] NOT NULL,
	[SectionName] [varchar](50) NOT NULL,
	[ControllerName] [varchar](50) NULL
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [int] NOT NULL,
	[UserUUID] [uniqueidentifier] NOT NULL,
	[CredentialTypeId] [tinyint] NOT NULL,
	[Identifier] [varchar](100) NOT NULL,
	[Password] [binary](200) NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[Email] [varchar](100) NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[DateAdded] [datetime] NOT NULL,
	[DateUpdated] [datetime] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Hidden]  DEFAULT ((0)) FOR [Hidden]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Reserved]  DEFAULT ((0)) FOR [Reserved]
GO

ALTER TABLE [dbo].[Product] ADD  CONSTRAINT [DF_Product_Sold]  DEFAULT ((0)) FOR [Sold]
GO


-- INDEXES

ALTER TABLE [dbo].[Client] ADD  CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientId] ASC
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


-- FOREIGN KEYS

ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Section] FOREIGN KEY([SectionId])
REFERENCES [Section] ([SectionId])
GO

ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Section]
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
GO

ALTER TABLE [dbo].[DiscountProduct] CHECK CONSTRAINT [FK_DiscountProduct_Product]
GO

ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [Category] ([CategoryId])
GO

ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO

ALTER TABLE [dbo].[ProductKeyword]  WITH CHECK ADD  CONSTRAINT [FK_ProductKeyword_Product] FOREIGN KEY([ProductId])
REFERENCES [Product] ([ProductId])
GO

ALTER TABLE [dbo].[ProductKeyword] CHECK CONSTRAINT [FK_ProductKeyword_Product]
GO

ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Section_Section] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[Section] CHECK CONSTRAINT [FK_Section_Section]
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Client] FOREIGN KEY([ClientId])
REFERENCES [Client] ([ClientId])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Client]
GO


-- CHECKS

