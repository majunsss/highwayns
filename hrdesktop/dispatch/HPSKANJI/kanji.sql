USE [HPS]
GO
/****** Object:  Table [dbo].[tbl_Lesson]    Script Date: 04/27/2014 07:37:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Lesson](
	[LessonId] [int] IDENTITY(1,1) NOT NULL,
	[LessonName] [nvarchar](50) NULL,
	[ClassId] [int] NULL,
	[UserID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_Lesson] PRIMARY KEY CLUSTERED 
(
	[LessonId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Content]    Script Date: 04/27/2014 07:37:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Content](
	[ContentId] [int] IDENTITY(1,1) NOT NULL,
	[LessonId] [int] NULL,
	[ContentType] [nvarchar](1) NULL,
	[Contents] [nvarchar](max) NULL,
	[AudioFile] [nvarchar](250) NULL,
	[block1] [nvarchar](50) NULL,
	[block2_1] [nvarchar](50) NULL,
	[block2_2] [nvarchar](50) NULL,
	[block2_3] [nvarchar](50) NULL,
	[block2_4] [nvarchar](50) NULL,
	[block3_1] [nvarchar](50) NULL,
	[block3_2] [nvarchar](50) NULL,
	[block3_3] [nvarchar](50) NULL,
	[block3_4] [nvarchar](50) NULL,
	[block4_1] [nvarchar](50) NULL,
	[block4_2] [nvarchar](50) NULL,
	[block4_3] [nvarchar](50) NULL,
	[block4_4] [nvarchar](50) NULL,
	[block5_1] [nvarchar](50) NULL,
	[block5_2] [nvarchar](50) NULL,
	[block5_3] [nvarchar](50) NULL,
	[block5_4] [nvarchar](50) NULL,
	[block6_1] [nvarchar](50) NULL,
	[block6_2] [nvarchar](50) NULL,
	[block6_3] [nvarchar](50) NULL,
	[block6_4] [nvarchar](50) NULL,
	[UserID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_Content] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_Class]    Script Date: 04/27/2014 07:37:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Class](
	[ClassId] [int] NOT NULL,
	[ClassName] [nvarchar](50) NULL,
	[UserID] [varchar](20) NULL,
 CONSTRAINT [PK_tbl_Class] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
