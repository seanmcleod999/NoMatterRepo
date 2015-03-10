

CREATE TABLE [dbo].[DatabaseVersion](
	[DatabaseVersionId] [int] IDENTITY(1,1) NOT NULL,
	[Version] [int] NOT NULL,
	[DateUpgraded] [datetime] NOT NULL CONSTRAINT [DF_DatabaseVersion_DateUpgraded]  DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[DatabaseVersionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--------------------------------------------------------------------

Insert into [DatabaseVersion] ([version]) values (1)

--------------------------------------------------------------------




ALTER TABLE dbo.Section ADD
	SectionDescription varchar(MAX) NULL,
	Hidden bit NOT NULL CONSTRAINT DF_Section_Hidden DEFAULT 0,
	Picture varchar(50) NULL,
	SectionOrder smallint NOT NULL CONSTRAINT DF_Section_SectionOrder DEFAULT 0
GO





CREATE TABLE [dbo].[Role](
	[RoleId] [smallint] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO






CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [smallint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [smallint] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO

ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO

ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO


INSERT INTO Role (RoleId, RoleName)
VALUES (1, 'ClientAdmin')
GO
INSERT INTO Role (RoleId, RoleName)
VALUES (2, 'SuperUser')
GO






