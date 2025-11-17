create database FA25BearDB

create table BearAccount(
AccountId int PRIMARY KEY,
Username Nvarchar(20),
Password Nvarchar(20),
FullName Nvarchar(20),
Email Nvarchar(20),
Phone Nvarchar(20),
RoleId int
)


USE [FA25BearDB]
GO

INSERT INTO [dbo].[BearAccount]
           ([AccountId]
           ,[Username]
           ,[Password]
           ,[FullName]
           ,[Email]
           ,[Phone]
           ,[RoleId])
     VALUES
           (1
           ,'tab'
           ,'1234'
           ,'TabValensky'
           ,'tab@a.com'
           ,'0912345678'
           ,1
		   )
GO
