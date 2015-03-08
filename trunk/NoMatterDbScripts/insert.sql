-- DiscountType
insert into [DiscountType] ([DiscountTypeId], [Description]) values (1, 'Free Shipping')
insert into [DiscountType] ([DiscountTypeId], [Description]) values (2, 'Fixed Amount')
insert into [DiscountType] ([DiscountTypeId], [Description]) values (3, 'Percentage')
GO

-- Role
insert into [Role] ([RoleId], [RoleName]) values (1, 'ClientAdmin')
insert into [Role] ([RoleId], [RoleName]) values (2, 'SuperUser')
GO

-- PaymentType
SET IDENTITY_INSERT PaymentType ON
GO
insert into [PaymentType] ([PaymentTypeId], [Name], [Details], [Picture]) values (1, 'EFT', 'Manual electronic funds transfer', null)
insert into [PaymentType] ([PaymentTypeId], [Name], [Details], [Picture]) values (2, 'Payfast', 'The easier way for online payments', null)
GO
SET IDENTITY_INSERT PaymentType OFF
GO

-- GlobalSetting
SET IDENTITY_INSERT GlobalSetting ON
GO
insert into [GlobalSetting] ([GlobalSettingId], [SettingName], [SettingType], [StringValue], [IntValue]) values (1, 'Test', 1, 'Test', null)
insert into [GlobalSetting] ([GlobalSettingId], [SettingName], [SettingType], [StringValue], [IntValue]) values (2, 'Test2', 1, 'Test2', null)
insert into [GlobalSetting] ([GlobalSettingId], [SettingName], [SettingType], [StringValue], [IntValue]) values (3, 'Test2', 1, 'Test3', null)
GO
SET IDENTITY_INSERT GlobalSetting OFF
GO

