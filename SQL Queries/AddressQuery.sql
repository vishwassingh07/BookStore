Use BookStoreDB

Create Table AddressType(
AddressTypeId int Identity(1,1) Primary Key,
AddressType varchar(100)
)

Insert Into AddressType Values ('Home'),('Office'),('Other');

Select * From AddressType

Create Table AddressInfo(
AddressId int Identity(1,1) Primary Key,
Address varchar(max),
City varchar (100),
State varchar (100),
AddressTypeId int Foreign Key(AddressTypeId) References AddressType(AddressTypeId),
UserId int Foreign Key(UserId) References UserInfo(UserId)
)

-------------- Stored Procedure For Adding Address ---------
Create Procedure spAddAddress
(
@Address varchar(max),
@City varchar(max),
@State varchar(max),
@AddressTypeId int,
@UserId int
)
As
Begin
	Insert Into AddressInfo(Address, City, State, AddressTypeId, UserId)
	Values(@Address, @City, @State, @AddressTypeId, @UserId)
End

Select * From AddressInfo

------------- Stored Procedure For Deleting Address -----------
Create Procedure spDeleteAddress
(
@UserId int,
@AddressId int
)
As
Begin
	Delete From AddressInfo Where UserId = @UserId and AddressId = @AddressId
End

------------ Stored Procedure For Updating Address ------------
Create Procedure spUpdateAddress
(
@AddressId int,
@UserId int,
@Address varchar(max),
@City varchar(max),
@State varchar(max),
@AddressTypeId int
)
As
Begin
	Update AddressInfo Set Address = @Address, City = @City,State = @State, AddressTypeId = @AddressTypeId
	Where AddressId = @AddressId and UserId = @UserId
End

------------- Stored Procedure For Retrieving Address ------------
Create Procedure spRetrieveAddress
(
@UserId int
)
As
Begin
	Select AddressInfo.AddressId, AddressInfo.Address, AddressInfo.City, AddressInfo.State,
	UserInfo.UserId, UserInfo.FullName, UserInfo.MobileNumber
	From AddressInfo Inner Join UserInfo ON AddressInfo.UserId = UserInfo.UserId
End