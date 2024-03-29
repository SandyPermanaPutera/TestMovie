USE [DbMovies]
GO
/****** Object:  Table [dbo].[mstMovie]    Script Date: 3/15/2024 8:50:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mstMovie](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [varchar](150) NULL,
	[description] [varchar](max) NULL,
	[rating] [float] NULL,
	[image] [varchar](255) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
 CONSTRAINT [PK_mstMovie] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[DeleteDataMovies]    Script Date: 3/15/2024 8:50:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteDataMovies]
@id INT
 as

DELETE a FROM dbo.mstMovie a WHERE a.id=@id

GO
/****** Object:  StoredProcedure [dbo].[InsertDataMovies]    Script Date: 3/15/2024 8:50:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertDataMovies]
@title VARCHAR(50),
 @DESCRIPTION VARCHAR(50),
 @rating varchar(50),
 @image varchar(50)
 as

INSERT INTO [dbo].[mstMovie]
(
    [title],
    [description],
    [rating],
    [image],
    [created_at],
    [updated_at]
)

SELECT @title,@DESCRIPTION,@rating,@image,GETDATE(),NULL

GO
/****** Object:  StoredProcedure [dbo].[UpdateDataMovies]    Script Date: 3/15/2024 8:50:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateDataMovies]
@id INT,
@title VARCHAR(50),
 @DESCRIPTION VARCHAR(50),
 @rating varchar(50),
 @image varchar(50),
 @updated_at DATETIME
 as

UPDATE a SET 
a.title=@title,
a.description=@DESCRIPTION,
a.rating=@rating,
a.image=@image,
a.updated_at=@updated_at
 FROM dbo.mstMovie a
WHERE id=@id

GO
