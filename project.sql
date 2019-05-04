IF db_id('formprojectDB') IS NULL 
    CREATE DATABASE formprojectDB

GO

CREATE TABLE formprojectDB.dbo.Users (
    id INT PRIMARY KEY IDENTITY (1, 1),
    first_name VARCHAR (50) NOT NULL,
    last_name VARCHAR (50) NOT NULL,
    registered_at DATETIME,
    secret VARCHAR(20),
    email VARCHAR (50) NOT NULL,
); 

CREATE TABLE formprojectDB.dbo.Forms (
    id INT PRIMARY KEY IDENTITY (1, 1),
    users_id INT NOT NULL,
    created_at DATETIME,
    context VARCHAR (MAX) NOT NULL,
    FOREIGN KEY (users_id) REFERENCES Users (id) 
); 