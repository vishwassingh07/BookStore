Use BookStoreDB

Create Table CartInfo(
CartId int Identity Primary Key,
UserId int Not Null Foreign Key(UserId) References UserInfo(UserId),
BookId int Not Null Foreign Key(BookId) References BookInfo(BookId),
BookQuantity int
)
Select * From CartInfo
------------ Stored Procedure For Adding To Cart --------
Create Procedure spAddToCart
(
@BookId int,
@UserId int,
@BookQuantity int
)
As
Begin
	Insert Into CartInfo(BookId, UserId, BookQuantity)
	Values(@BookId, @UserId, @BookQuantity)
End