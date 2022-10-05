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