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
	    ('Marwa', 'NASR', '319876546', 'Marwa@company.com'),
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
	[RoleID]           [nvarchar](50) DEFAULT  'Reciption'  NOT NULL,
	
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
			 (100002, 'Reciption'),
			 (100003,'Reciption'),
			 (100004,'Reciption')
GO			 


/* Car Table */
print '' print '*** creating Car table ***'
GO
CREATE TABLE [dbo].[Cars] (
    	[CarID]       [int]  IDENTITY(100000, 1)  NOT NULL,
	[Name]        [nvarchar] (50)             NOT NULL,
	[Model]       [nvarchar] (50)             NOT NULL,
	[Color]       [nvarchar] (50)             NOT NULL,
	[Year]        [nvarchar] (50)             NOT NULL,
	[Active] [bit]                         NOT NULL DEFAULT 1,
	CONSTRAINT   [pk_CarID] PRIMARY KEY ([CarID])
)
GO
print '' print '*** inserting Car test records ***'
GO
INSERT INTO [dbo].[Cars]
         ([Name], [Model], [Color], [Year])
		 VALUES
		     ('Camry', 'Toyota','white','2023'),
			 ('Corrella', 'Toyota','Silver','2020'),
			 ('Tucosn', 'kia','brown','2022')
GO



/*Customer */
print '' print '*** creating Customers table ***'
GO
CREATE TABLE [dbo].[Customers] (
    	[CustomerID]       [int]  IDENTITY(100000, 1)  NOT NULL,
	[FirstName]        [nvarchar] (50)             NOT NULL,
	[LastName]       [nvarchar] (50)             NOT NULL,
	[Email]       [nvarchar] (50)             NOT NULL,
	[PhoneNumber]        [nvarchar] (50)             NOT NULL,
	[Active]	bit	default 1,
	CONSTRAINT   [pk_CustomerID] PRIMARY KEY ([CustomerID])
)
GO
print '' print '*** inserting Customers test records ***'
GO
INSERT INTO [dbo].[Customers]
         ([FirstName], [LastName], [Email], [PhoneNumber])
		 VALUES
		     ('John', 'Kennedy','John@Gmail.com','123456789'),
			 ('Ali', 'Mohamed','Ali@Gmail.com','3192134432'),
			 ('Maria', 'Jackson','Maria67@Gmail.com','7023456578')
GO

/*Transaction */
print '' print '*** creating transactions table ***'
GO
CREATE TABLE [dbo].[transactions] (
    	[transactionID]       [int]  IDENTITY(100000, 1)  NOT NULL,
	[customerId]        [int]             NOT NULL,
	[CarId]       [int]             NOT NULL,
	[Price]       [nvarchar] (50)             NOT NULL,
	[Date]        [nvarchar] (50)             NOT NULL,
	CONSTRAINT   [pk_transactionID] PRIMARY KEY ([transactionID]),
	CONSTRAINT [fk_transactions_customerId] FOREIGN KEY([customerId])
	    REFERENCES [dbo].[Customers]([CustomerID]),
	CONSTRAINT [fk_transactions_CarId] FOREIGN KEY([CarId])
	    REFERENCES [dbo].[Cars]([CarID])
)
GO
print '' print '*** inserting transactions test records ***'
GO
INSERT INTO [dbo].[transactions]
         ([customerId], [CarId], [Price], [Date])
		 VALUES
		     (100000, 100000,'3000','11/18/2023')




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
	SELECT [EmployeeID],[GivenName],[FamilyName],[Phone],[Email],[PasswordHash],[Active]
	FROM [dbo].[Employee]	  
	END
GO
print '' print '*** creating sp_insert_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_employee]
(@GivenName NVARCHAR(50), @FamilyName NVARCHAR(50), @Phone NVARCHAR(11), @Email NVARCHAR(100), @Password NVARCHAR(100),  @Active bit, @Role NVARCHAR(50))
AS    
      BEGIN
	      INSERT INTO [dbo].[Employee]
	([GivenName],[FamilyName],[Phone],[Email],[PasswordHash],[Active])
		VALUES (@GivenName,@FamilyName,@Phone,@Email,@Password,@Active);
DECLARE @EmployeeID AS VARCHAR(100)
SET @EmployeeID = (SELECT EmployeeID
FROM [dbo].[Employee]
WHERE [GivenName] = @GivenName
AND [FamilyName] = @FamilyName
AND [Phone] = @Phone
AND [Email] = @Email
AND [Active] = @Active);
INSERT INTO [dbo].[EmployeeRole]
	([EmployeeID],[RoleID])
VALUES(@EmployeeID,@Role);
return @EmployeeID;

	  END
GO
print '' print '*** creating sp_delete_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_employee]
(@EmployeeID INT)

AS    
      BEGIN
	 UPDATE [dbo].[Employee]
	SET [Active] = 0
	WHERE EmployeeID = @EmployeeID;		  
     END
