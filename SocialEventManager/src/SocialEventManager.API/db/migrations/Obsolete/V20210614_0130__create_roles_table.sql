/* This is an example of a partial Identity implementation with Dapper.
   It was just for learning purposes.
   It is much more recommended to use the Identity packages with EF and not reinventing the wheel.
*/

/*
CREATE TABLE [dbo].[Roles]
(
	[Id] [uniqueidentifier] NOT NULL,
	[ConcurrencyStamp] [nvarchar](255) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[NormalizedName] [nvarchar](255) NOT NULL,
	CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
	CONSTRAINT [UQ_Roles_Name] UNIQUE 
	(
		[Name] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];
*/