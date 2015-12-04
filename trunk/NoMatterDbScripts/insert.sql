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

-- SettingType
insert into [SettingType] ([SettingTypeId], [Type]) values (1, 'String')
insert into [SettingType] ([SettingTypeId], [Type]) values (2, 'Int')
GO

-- SettingCategory
insert into [SettingCategory] ([SettingCategoryId], [Category]) values (1, 'Banking')
insert into [SettingCategory] ([SettingCategoryId], [Category]) values (2, 'GoogleMap')
insert into [SettingCategory] ([SettingCategoryId], [Category]) values (3, 'SEO')
insert into [SettingCategory] ([SettingCategoryId], [Category]) values (4, 'General')
insert into [SettingCategory] ([SettingCategoryId], [Category]) values (5, 'Facebook')
GO

-- Setting
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (1, 'BankName', '<p>The Bank name for EFT transactions</p>', 1, 1, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (2, 'AccountName', 'The Account name for EFT transactions', 1, 1, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (3, 'BranchName', 'The Branch Name for EFT transactions', 1, 1, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (4, 'BranchNumber', 'The Branch Number for EFT transactions', 1, 1, '^[0-9]*$')
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (5, 'AccountNumber', 'The Account number for EFT transactions', 1, 1, '^[0-9]*$')
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (6, 'MapLongitude', 'The google map Longitude coordinates of the site physical office. e.g. 28.991066', 1, 2, '^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}')
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (7, 'MapLatitude', 'The google map Latitude coordinates of the site physical office. e.g. -27.991066', 1, 2, '^-?([1-8]?[1-9]|[1-9]0)\.{1}\d{1,6}')
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (11, 'SEOKeywords', 'The keywords used for SEO. Comma delimited.', 1, 3, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (12, 'SiteDescription', 'Your site description used for SEO. Usually your site name followed by a short description. E.g. MyCoolSite.co.za - Your favourite online store for cool goodies ', 1, 3, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (13, 'SiteMetaDescription', 'Meta descriptions are commonly used on search engine result pages (SERPs) to display preview snippets for a given page. Should be unique for each page. E.g. online store selling cool goodies ', 1, 3, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (14, 'SiteHomePageTitle', 'The title of your homepage. E.g. MyCoolSite Online Store', 1, 3, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (20, 'EmailAddressSales', null, 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (21, 'EmailAddressInfo', null, 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (30, 'FacebookPageUrl', '', 1, 5, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (31, 'TwitterUrl', '', 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (32, 'InstagramUrl', '', 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (33, 'PintrestUrl', '', 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (34, 'GooglePlusUrl', '', 1, 4, null)
insert into [Setting] ([SettingId], [SettingName], [SettingDescription], [SettingTypeId], [SettingCategoryId], [RegexValidation]) values (35, 'FacebookPostTemplate', null, 1, 5, null)
GO

