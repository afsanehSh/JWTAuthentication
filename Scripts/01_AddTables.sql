IF not EXISTS(SELECT TOP 1 1 FROM MASTER.sys.databases WHERE NAME = 'JWtAuthenticationDb')
BEGIN
CREATE DATABASE JWtAuthenticationDb
END
go
use JWtAuthenticationDb
go
/*استان*/
IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'Province')
BEGIN
CREATE SEQUENCE dbo.ProvinceSeqId  
    START WITH 1  
    increment BY 1;
	
CREATE TABLE dbo.Province
(
	Id int not null default (next value for dbo.ProvinceSeqId) primary key,
	ProvinceName nvarchar(250) not null,
)	
	
End


/*شهر*/
IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'City')
BEGIN
CREATE SEQUENCE dbo.CitySeqId  
    START WITH 1  
    increment BY 1;
	
CREATE TABLE dbo.City
(
	Id int not null default (next value for dbo.CitySeqId) primary key,
	CityName nvarchar(250) not null,
	ProvinceId int not null,
)	
	
END
 


/*جدول پایه*/
IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'Constant')
BEGIN
CREATE SEQUENCE dbo.ConstantSeqId  
    START WITH 1  
    increment BY 1;

CREATE TABLE dbo.Constant
(
	Id int not null default (next value for dbo.ConstantSeqId) primary key,
	ConstantName nvarchar(250) not null,
	ConstantTitle nvarchar(250) not null,
	ConstantType nvarchar(250) not null,
	CreateDate datetime null,
)

END



/* کاربران*/
IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'AppUser')
BEGIN
CREATE SEQUENCE dbo.AppUserSeqId  
    START WITH 1  
    increment BY 1;

CREATE TABLE dbo.AppUser
(
	Id int not null default (next value for dbo.AppUserSeqId) primary key,
	UserName nvarchar(250) not null,
	Email nvarchar(250) not null,
	Password nvarchar(250) not null,
	FirstName nvarchar(250) null,
	LastName nvarchar(250) null,
	PhoneNumber nchar(11) null,
	Mobile nchar(11) null,
	DateOfBirth datetime null,
	PersonalCode nchar(10) null,
	UserAddress nvarchar(max) null,
	IsActive bit null,
	IsAdmin bit null,
	UserImage nvarchar(max) null,
	JobTitle nvarchar(250) null,
	EducationDegreeId int null,
	GenderTypeId int null,
	CityId int null,
	)
END


--------------------------------
IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'Role')
BEGIN
CREATE SEQUENCE dbo.RoleSeqId  
    START WITH 1  
    increment BY 1;
	
create table dbo.Role
(
	Id int not null default (next value for dbo.RoleSeqId) primary key,
	RoleName nvarchar(250) not null,
	RoleTitle nvarchar(100) null,
)	
	
End

go


IF not exists (SELECT TOP 1 1 FROM sys.objects WHERE NAME = 'UserRole')
BEGIN
CREATE SEQUENCE dbo.UserRoleSeqId  
    START WITH 1  
    increment BY 1;
	
create table dbo.UserRole
(
	Id int not null default (next value for dbo.UserRoleSeqId) primary key,
	RoleId int not null,
	UserId int not null,
)	
	
End
go
ALTER TABLE dbo.UserRole
ADD CONSTRAINT fk_UserRole_User FOREIGN KEY(UserId) REFERENCES dbo.AppUser(Id)
go
ALTER TABLE dbo.UserRole
ADD CONSTRAINT fk_UserRole_Role FOREIGN KEY(RoleId) REFERENCES dbo.Role(Id)
go

 /*add Role*/
INSERT INTO dbo.Role
VALUES (default,'Admin','Admin')

INSERT INTO dbo.Role
VALUES (default,'User','User')
