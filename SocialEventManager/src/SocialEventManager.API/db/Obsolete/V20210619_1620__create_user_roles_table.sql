/* This is an example of a partial Identity implementation with Dapper.
   It was just for learning purposes.
   It is much more recommended to use the Identity packages with EF and not reinventing the wheel.
*/

/*
CREATE TABLE [dbo].[UserRoles]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_UserRoles_UserId_Accounts] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Accounts] ([UserId]) ON DELETE CASCADE,
	CONSTRAINT [FK_UserRoles_RoleId_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [UQ_UserRoles_UserId_RoleId] UNIQUE 
	(
		[UserId] ASC,
		[RoleId] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
*/