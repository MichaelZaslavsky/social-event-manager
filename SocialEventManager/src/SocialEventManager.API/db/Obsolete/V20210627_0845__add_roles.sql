/* This is an example of a partial Identity implementation with Dapper.
   It was just for learning purposes.
   It is much more recommended to use the Identity packages with EF and not reinventing the wheel.
*/

/*
INSERT INTO [dbo].[Roles] (Id, ConcurrencyStamp, [Name], NormalizedName)
VALUES (NEWID(),LOWER(NEWID()),'Admin' ,'ADMIN'), (NEWID(),LOWER(NEWID()),'User' ,'USER');
*/