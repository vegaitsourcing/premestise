USE [premestise]
GO

/****** Object:  Table [dbo].[RequestMatches]    Script Date: 10/4/2019 10:29:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RequestMatches](
	[Request_One_Id] [int] NOT NULL,
	[Request_Two_Id] [int] NOT NULL,
	[Status] [nvarchar](50) NULL,
	[Date_Matched] [datetime2](7) NULL,
 CONSTRAINT [PK_RequestMatches] PRIMARY KEY CLUSTERED 
(
	[Request_One_Id] ASC,
	[Request_Two_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[RequestMatches]  WITH CHECK ADD  CONSTRAINT [FK_RequestMatches_Request] FOREIGN KEY([Request_One_Id])
REFERENCES [dbo].[Request] ([Id])
GO

ALTER TABLE [dbo].[RequestMatches] CHECK CONSTRAINT [FK_RequestMatches_Request]
GO

ALTER TABLE [dbo].[RequestMatches]  WITH CHECK ADD  CONSTRAINT [FK_RequestMatches_Request1] FOREIGN KEY([Request_Two_Id])
REFERENCES [dbo].[Request] ([Id])
GO

ALTER TABLE [dbo].[RequestMatches] CHECK CONSTRAINT [FK_RequestMatches_Request1]
GO


