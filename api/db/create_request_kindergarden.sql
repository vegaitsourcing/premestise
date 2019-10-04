USE [premestise]
GO

/****** Object:  Table [dbo].[Request_Kindergarden]    Script Date: 10/4/2019 9:15:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Request_Kindergarden](
	[Request_Id] [int] NOT NULL,
	[Kindergarden_Id] [int] NOT NULL,
	[Priority_Level] [int] NOT NULL,
 CONSTRAINT [PK_Request_Kindergarden] PRIMARY KEY CLUSTERED 
(
	[Request_Id] ASC,
	[Kindergarden_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Request_Kindergarden]  WITH CHECK ADD  CONSTRAINT [FK_Request_Kindergarden_Kindergarden] FOREIGN KEY([Kindergarden_Id])
REFERENCES [dbo].[Kindergarden] ([Id])
GO

ALTER TABLE [dbo].[Request_Kindergarden] CHECK CONSTRAINT [FK_Request_Kindergarden_Kindergarden]
GO

ALTER TABLE [dbo].[Request_Kindergarden]  WITH CHECK ADD  CONSTRAINT [FK_Request_Kindergarden_Request] FOREIGN KEY([Request_Id])
REFERENCES [dbo].[Request] ([Id])
GO

ALTER TABLE [dbo].[Request_Kindergarden] CHECK CONSTRAINT [FK_Request_Kindergarden_Request]
GO


