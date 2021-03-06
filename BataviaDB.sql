/****** Object:  ForeignKey [FK_Crew_Whitelist_Crew]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Crew_Whitelist_Crew]') AND parent_object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist]'))
ALTER TABLE [dbo].[Crew_Whitelist] DROP CONSTRAINT [FK_Crew_Whitelist_Crew]
GO
/****** Object:  StoredProcedure [dbo].[Crew_Whitelist_CRUD]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist_CRUD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Crew_Whitelist_CRUD]
GO
/****** Object:  StoredProcedure [dbo].[Crew_CRUD]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_CRUD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Crew_CRUD]
GO
/****** Object:  Table [dbo].[Crew_Whitelist]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist]') AND type in (N'U'))
DROP TABLE [dbo].[Crew_Whitelist]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
DROP TABLE [dbo].[Admin]
GO
/****** Object:  Table [dbo].[Crew]    Script Date: 03/31/2016 09:39:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew]') AND type in (N'U'))
DROP TABLE [dbo].[Crew]
GO
/****** Object:  Table [dbo].[Crew]    Script Date: 03/31/2016 09:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Crew](
	[Barcode] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Nama] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Tanggal_Daftar] [smalldatetime] NOT NULL,
	[Status] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Airport] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Company_Airways] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Crew] PRIMARY KEY CLUSTERED 
(
	[Barcode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Crew] ([Barcode], [Nama], [Tanggal_Daftar], [Status], [Airport], [Company_Airways]) VALUES (N'27032016-001', N'Yessie', CAST(0xA5D60348 AS SmallDateTime), N'Pramugari', N'Cengkareng', N'Batavia')
INSERT [dbo].[Crew] ([Barcode], [Nama], [Tanggal_Daftar], [Status], [Airport], [Company_Airways]) VALUES (N'27032016-002', N'John', CAST(0xA5D6036A AS SmallDateTime), N'Pilot', N'Cengkareng', N'Batavia')
INSERT [dbo].[Crew] ([Barcode], [Nama], [Tanggal_Daftar], [Status], [Airport], [Company_Airways]) VALUES (N'27032016-003', N'Yessie', CAST(0xA5D60453 AS SmallDateTime), N'Pramugari', N'Cengkareng', N'Air Asia')
/****** Object:  Table [dbo].[Admin]    Script Date: 03/31/2016 09:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Admin]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Password] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Whitelist] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Admin] ON
INSERT [dbo].[Admin] ([Id], [Username], [Password], [Whitelist]) VALUES (1, N'Andi', N'admin', N'0')
INSERT [dbo].[Admin] ([Id], [Username], [Password], [Whitelist]) VALUES (2, N'Walter', N'white', N'1')
SET IDENTITY_INSERT [dbo].[Admin] OFF
/****** Object:  Table [dbo].[Crew_Whitelist]    Script Date: 03/31/2016 09:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Crew_Whitelist](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Barcode] [nvarchar](50) COLLATE Latin1_General_CI_AS NOT NULL,
	[Tanggal_Awal] [smalldatetime] NOT NULL,
	[Tanggal_Akhir] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Crew_Whitelist] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Crew_Whitelist] ON
INSERT [dbo].[Crew_Whitelist] ([Id], [Barcode], [Tanggal_Awal], [Tanggal_Akhir]) VALUES (1, N'27032016-002', CAST(0xA5CB0000 AS SmallDateTime), CAST(0xA5D70000 AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Crew_Whitelist] OFF
/****** Object:  StoredProcedure [dbo].[Crew_CRUD]    Script Date: 03/31/2016 09:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_CRUD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Crew_CRUD]
	@Action VARCHAR(10)
      ,@Barcode VARCHAR(100) = NULL
      ,@Nama VARCHAR(100) = NULL
      ,@Tanggal_Daftar smalldatetime = NULL
	  ,@Status VARCHAR(100) = NULL
      ,@Airport VARCHAR(100) = NULL
      ,@Company_Airways VARCHAR(100) = NULL
AS
BEGIN
      SET NOCOUNT ON;
 
      --SELECT
    IF @Action = ''SELECT''
      BEGIN
            SELECT Barcode, Nama, Tanggal_Daftar, Status, Airport, Company_Airways
            FROM Crew
      END
 
      --INSERT
    IF @Action = ''INSERT''
      BEGIN
            INSERT INTO Crew(Barcode, Nama, Tanggal_Daftar, Status, Airport, Company_Airways)
            VALUES (@Barcode, @Nama, @Tanggal_Daftar, @Status, @Airport, @Company_Airways)
      END
 
      --UPDATE
    IF @Action = ''UPDATE''
      BEGIN
            UPDATE Crew
            SET Nama = @Nama, Status = @Status, Airport = @Airport, Company_Airways = @Company_Airways
            WHERE Barcode = @Barcode
      END
 
      --DELETE
    IF @Action = ''DELETE''
      BEGIN
            DELETE FROM Crew
            WHERE Barcode = @Barcode
      END
END
	RETURN
' 
END
GO
/****** Object:  StoredProcedure [dbo].[Crew_Whitelist_CRUD]    Script Date: 03/31/2016 09:39:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist_CRUD]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Crew_Whitelist_CRUD]
	@Action VARCHAR(10)
	  ,@Id int = NULL
      ,@Barcode VARCHAR(100) = NULL
      ,@Tanggal_Awal smalldatetime = NULL
	  ,@Tanggal_Akhir smalldatetime = NULL
AS
BEGIN
      SET NOCOUNT ON;
 
      --SELECT
    IF @Action = ''SELECT''
      BEGIN
            SELECT w.Id, w.Barcode, c.Nama, w.Tanggal_Awal, w.Tanggal_Akhir
            FROM Crew_Whitelist w
			JOIN CREW c
			ON (w.Barcode = c.Barcode)
      END
 
      --INSERT
    IF @Action = ''INSERT''
      BEGIN
            INSERT INTO Crew_Whitelist(Barcode, Tanggal_Awal, Tanggal_Akhir)
            VALUES (@Barcode, @Tanggal_Awal, @Tanggal_Akhir)
      END
 
      --UPDATE
    IF @Action = ''UPDATE''
      BEGIN
            UPDATE Crew_Whitelist
            SET Barcode = @Barcode, Tanggal_Awal = @Tanggal_Awal, Tanggal_Akhir = @Tanggal_Akhir
            WHERE Id = @Id 
      END
 
      --DELETE
    IF @Action = ''DELETE''
      BEGIN
            DELETE FROM Crew_Whitelist
            WHERE Id = @Id
      END
END
	RETURN' 
END
GO
/****** Object:  ForeignKey [FK_Crew_Whitelist_Crew]    Script Date: 03/31/2016 09:39:59 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Crew_Whitelist_Crew]') AND parent_object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist]'))
ALTER TABLE [dbo].[Crew_Whitelist]  WITH CHECK ADD  CONSTRAINT [FK_Crew_Whitelist_Crew] FOREIGN KEY([Barcode])
REFERENCES [dbo].[Crew] ([Barcode])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Crew_Whitelist_Crew]') AND parent_object_id = OBJECT_ID(N'[dbo].[Crew_Whitelist]'))
ALTER TABLE [dbo].[Crew_Whitelist] CHECK CONSTRAINT [FK_Crew_Whitelist_Crew]
GO
