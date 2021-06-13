CREATE TABLE [dbo].[Accounts]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](MAX) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[EmailConfirmed] [BIT] NOT NULL,
	[PhoneNumber] [nvarchar](MAX) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[LockoutEnd] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[NormalizedEmail] [nvarchar](255) NOT NULL,
	[NormalizedUserName] [nvarchar](255) NOT NULL,
	[ConcurrencyStamp] [nvarchar](255) NOT NULL,
	[SecurityStamp] [nvarchar](MAX) NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [UQ_Accounts_Email] UNIQUE NONCLUSTERED 
	(
		[Email] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [NCI_Accounts_UserId] UNIQUE NONCLUSTERED
	(
		[UserId] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];