USE master

CREATE DATABASE FA25BearDB
GO

USE FA25BearDB
GO

CREATE TABLE BearAccount(
  AccountID nvarchar(20) primary key,
  UserName nvarchar(80) not null,
  Password nvarchar(80) not null,
  FullName nvarchar(80) not null,
  Email nvarchar(100) unique,
  Phone nvarchar(10) not null,
  RoleId int
)
GO

INSERT INTO BearAccount VALUES(N'BA0001', N'User1',N'@1', N'Administrator', 'admin@Bear.com.au', N'0853572937', 1);
INSERT INTO BearAccount VALUES(N'BA0002', N'User2',N'@1', N'Manager', 'manager@Bear.com.au', N'0739572583', 2);
INSERT INTO BearAccount VALUES(N'BA0003', N'User3',N'@1', N'Staff', 'staff@Bear.com.au', N'0123859673', 3);
INSERT INTO BearAccount VALUES(N'BA0004', N'User4',N'@1', N'Member', 'member@Bear.com.au', N'0535729352', 4);
GO


CREATE TABLE BearType(
  BearTypeId nvarchar(20) primary key,
  BearTypeName nvarchar(80) not null,
  Orgin nvarchar(150), 
  Description nvarchar(200)
)
GO
INSERT INTO BearType VALUES(N'BT0001',N'Brown', N'US',N'They are brown bears.')
INSERT INTO BearType VALUES(N'BT0002',N'Black', N'Canada',N'They are black bears.')
INSERT INTO BearType VALUES(N'BT0003',N'Panda', N'China',N'They are panda.')

GO

CREATE TABLE BearProfile (
 BearProfileId int primary key,
 BearName nvarchar(200) not null,
 BearWeight float not null,
 Characteristics nvarchar(220),
 CareNeeds bit not null,
 ModifiedDate datetime,
 BearTypeId nvarchar(20) references BearType(BearTypeId) on delete cascade on update cascade
)
GO


INSERT INTO BearProfile VALUES(1,N'Canada Brown bear',1000,N'It is athletic and excel at hunting and guarding.',1,CAST(N'2022-03-25' AS DateTime),'BT0001')
INSERT INTO BearProfile VALUES(2,N'US Black bear',1100, N'It was originally bred for hunting and herding.',0,CAST(N'2022-09-15' AS DateTime),'BT0002')
INSERT INTO BearProfile VALUES(3,N'Vietnamese Panda',700,N'It is fast, agile, and used for hunting and racing.',1,CAST(N'2022-08-05' AS DateTime), 'BT0003')
INSERT INTO BearProfile VALUES(4,N'Chinese Black bear',1200,N'It is commonly used as guard dogs and for hunting.',0,CAST(N'2022-09-16' AS DateTime), 'BT0002')
GO