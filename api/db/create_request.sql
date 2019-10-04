USE [premestise]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 10/4/2019 9:15:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Parent_Name] [nvarchar](50) NULL,
	[Child_Name] [nvarchar](50) NULL,
	[Phone_Number] [nvarchar](50) NULL,
	[Email] [nvarchar](128) NULL,
	[Age_Group] [int] NULL,
	[Date_Created] [datetime2](7) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


