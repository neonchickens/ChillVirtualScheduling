USE [Advising]
GO
/****** Object:  Table [dbo].[Confirm]    Script Date: 1/28/2019 10:08:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Confirm](
	[studentId] [int] NOT NULL,
	[confirmCode] [varchar](4) NOT NULL,
 CONSTRAINT [PK_Confirm] PRIMARY KEY CLUSTERED 
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Confirm]  WITH CHECK ADD  CONSTRAINT [FK_Confirm_Person] FOREIGN KEY([studentId])
REFERENCES [dbo].[Person] ([id])
GO
ALTER TABLE [dbo].[Confirm] CHECK CONSTRAINT [FK_Confirm_Person]
GO
