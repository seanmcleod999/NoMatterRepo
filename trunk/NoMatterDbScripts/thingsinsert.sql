-- ThingAlertType
insert into [ThingAlertType] ([ThingAlertTypeId], [Type]) values (1, 'Equals')
insert into [ThingAlertType] ([ThingAlertTypeId], [Type]) values (2, 'Greater Than')
insert into [ThingAlertType] ([ThingAlertTypeId], [Type]) values (3, 'Less Than')
GO

-- ThingAlertFrequency
insert into [ThingAlertFrequency] ([ThingAlertFrequencyId], [Description]) values (5, 'Half Hourly')
insert into [ThingAlertFrequency] ([ThingAlertFrequencyId], [Description]) values (10, 'Hourly')
insert into [ThingAlertFrequency] ([ThingAlertFrequencyId], [Description]) values (20, 'Daily')
insert into [ThingAlertFrequency] ([ThingAlertFrequencyId], [Description]) values (30, 'Weekly')
insert into [ThingAlertFrequency] ([ThingAlertFrequencyId], [Description]) values (40, 'Monthly')
GO

