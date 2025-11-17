USE master
GO

CREATE DATABASE FA25BearDB  
GO

USE FA25BearDB 
GO

CREATE TABLE BearAccount (
    AccountID INT PRIMARY KEY, 
    UserName VARCHAR(100) NOT NULL, 
    Email VARCHAR(255) NOT NULL, 
    [Password] VARCHAR(255) NOT NULL, 
    FullName varchar(255),
    Phone varchar(255),
    RoleId int, 
);

delete from BearAccount

INSERT INTO BearAccount (AccountID, Username, Email, Password, FullName, Phone, RoleId) VALUES
(1, 'admin','admin@handbagasian.org',1 ,'admin1','1', 1),
(2, 'manager','manager@handbagasian.org',1, 'manager1','1', 2),
(3, 'staff', 'staff@handbagasian.org', 1,'staff1','1', 3),
(4, 'member', 'member@handbagasian.org', 1,'member1','1', 4)


CREATE TABLE BearType (
    BearTypeID INT PRIMARY KEY,
    BearTypeName VARCHAR(255) NOT NULL,
    Origin int,
    [Description] VARCHAR(255)
);


INSERT INTO BearType VALUES 
(1, 'type 1', 1, 't1'),
(2, 'type 2', 1, 't2')

CREATE TABLE BearProfile (
    BearProfileID INT PRIMARY KEY,
    BearTypeID INT FOREIGN KEY (BearTypeID) REFERENCES BearType(BearTypeID),
    BearName VARCHAR(255) NOT NULL,
    BearWeight int,
    Characteristics varchar(255),
    CareNeeds varchar(255),
    ModifiedDate date,
);