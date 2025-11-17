USE master
GO

CREATE DATABASE [FA25BearDB]
GO


USE [FA25BearDB]
GO
/****** Object:  Table [dbo].[BearAccount]    Script Date: 06/24/25 1:57:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[BearAccount](
	[AccountID] [int] NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Phone] [nvarchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_BearAccount] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BearProfile]    Script Date: 06/24/25 1:57:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[BearProfile](
	[BearProfileId] [int] NOT NULL,
	[BearTypeId] [int] NOT NULL,
	[BearName] [nvarchar](150) NOT NULL,
	[BearWeight] [float] NOT NULL,
	[Characteristics] [nvarchar](2000) NOT NULL,
	[CareNeeds] [nvarchar](1500) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_BearProfile] PRIMARY KEY CLUSTERED 
(
	[BearProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BearType]    Script Date: 06/24/25 1:57:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE TABLE [dbo].[BearType](
	[BearTypeId] [int] NOT NULL,
	[BearTypeName] [nvarchar](250) NULL,
	[Origin] [nvarchar](250) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_BearType] PRIMARY KEY CLUSTERED 
(
	[BearTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BearAccount] ON 

INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (1, N'admin', N'@1', N'admin', N'admin@leopard.com', N'09011223435', 4)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (2, N'manager', N'@1', N'manager', N'manager@leopard.com', N'09011223440', 1)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (3, N'staff', N'@1', N'staff', N'staff@leopard.com', N'09011223450', 2)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (4, N'member', N'@1', N'member', N'member@leopard.com', N'09011223458', 3)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (5, N'administrator', N'@1', N'administrator', N'administrator@leopard.com', N'09011223435', 4)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (9, N'moderator', N'@1', N'moderator', N'moderator@leopard.com', N'09011223435', 4)
INSERT [dbo].[BearAccount] ([AccountID], [UserName], [Password], [FullName], [Email], [Phone], [RoleId]) VALUES (10, N'developer', N'@1', N'developer', N'developer@leopard.com', N'09011223435', 4)
SET IDENTITY_INSERT [dbo].[BearAccount] OFF
GO
SET IDENTITY_INSERT [dbo].[BearProfile] ON 

INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (1, 1, N'Panthera tigris tigris', 35, N'The leopard possesses a tawny or rusty yellow-colored coat with close-set rosettes and dark spots', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (2, 1, N'Nepalaliis', 22, N'Females of this subspecies weigh around 29 kg, and males weigh around 56 kg', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (3, 1, N'Bhutanient', 39, N'The Sri Lankan leopard has historically been found across a wide range of habitats on the island nation including arid scrub jungle, rainforest, upper highland forest, and dry evergreen monsoon forest', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (4, 2, N'Bengal', 33, N'The leopards are either completely black due to a recessive phenotype or have the usual spotted coat', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (5, 2, N'Ekaterina', 30, N'The Javan leopard is critically endangered, and only about 250 individuals survive in protected habitats in their range', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (6, 2, N'Sierra', 31, N'Depletion of the prey base, poaching, habitat loss and also conflicts with humans have resulted in a rapid downfall in the numbers of the Javan leopard', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (7, 3, N'Panthera', 17, N'Like most other wildlife in the region, the leopard faces threats due to habitat loss and poaching for illegal wildlife trade', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (8, 3, N'Sumatra', 20, N'A report produced in 2016 came as a shock to conservationists since it revealed that there are only about 400 to 1,000 breeding adults of the Indochinese leopard left in the wild', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
INSERT [dbo].[BearProfile] ([BearProfileId], [BearTypeId], [BearName], [BearWeight], [Characteristics], [CareNeeds], [ModifiedDate]) VALUES (9, 3, N'Baliance', 20, N'The Indochinese leopard appears in a predominantly black form south of the Kra Isthmus and a predominantly spotted form north of the Isthmus', N'These animals are classified as endangered by the IUCN', CAST(N'2025-06-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[BearProfile] OFF
GO
SET IDENTITY_INSERT [dbo].[BearType] ON 

INSERT [dbo].[BearType] ([BearTypeId], [BearTypeName], [Origin], [Description]) VALUES (1, N'Sri Lankan Bear', N'Nepal', N'The Sri Lankan leopard (Panthera pardus kotiya) is a leopard subspecies that is native to Sri Lanka')
INSERT [dbo].[BearType] ([BearTypeId], [BearTypeName], [Origin], [Description]) VALUES (2, N'Javan Bear', N'Georgia', N'The highly threatened Javan leopard (Panthera pardus melas) is endemic to the Indonesian island of Java')
INSERT [dbo].[BearType] ([BearTypeId], [BearTypeName], [Origin], [Description]) VALUES (3, N'Indochinese Bear', N'Bali', N'The Indochinese leopard (Panthera pardus delacouri) is native to southern China and mainland Southeast Asia')
SET IDENTITY_INSERT [dbo].[BearType] OFF
GO
ALTER TABLE [dbo].[BearProfile]  WITH CHECK ADD  CONSTRAINT [FK_BearProfile_BearType] FOREIGN KEY([BearTypeId])
REFERENCES [dbo].[BearType] ([BearTypeId])
GO
ALTER TABLE [dbo].[BearProfile] CHECK CONSTRAINT [FK_BearProfile_BearType]
GO
