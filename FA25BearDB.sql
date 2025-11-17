USE master
GO

Create Database FA25BearDB
go

use FA25BearDB
go

CREATE TABLE BearAccount (
    AccountID INT PRIMARY KEY, 
    Username VARCHAR(100) NOT NULL, 
	Fullname VARCHAR(100),
    Email VARCHAR(255) NOT NULL, 
	Password VARCHAR(255) NOT NULL, 
    RoleId int, 
    
);
go
create table BearType (
	BearTypeId INT PRIMARY KEY,
	BearTypeName VARCHAR(255),
	Origin VARCHAR(50),
	Description VARCHAR(255)
);
go
create table BearProfile (
	BearProfileId INT PRIMARY KEY,
    BearTypeId INT,
    BearName VARCHAR(255) NOT NULL,
    BearWeight Decimal(10, 2),
    Characteristics VARCHAR(50),
    CareNeeds VARCHAR(50),
    ModifiedDate Date,
    CONSTRAINT fk_bearprofile_beartype FOREIGN KEY (BearTypeId) REFERENCES BearType(BearTypeId) ON DELETE CASCADE
);


