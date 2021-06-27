INSERT INTO [dbo].[Roles] (Id, ConcurrencyStamp, [Name], NormalizedName)
VALUES (NEWID(),LOWER(NEWID()),'Admin' ,'ADMIN'), (NEWID(),LOWER(NEWID()),'User' ,'USER');