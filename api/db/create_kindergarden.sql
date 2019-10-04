USE [premestise]
GO

/****** Object:  Table [dbo].[Kindergarden]    Script Date: 10/4/2019 9:13:39 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Kindergarden](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Government] [nvarchar](50) NULL,
	[Municipality] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Department] [nvarchar](50) NULL,
	[Street] [nvarchar](50) NULL,
	[Street_Number] [nvarchar](50) NULL,
	[Postal_Code] [int] NULL,
	[Location_Type] [bit] NULL,
	[Longitude] [decimal](9, 7) NULL,
	[Latitude] [decimal](9, 7) NULL,
 CONSTRAINT [PK_Kindergarden] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


