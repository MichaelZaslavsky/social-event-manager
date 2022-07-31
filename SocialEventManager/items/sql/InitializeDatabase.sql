USE master;
GO
DROP DATABASE IF EXISTS SocialEventManager;
GO
CREATE DATABASE SocialEventManager;
GO
DROP DATABASE IF EXISTS SocialEventManagerHangfire;
GO
CREATE DATABASE SocialEventManagerHangfire;
GO
DROP DATABASE IF EXISTS SocialEventManagerTest;
GO
CREATE DATABASE SocialEventManagerTest;
GO

----------------------------------------------------------------------------
--- DB USER CREATION
----------------------------------------------------------------------------
USE master;
GO
CREATE LOGIN [$(DB_USER)] WITH PASSWORD='$(DB_PASSWORD)', CHECK_EXPIRATION=OFF, CHECK_POLICY=ON;
GO
USE SocialEventManager;
GO
CREATE USER [$(DB_USER)] FOR LOGIN [$(DB_USER)];
GO
EXEC sp_addrolemember N'db_owner', [$(DB_USER)];
GO
EXEC master..sp_addsrvrolemember @loginame = N'$(DB_USER)', @rolename = N'dbcreator';
GO
USE SocialEventManagerHangfire;
GO
CREATE USER [$(DB_USER)] FOR LOGIN [$(DB_USER)];
GO
EXEC sp_addrolemember N'db_owner', [$(DB_USER)];
GO
EXEC master..sp_addsrvrolemember @loginame = N'$(DB_USER)', @rolename = N'dbcreator';
GO