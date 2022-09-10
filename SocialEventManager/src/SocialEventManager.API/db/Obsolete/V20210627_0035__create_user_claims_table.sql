/* This is an example of a partial Identity implementation with Dapper.
   It was just for learning purposes.
   It is much more recommended to use the Identity packages with EF and not reinventing the wheel.
*/

/*
CREATE TABLE [dbo].[UserClaims]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](MAX) NOT NULL,
	CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [FK_UserClaims_UserId_Accounts] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Accounts] ([UserId]) ON DELETE CASCADE,
	CONSTRAINT [UQ_UserClaims_UserId_Type] UNIQUE NONCLUSTERED 
	(
		[UserId] ASC,
		[Type] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
*/