--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblUser')
drop table tblUser

create table tblUser
(
UserID int primary key IDENTITY(1,1),
Firstname varchar(100),
Lastname varchar(100),
DateOfBirth varchar(100),
Gender varchar(100),
Email varchar(100),
PhoneNumber varchar(100),
Username varchar(100),
Password varchar(100)
)