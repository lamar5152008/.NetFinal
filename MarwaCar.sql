/* check to see if the database exists, if so, dorp it */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'MarwaCar')
BEGIN
	DROP DATABASE MarwaCar
	print '' print '*** dropping database MarwaCar ***'
END
GO

print '' print '*** creating database MarwaCar ***'	
GO
CREATE DATABASE MarwaCar
GO
USE MarwaCar
GO

/* Employee Table */
print '' print '*** creating employee table ***'
GO
CREATE TABLE [dbo].[Employee] (
    [EmployeeID]       [int]  IDENTITY(100000, 1)  NOT NULL,
	[GivenName]        [nvarchar] (50)               NOT NULL,
	[FamilyName]       [nvarchar] (50)               NOT NULL,
	[Phone]            [nvarchar] (11)               NOT NULL,
	[Email]            [nvarchar] (100)              NOT NULL,
	[PasswordHash]     [nvarchar] (100)              NOT null DEFAULT
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]           [bit]                         NOT NULL DEFAULT 1,
	CONSTRAINT   [pk_EmployeeID] PRIMARY KEY ([EmployeeID]),
	CONSTRAINT   [ak_Email] UNIQUE ([Email])
)
GO


print '' print '*** inserting employee test records ***'
GO
INSERT INTO [dbo].[Employee]
         ([GivenName], [FamilyName], [Phone], [Email])
	VALUES
	    ('Marwa', 'NASR', '319876546', 'Marwa@comany.com'),
		('Mahammed', 'amgad', '319000007', 'mahammed@company.com'),
		('Leen', 'Ibrahim', '319872340', 'leen@company.com'),
		('Lamar', 'Ayman', '319501276', 'lamar@company.com'),
		('Ahmed', 'Mohammad', '319507418', 'ahmed@company.com')
GO

/* Role Table */
print '' print '*** creating role table ***'
GO
CREATE TABLE [dbo].[Role] (
     [RoleID]          [nvarchar](50)
	 CONSTRAINT [pk_RoleID] PRIMARY KEY([RoleID])
)
GO

print '' print '*** inserting role test records ***'
GO
INSERT INTO [dbo].[Role]
         ([RoleID])
	VALUES
	    ('Manager'),
		('Admin'),
		('Reciption')
		
GO

/* Employee Table */
print '' print '*** creating EmployeeRole table ***'
GO
CREATE TABLE [dbo] . [EmployeeRole] (
    [EmployeeID]       [int]             NOT NULL,
	[RoleID]           [nvarchar](50)    NOT NULL,
	
	CONSTRAINT [fk_EmployeeRole_EmployeeID] FOREIGN KEY([EmployeeID])
	    REFERENCES [dbo].[Employee]([EmployeeID]),
		
	CONSTRAINT [fk_EmployeeRole_RoleID] FOREIGN KEY([RoleID])
	    REFERENCES [dbo].[Role]([RoleID]),
		
	CONSTRAINT [pk_EmployeeRole] PRIMARY KEY ([EmployeeID], [RoleID])
)
GO

print '' print '*** inserting EmployeeRole test records ***'
GO
INSERT INTO [dbo].[EmployeeRole]
         ([EmployeeID], [RoleID])
		 VALUES
		     (100000, 'Admin'),
			 (100001, 'Manager'),
			 (100002, 'Reciption')
GO			 

print '' print '*** creating sp_verify_user ***'
GO
CREATE PROCEDURE [dbo].[sp_verify_user]
(
    @UserName           [nvarchar](100), 
    @Password           [nvarchar](100)
)
AS
     BEGIN
	   Select         [EmployeeID], [PasswordHash], [Email]
		  FROM    [Employee]
		  WHERE   @UserName = [Email]
                  And     @Password = [PasswordHash]
	END
GO

print '' print '*** creating sp_select_roles_by_employeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_employeeID]
(
      @EmployeeID        [int]
)
AS    
      BEGIN
	      SELECT  [RoleID]
		  FROM    [EmployeeRole]
		  WHERE   @EmployeeID = [EmployeeID]
	  END
GO



print '' print '*** creating sp_select_all_employees ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_employees]

AS    
      BEGIN
	      SELECT  [EmployeeID],[GivenName],[FamilyName],[Phone],[Email],[PasswordHash],[Active]
		  FROM    [dbo].[Employee]
		  
	  END
GO

	




