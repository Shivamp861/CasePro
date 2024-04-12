USE [master]
GO
/****** Object:  Database [caseproDB]    Script Date: 12-04-2024 09:32:14 ******/
CREATE DATABASE [caseproDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'caseproDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\caseproDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'caseproDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\caseproDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [caseproDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [caseproDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [caseproDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [caseproDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [caseproDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [caseproDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [caseproDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [caseproDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [caseproDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [caseproDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [caseproDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [caseproDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [caseproDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [caseproDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [caseproDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [caseproDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [caseproDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [caseproDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [caseproDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [caseproDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [caseproDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [caseproDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [caseproDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [caseproDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [caseproDB] SET RECOVERY FULL 
GO
ALTER DATABASE [caseproDB] SET  MULTI_USER 
GO
ALTER DATABASE [caseproDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [caseproDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [caseproDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [caseproDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [caseproDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [caseproDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'caseproDB', N'ON'
GO
ALTER DATABASE [caseproDB] SET QUERY_STORE = OFF
GO
USE [caseproDB]
GO
/****** Object:  Table [dbo].[Activity_Customer_Table]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Customer_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ContactNo] [nvarchar](50) NULL,
	[Address_Line1] [nvarchar](max) NULL,
	[Address_Line2] [nvarchar](max) NULL,
	[City] [nvarchar](50) NULL,
	[Postcode] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[activityId] [int] NULL,
 CONSTRAINT [PK_Activity_Customer_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_SignOffdetail]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_SignOffdetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompetionDate] [date] NULL,
	[Signature] [nvarchar](50) NULL,
	[PrintName] [nvarchar](50) NULL,
	[activityId] [int] NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Activity_SignOffdetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Status_Table]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Status_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[activityId] [int] NULL,
 CONSTRAINT [PK_Activity_Status_Code] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Table]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerOrder_Number] [nvarchar](50) NULL,
	[SageOrder_Number] [nvarchar](50) NULL,
	[HCorder_Number] [nvarchar](50) NULL,
	[Date_Raised] [nvarchar](50) NULL,
	[Raised_By] [nvarchar](50) NULL,
	[Outorhours_Emrg_Contact] [nvarchar](50) NULL,
	[Nearest_A_E] [nvarchar](50) NULL,
	[Activit_Type] [nvarchar](50) NULL,
	[customerId] [int] NULL,
	[signOffId] [int] NULL,
	[IsActive] [bit] NULL,
	[userID] [int] NULL,
	[SiteAddress] [nvarchar](max) NULL,
 CONSTRAINT [PK_Activity_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Activity_Trailer_Table]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activity_Trailer_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TrailerSupplier] [nvarchar](50) NULL,
	[TrailerNumber] [nvarchar](50) NULL,
	[Quantity] [nvarchar](50) NULL,
	[LoadDepot] [nvarchar](50) NULL,
	[DepartFrom] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[LoadedTippedBy] [nvarchar](50) NULL,
	[IsOutBound] [bit] NULL,
	[ActivityId] [int] NULL,
	[VehicleReg] [nvarchar](50) NULL,
	[Loadpositioned] [nvarchar](50) NULL,
 CONSTRAINT [PK_Activity_Trailer_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityDetail]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityDetail](
	[ActivityId] [int] NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityDate] [datetime] NULL,
	[MeetingSite] [nvarchar](50) NULL,
	[LabourSupplier] [nvarchar](50) NULL,
	[SupplierContact] [nvarchar](50) NULL,
	[No_of_Persone_Supplied] [nvarchar](50) NULL,
	[BarrierType] [nvarchar](50) NULL,
	[Barrier_Qty] [nvarchar](50) NULL,
	[BarrierStartAndFinishLocation] [nvarchar](50) NULL,
	[Barrier_Performance] [nvarchar](50) NULL,
	[LengthOfRuns] [nvarchar](50) NULL,
	[AnchoringDetails] [nvarchar](50) NULL,
	[Isapermittobreakgroundrequired] [nvarchar](50) NULL,
	[ChainLiftingequipmenttobeused] [nvarchar](50) NULL,
	[LiftingEquipmentUsed] [nvarchar](50) NULL,
	[IncidentReporting] [nvarchar](50) NULL,
	[OtherResourcesEquipmentUsed] [nvarchar](50) NULL,
	[AnySpecialInstructions] [nvarchar](50) NULL,
	[AllRelevantActivityRAMS] [nvarchar](50) NULL,
	[AnyNearMissOccurrences] [nvarchar](50) NULL,
	[BarrierConditionChecks] [nvarchar](50) NULL,
	[Startandfinishtime] [nvarchar](50) NULL,
 CONSTRAINT [PK_ActivityDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityImages]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityImages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ActivityId] [int] NULL,
	[ImageName] [nvarchar](50) NULL,
	[UploadPath] [nvarchar](max) NULL,
	[UploadedDate] [datetime] NULL,
 CONSTRAINT [PK_ActivityImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityNotes]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityNotes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Notes] [varchar](max) NULL,
	[activityId] [int] NULL,
 CONSTRAINT [PK_ActivityNotes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityProduct_Details]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityProduct_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Shift] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[SummaryOfWorks] [nvarchar](50) NULL,
	[ActivityId] [int] NULL,
 CONSTRAINT [PK_ActivityProduct_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityResources_Details]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityResources_Details](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Shift] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Name] [nvarchar](50) NULL,
	[ResourceType] [nvarchar](max) NULL,
	[ActivityId] [int] NULL,
	[Comments] [nvarchar](max) NULL,
	[Day_night] [nvarchar](50) NULL,
 CONSTRAINT [PK_ActivityResources_Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstructorFormDetails]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstructorFormDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SelectedActivity] [nvarchar](50) NULL,
	[Name] [nvarchar](50) NULL,
	[Date] [date] NULL,
	[Note] [varchar](max) NULL,
	[HasSent] [bit] NULL,
	[HasSubmitted] [bit] NULL,
	[ActivityId] [int] NOT NULL,
 CONSTRAINT [PK_InstructorFormDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InstructorNames]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InstructorNames](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_InstructorNames] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users_table]    Script Date: 12-04-2024 09:32:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users_table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[ClientName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Isactive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[LastLogindate] [datetime] NULL,
 CONSTRAINT [PK_Users_table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Activity_Customer_Table] ON 

INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (77, N'Ritu126', N'7485457854', NULL, NULL, NULL, NULL, NULL, 149)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (78, N'Notesval123', N'7541236500', NULL, NULL, NULL, NULL, NULL, 149)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (79, N'tested', N'1234567897', NULL, NULL, NULL, NULL, NULL, 179)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (80, N'Trial', N'7485965874', NULL, NULL, NULL, NULL, NULL, 179)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (81, N'testded', N'fdfd', NULL, NULL, NULL, NULL, NULL, 179)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (82, N'tested', N'1234567897', NULL, NULL, NULL, NULL, NULL, 183)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (83, N'Trial', N'7485965874', NULL, NULL, NULL, NULL, NULL, 183)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (84, N'tested', N'1234567897', NULL, NULL, NULL, NULL, NULL, 184)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (85, N'Tested', N'7485965874', NULL, NULL, NULL, NULL, NULL, 184)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (86, N'Tested', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (87, N'Trial', N'7485965874', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (88, N'Tested123`', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (89, N'Tested123`', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (90, N'Tested123`', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (91, N'Tested123`', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (92, N'Tested123`', N'1234567897', NULL, NULL, NULL, NULL, NULL, 188)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (93, N'Tested', N'1234567897', NULL, NULL, NULL, NULL, NULL, 189)
INSERT [dbo].[Activity_Customer_Table] ([Id], [Name], [ContactNo], [Address_Line1], [Address_Line2], [City], [Postcode], [Country], [activityId]) VALUES (94, N'Tested123456158gg', N'1234567897', NULL, NULL, NULL, NULL, NULL, 189)
SET IDENTITY_INSERT [dbo].[Activity_Customer_Table] OFF
GO
SET IDENTITY_INSERT [dbo].[Activity_Table] ON 

INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (149, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (150, N'GGG0012356565', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (151, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (152, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (153, N'GGG0011212', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'test', N'test', N'test', N'Site Uplift', NULL, NULL, NULL, NULL, N'Goods')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (154, N'GGG0011212', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'test', N'test', N'test', N'Site Uplift', NULL, NULL, NULL, NULL, N'Goods')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (155, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (156, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Uplift', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (157, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Yard Loading', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (158, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Yard Loading', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (159, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (160, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (161, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (162, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (163, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (164, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Uplift', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (165, N'dasdasdsa', N'dasdasdsa', N'dasdasdsa', N'12-04-2024 00:00:00', N'dasdasdsa', N'dasdasdsa', N'dasdasdsa', N'Site Installation', NULL, NULL, NULL, NULL, N'dasdasdsa')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (166, N'test', N'test', N'test', N'19-04-2024 00:00:00', N'test', N'test', N'test', N'Yard Tipping', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (167, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Uplift', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (168, N'GGG001', N'SG001', N'HC001', N'10-04-2024 00:00:00', N'Tested', N'7485962145', N'Goods', N'Site Installation', NULL, NULL, NULL, NULL, N'test')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (169, N'GGo', N'DHGH001', N'HC1212', N'30-11-2023 00:00:00', N'sds', N'fgfg', N'gfg', N'Site Uplift', NULL, NULL, NULL, NULL, N'gfg')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (170, N'ds', N'dsd', N'dsd', N'11-04-2024 00:00:00', N'dsd', N'1234567897', N'dsd', N'Site Uplift', NULL, NULL, NULL, NULL, N'ds')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (171, N'gg', N'df', N'dfd', N'11-04-2024 00:00:00', N'fdf', N'1234567897', N'dsdf', N'Site Uplift', NULL, NULL, NULL, NULL, N'fdf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (172, N'gg', N'gg', N'gg', N'11-04-2024 00:00:00', N'gg', N'4578963214', N'fgfg', N'Site Installation', NULL, NULL, NULL, NULL, N'gg')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (173, N'dds', N'dsds', N'sdsd', N'11-04-2024 00:00:00', N'dsd', N'1234567891', N'sds', N'Site Uplift', NULL, NULL, NULL, NULL, N'ds')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (174, N'GG001', N'SGG001', N'HC001', N'11-04-2024 00:00:00', N'sdsd', N'1234567897', N'Tested', N'Site Installation', NULL, NULL, NULL, NULL, N'sdsd')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (175, N'GG001', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tes', N'7485963214', N'sds', N'Site Installation', NULL, NULL, NULL, NULL, N'sds')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (176, N'GG012', N'dfd12fddfd', N'fdf121', N'13-04-2024 00:00:00', N'sd', N'1234567897', N'tesrted', N'Yard Tipping', NULL, NULL, NULL, NULL, N'sd')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (177, N'vcfg', N'fgf', N'fgf', N'11-04-2024 00:00:00', N'fgf', N'1234567894', N'fgf', N'Site Uplift', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (178, N'GG01', N'SSG001', N'HC001', N'11-04-2024 00:00:00', N'fgf', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (179, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (180, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Loading', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (181, N'dsd', N'ds', N'dsd', N'11-04-2024 00:00:00', N'sds', N'1234567894', N'fgf', N'Yard Loading', NULL, NULL, NULL, NULL, N'sds')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (182, N'ds', N'sds', N'sds', N'11-04-2024 00:00:00', N'ds', N'1234567894', N'fgf', N'Site Uplift', NULL, NULL, NULL, NULL, N'sds')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (183, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (184, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (185, N'GG01', N'SG001', N'HC001', N'31-12-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Site Installation', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (186, N'GG01', N'SG001', N'HC001', N'31-12-2024 00:00:00', N'fgf', N'1234567894', N'fgf', N'Site Installation', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (187, N'GG01', N'SG001', N'HC001', N'31-12-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Site Installation', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (188, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
INSERT [dbo].[Activity_Table] ([Id], [CustomerOrder_Number], [SageOrder_Number], [HCorder_Number], [Date_Raised], [Raised_By], [Outorhours_Emrg_Contact], [Nearest_A_E], [Activit_Type], [customerId], [signOffId], [IsActive], [userID], [SiteAddress]) VALUES (189, N'GG01', N'SG001', N'HC001', N'11-04-2024 00:00:00', N'tested', N'1234567894', N'fgf', N'Yard Tipping', NULL, NULL, NULL, NULL, N'fgf')
SET IDENTITY_INSERT [dbo].[Activity_Table] OFF
GO
SET IDENTITY_INSERT [dbo].[Activity_Trailer_Table] ON 

INSERT [dbo].[Activity_Trailer_Table] ([Id], [TrailerSupplier], [TrailerNumber], [Quantity], [LoadDepot], [DepartFrom], [Date], [LoadedTippedBy], [IsOutBound], [ActivityId], [VehicleReg], [Loadpositioned]) VALUES (34, N'test', N'Trailer 2', N'5', N'drop', NULL, CAST(N'2024-04-11' AS Date), N'by', NULL, 187, N'gj001', NULL)
INSERT [dbo].[Activity_Trailer_Table] ([Id], [TrailerSupplier], [TrailerNumber], [Quantity], [LoadDepot], [DepartFrom], [Date], [LoadedTippedBy], [IsOutBound], [ActivityId], [VehicleReg], [Loadpositioned]) VALUES (35, N'fh', N'Trailer 2', N'6', N'hghg', NULL, CAST(N'2024-04-11' AS Date), N'hgh', NULL, 187, N'hgh', NULL)
INSERT [dbo].[Activity_Trailer_Table] ([Id], [TrailerSupplier], [TrailerNumber], [Quantity], [LoadDepot], [DepartFrom], [Date], [LoadedTippedBy], [IsOutBound], [ActivityId], [VehicleReg], [Loadpositioned]) VALUES (36, N'fh', N'Trailer 2', N'6', N'hghg', NULL, CAST(N'2024-04-11' AS Date), N'hgh', NULL, 187, N'hgh', NULL)
INSERT [dbo].[Activity_Trailer_Table] ([Id], [TrailerSupplier], [TrailerNumber], [Quantity], [LoadDepot], [DepartFrom], [Date], [LoadedTippedBy], [IsOutBound], [ActivityId], [VehicleReg], [Loadpositioned]) VALUES (37, N'rt', N'Trailer 3', N'4', N'fgf', NULL, CAST(N'2024-04-11' AS Date), N'fgfg', NULL, 188, N'gfg', NULL)
INSERT [dbo].[Activity_Trailer_Table] ([Id], [TrailerSupplier], [TrailerNumber], [Quantity], [LoadDepot], [DepartFrom], [Date], [LoadedTippedBy], [IsOutBound], [ActivityId], [VehicleReg], [Loadpositioned]) VALUES (38, N'yty', N'Trailer 3', N'8', N'tty', NULL, CAST(N'2024-04-11' AS Date), N'ytyty', NULL, 188, N'tfg', NULL)
SET IDENTITY_INSERT [dbo].[Activity_Trailer_Table] OFF
GO
SET IDENTITY_INSERT [dbo].[ActivityDetail] ON 

INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (183, 39, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'sds', NULL, N'dsd', NULL, NULL, N'dsd', NULL, NULL, NULL)
INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (184, 41, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (184, 42, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (187, 43, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'chan', N'Lifting', N'incident', NULL, NULL, N'sds', N'sd', N'dsd', N'tese')
INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (187, 44, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'chan', N'Lifting', N'incident', NULL, NULL, N'sds', N'sd', N'dsd', N'tese')
INSERT [dbo].[ActivityDetail] ([ActivityId], [Id], [ActivityDate], [MeetingSite], [LabourSupplier], [SupplierContact], [No_of_Persone_Supplied], [BarrierType], [Barrier_Qty], [BarrierStartAndFinishLocation], [Barrier_Performance], [LengthOfRuns], [AnchoringDetails], [Isapermittobreakgroundrequired], [ChainLiftingequipmenttobeused], [LiftingEquipmentUsed], [IncidentReporting], [OtherResourcesEquipmentUsed], [AnySpecialInstructions], [AllRelevantActivityRAMS], [AnyNearMissOccurrences], [BarrierConditionChecks], [Startandfinishtime]) VALUES (188, 45, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, N'chan', N'dfdf', N'incident', NULL, NULL, N'gfg', N'fgf', N'gfg', N'tese')
SET IDENTITY_INSERT [dbo].[ActivityDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[ActivityImages] ON 

INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (15, 182, N'0555cc76-97bb-4cd4-ba63-2a395566e850.jpg', N'https://localhost:7007/Upload/0555cc76-97bb-4cd4-ba63-2a395566e850.jpg', CAST(N'2024-04-11T19:13:06.913' AS DateTime))
INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (16, 183, N'cc027e3b-ba07-435d-b5d3-bb549724ddc8.jpg', N'https://localhost:7007/Upload/cc027e3b-ba07-435d-b5d3-bb549724ddc8.jpg', CAST(N'2024-04-11T19:15:46.647' AS DateTime))
INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (17, 183, N'a5de8d96-e763-4cae-8e85-1c64622ffa32.jpg', N'https://localhost:7007/Upload/a5de8d96-e763-4cae-8e85-1c64622ffa32.jpg', CAST(N'2024-04-11T19:16:06.280' AS DateTime))
INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (18, 187, N'15a99399-8407-4a53-b7bd-70ee847f86df.jpg', N'https://localhost:7007/Upload/15a99399-8407-4a53-b7bd-70ee847f86df.jpg', CAST(N'2024-04-11T21:49:23.253' AS DateTime))
INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (19, 187, N'976310b7-d59d-46da-87c7-486554269b05.jpg', N'https://localhost:7007/Upload/976310b7-d59d-46da-87c7-486554269b05.jpg', CAST(N'2024-04-11T21:49:23.253' AS DateTime))
INSERT [dbo].[ActivityImages] ([Id], [ActivityId], [ImageName], [UploadPath], [UploadedDate]) VALUES (20, 188, N'98e258e0-9030-45df-acd2-7f25db6c5f8f.jpg', N'https://localhost:7007/Upload/98e258e0-9030-45df-acd2-7f25db6c5f8f.jpg', CAST(N'2024-04-11T22:00:40.290' AS DateTime))
SET IDENTITY_INSERT [dbo].[ActivityImages] OFF
GO
SET IDENTITY_INSERT [dbo].[ActivityNotes] ON 

INSERT [dbo].[ActivityNotes] ([id], [Notes], [activityId]) VALUES (10, N'tesfd123', 154)
SET IDENTITY_INSERT [dbo].[ActivityNotes] OFF
GO
SET IDENTITY_INSERT [dbo].[Users_table] ON 

INSERT [dbo].[Users_table] ([Id], [Username], [ClientName], [Password], [Isactive], [CreatedDate], [LastLogindate]) VALUES (1, N'shivamparikh', N'shivam', N'shivam@123', 1, CAST(N'2024-04-02T00:00:00.000' AS DateTime), CAST(N'2024-04-10T19:47:00.817' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users_table] OFF
GO
ALTER TABLE [dbo].[Activity_Customer_Table]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Customer_Table_Activity_Table] FOREIGN KEY([activityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[Activity_Customer_Table] CHECK CONSTRAINT [FK_Activity_Customer_Table_Activity_Table]
GO
ALTER TABLE [dbo].[Activity_SignOffdetail]  WITH CHECK ADD  CONSTRAINT [FK_Activity_SignOffdetail_Activity_Table] FOREIGN KEY([activityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[Activity_SignOffdetail] CHECK CONSTRAINT [FK_Activity_SignOffdetail_Activity_Table]
GO
ALTER TABLE [dbo].[Activity_Status_Table]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Status_Table_Activity_Table] FOREIGN KEY([activityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[Activity_Status_Table] CHECK CONSTRAINT [FK_Activity_Status_Table_Activity_Table]
GO
ALTER TABLE [dbo].[Activity_Table]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Userid] FOREIGN KEY([userID])
REFERENCES [dbo].[Users_table] ([Id])
GO
ALTER TABLE [dbo].[Activity_Table] CHECK CONSTRAINT [FK_Activity_Userid]
GO
ALTER TABLE [dbo].[Activity_Trailer_Table]  WITH CHECK ADD  CONSTRAINT [FK_Activity_Trailer_Table_Activity_Table] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[Activity_Trailer_Table] CHECK CONSTRAINT [FK_Activity_Trailer_Table_Activity_Table]
GO
ALTER TABLE [dbo].[ActivityDetail]  WITH CHECK ADD  CONSTRAINT [FK_ActivityDetail_ActivityTable] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[ActivityDetail] CHECK CONSTRAINT [FK_ActivityDetail_ActivityTable]
GO
ALTER TABLE [dbo].[ActivityImages]  WITH CHECK ADD  CONSTRAINT [FK_ActivityImages_ActivityTable] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[ActivityImages] CHECK CONSTRAINT [FK_ActivityImages_ActivityTable]
GO
ALTER TABLE [dbo].[ActivityNotes]  WITH CHECK ADD  CONSTRAINT [FK_ActivityNotes_ActivityTable] FOREIGN KEY([activityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[ActivityNotes] CHECK CONSTRAINT [FK_ActivityNotes_ActivityTable]
GO
ALTER TABLE [dbo].[ActivityProduct_Details]  WITH CHECK ADD  CONSTRAINT [FK_Productdetails_Activityid] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[ActivityProduct_Details] CHECK CONSTRAINT [FK_Productdetails_Activityid]
GO
ALTER TABLE [dbo].[ActivityResources_Details]  WITH CHECK ADD  CONSTRAINT [FK_ResourcesDetails_ActivityId] FOREIGN KEY([ActivityId])
REFERENCES [dbo].[Activity_Table] ([Id])
GO
ALTER TABLE [dbo].[ActivityResources_Details] CHECK CONSTRAINT [FK_ResourcesDetails_ActivityId]
GO
USE [master]
GO
ALTER DATABASE [caseproDB] SET  READ_WRITE 
GO
