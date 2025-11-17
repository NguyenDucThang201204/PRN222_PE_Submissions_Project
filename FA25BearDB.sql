USE master
GO

CREATE DATABASE FA25BearDB  
GO

USE FA25BearDB 
GO

CREATE TABLE BearAccount (
    AccountID INT PRIMARY KEY, 
    UserName VARCHAR(100) NOT NULL, 
	Password VARCHAR(255) NOT NULL, 
    FullName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL, 
    Phone VARCHAR(100) NOT NULL,
    RoleId int
);

INSERT INTO BearAccount (AccountID, UserName, Password, FullName, Email, Phone, RoleId) VALUES
(1, 'admin456', '@1','LuanM', 'admin@handbagasian.org', '0123456789', 1),
(2, 'steve', '@1','LuanM','steve@handbagasian.org', '0123456789',  4),
(3, 'modjose','@1','LuanM', 'jose@handbagasian.org', '0123456789', 2),
(4, 'michael','@1','LuanM', 'machael@handbagasian.org', '0123456789', 4),
(5, 'devops','@1','LuanM', 'dev@globalflower.com', '0123456789', 3);

CREATE TABLE BearType (
    BearTypeID INT PRIMARY KEY,
    BearTypeName VARCHAR(255) NOT NULL,
    Origin VARCHAR(100),
    Description VARCHAR(255) 
);

CREATE TABLE BearProfile (
    BearProfileID INT PRIMARY KEY,
    BearTypeID INT,
    BearName VARCHAR(255) NOT NULL,
    BearWeight VARCHAR(100),
    Characteristics VARCHAR(50),
    CareNeeds VARCHAR(50),
    ModifiedDate DATE,
    CONSTRAINT fk_handbag_brand FOREIGN KEY (BearTypeID) REFERENCES BearType(BearTypeID) ON DELETE CASCADE
);

INSERT INTO BearType (BearTypeID, BearTypeName, Origin, Description) VALUES
(1, 'Louis Vuitton', 'France', 'https://www.louisvuitton.com'),
(2, 'Gucci', 'Italy', 'https://www.gucci.com'),
(3, 'Chanel', 'France', 'https://www.chanel.com'),
(4, 'Prada', 'Italy', 'https://www.prada.com'),
(5, 'Hermes', 'France', 'https://www.hermes.com'),
(6, 'Dior', 'France', 'https://www.dior.com'),
(7, 'Burberry', 'UK', 'https://www.burberry.com'),
(8, 'Coach', 'USA', 'https://www.coach.com'),
(9, 'Michael Kors', 'USA', 'https://www.michaelkors.com'),
(10, 'Fendi', 'Italy', 'https://www.fendi.com');


INSERT INTO BearProfile (BearProfileID, BearTypeID, BearName, BearWeight, Characteristics, CareNeeds, ModifiedDate) VALUES
(1, 1, 'Speedy 30', 'Canvas', 'Brown', 'Brown', '2024-01-10'),
(2, 2, 'Marmont Mini', 'Leather', 'Black', 'Brown', '2024-02-05'),
(3, 3, 'Classic Flap Bag', 'Leather', 'Black','Brown', '2023-12-20'),
(4, 4, 'Galleria Bag', 'Leather', 'Beige', 'Brown', '2023-11-15'),
(5, 5, 'Birkin 25', 'Leather', 'Orange', 'Brown', '2023-10-01'),
(6, 6, 'Lady Dior', 'Leather', 'Red', 'Brown', '2024-02-01'),
(7, 6, 'TB Bag', 'Leather', 'Beige', 'Brown', '2024-01-20'),
(8, 6, 'Tabby Shoulder Bag', 'Leather', 'Green', 'Brown', '2024-03-01'),
(9, 9, 'Jet Set Tote', 'Leather', 'Black', 'Brown', '2024-02-10'),
(10, 10, 'Baguette Bag', 'Canvas', 'Brown', 'Brown', '2023-12-10');
