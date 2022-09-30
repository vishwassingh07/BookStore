Use BookStoreDB

Create Table WishList(
WishListId int Identity Primary Key,
UserId int Not Null Foreign Key(UserId) References UserInfo(UserId),
BookId int Not Null Foreign Key(BookId) References BookInfo(BookId),
)
--------- Stored Procedure For Adding Book To WishList -----------
Create Procedure spAddToWishlList
(
@UserId int,
@BookId int
)
As
Begin
	Insert Into WishList(UserId, BookId)
	Values(@UserId, @BookId)
End

-------- Stored Procedure For Deleting From WishList -----------
Create Procedure spDeleteFromWishList
(
@UserId int,
@WishListId int
)
As
Begin
	Delete From WishList Where UserId = @UserId and WishListId = @WishListId
End