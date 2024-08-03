# Logique-Technical-Test-

For installation :
# 1 Create db
# 2 execute this scipt :
CREATE TABLE [dbo].[User](
	[UserId] [varchar](10) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[TNC] [bit] NOT NULL,
	[Password] [varchar](50) NULL,
	[RegisterDate] [datetime] NULL,
	[MemberId] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

# 3. Setting connection string at config.json on WebApp