GO	
GO
print '' print '*** creating sp_update_employee ***'
GO
CREATE PROCEDURE [dbo].[sp_update_employee]
(@EmployeeID INT, @GivenName [nvarchar] (50) , @FamilyName [nvarchar] (50) , @Phone [nvarchar] (11), @Email [nvarchar] (100), @Password [nvarchar] (100), @Active bit, @Role [nvarchar](50))

AS    
      BEGIN
	 UPDATE [dbo].[Employee]
	 SET	[GivenName] = @GivenName,
		[FamilyName]=@FamilyName,
		[Phone] = @Phone,
		[Email] = @Email,
		[PasswordHash] = @Password,
		[Active] = @Active
	 WHERE EmployeeID = @EmployeeID;
	UPDATE [dbo].[EmployeeRole]
	SET	[RoleID] = @Role
	WHERE 	EmployeeID = @EmployeeID;	  
     END
GO	

print '' print '*** creating sp_select_all_cars ***'
GO
CREATE PROCEDURE [dbo].[sp_select_all_cars]
AS    
	BEGIN
		SELECT [CarID],[Name], [Model], [Color], [Year], [Active]
		FROM [dbo].[Cars]		  
	END
GO
print '' print '*** creating sp_insertCar ***'
GO
CREATE PROCEDURE [dbo].[sp_insertCar]
(@Name NVARCHAR(50), @Model NVARCHAR(50), @Color NVARCHAR(50), @Year NVARCHAR(50), @Active NVARCHAR(50))
AS    
 BEGIN
		INSERT INTO [dbo].[Cars]
			([Name],[Model],[Color],[Year],[Active])
		VALUES (@Name,@Model,@Color,@Year,@Active);
		Return @@ROWCOUNT
 END
GO
print '' print '*** creating sp_update_car ***'
GO
CREATE PROCEDURE [dbo].[sp_update_car]
(@CarID INT, @Name NVARCHAR(50), @Model NVARCHAR(50), @Color NVARCHAR(50), @Year NVARCHAR(50), @Active NVARCHAR(50))

AS    
      BEGIN
	 UPDATE [dbo].[Cars]
	 SET	[Name] = @Name,
		[Model]=@Model,
		[Color] = @Color,
		[Year] = @Year,
		[Active] = @Active
	 WHERE CarID = @CarID;	  
     END
GO	
print '' print '*** creating sp_select_customers ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customers]
AS    
	BEGIN
		SELECT [CustomerID],[FirstName], [LastName], [Email], [PhoneNumber]
		FROM [dbo].[Customers]	
		WHERE Active = 1;	  
	END
GO
print '' print '*** creating sp_insert_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_customer]
(@FirstName NVARCHAR(50), @LastName NVARCHAR(50), @Email NVARCHAR(50), @Phone NVARCHAR(50))
AS    
 BEGIN
		INSERT INTO [dbo].[Customers]
			([FirstName], [LastName], [Email], [PhoneNumber])
		VALUES (@FirstName,@LastName,@Email,@Phone);
		Return @@ROWCOUNT
 END
GO
print '' print '*** creating sp_update_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_update_customer]
(@CustomerID int,@FirstName NVARCHAR(50), @LastName NVARCHAR(50), @Email NVARCHAR(50), @Phone NVARCHAR(50))
AS    
 BEGIN
		UPDATE [dbo].[Customers]
		SET [FirstName]= @FirstName, [LastName]= @LastName, [Email]= @Email, [PhoneNumber]= @Phone
		WHERE  [CustomerID]= @CustomerID;
		Return @@ROWCOUNT
 END
GO
print '' print '*** creating sp_delete_customer ***'
GO
CREATE PROCEDURE [dbo].[sp_delete_customer]
(@CustomerID int)
AS    
 BEGIN
		UPDATE [dbo].[Customers]
		SET [Active]= 0
		WHERE  [CustomerID]= @CustomerID;
		Return @@ROWCOUNT
 END
GO
print '' print '*** creating sp_insert_transaction ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_transaction]
(@CustomerId int, @CarId int, @Price NVARCHAR(50), @Date NVARCHAR(50))
AS    
 BEGIN
		INSERT INTO [dbo].[transactions]
         	([customerId], [CarId], [Price], [Date])
		VALUES (@CustomerId,@CarId,@Price,@Date);
		Return @@ROWCOUNT
 END
GO
print '' print '*** creating sp_select_transactions_by_customerId ***'
GO
CREATE PROCEDURE [dbo].[sp_select_transactions_by_customerId]
(@customerID int)
AS    
	BEGIN
		SELECT [transactionID], [customerId], [CarId], [Price], [Date]
		FROM [dbo].[transactions]	
		WHERE [customerId] = @customerID;	  
	END
GO