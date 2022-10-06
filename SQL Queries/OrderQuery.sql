Use BookStoreDB

Create Table OrderInfo(
OrderId int Identity Primary Key,
UserId int Not Null Foreign Key(UserId) References UserInfo(UserId),
BookId int Not Null Foreign Key(BookId) References BookInfo(BookId),
AddressId int Not Null Foreign Key(AddressId) References AddressInfo(AddressId),
OrderQuantity int Not Null,
TotalPrice money Not Null,
OrderDate DateTime Default GetDate()
) 
Select * From OrderInfo

------------ Stored Procedure For Adding Order ---------------
Create Procedure spAddOrder
(
@OrderQuantity int,
@UserId int,
@BookId int,
@AddressId int
)
As
Declare @TotalPrice int
Begin
		Set @TotalPrice = (Select DiscountedPrice From BookInfo Where BookId = @BookId);
		If(Exists(Select * From BookInfo Where BookId = @BookId))
		Begin
			If(Exists(Select * From UserInfo Where UserId = @UserId))
				BEGIN
						Begin Try
								Begin Transaction
								Insert Into OrderInfo(TotalPrice, OrderQuantity, OrderDate, UserId, BookId, AddressId)
								Values(@TotalPrice, @OrderQuantity, GETDATE(), @UserId, @BookId, @AddressId)
								Update BookInfo Set Quantity = Quantity - @OrderQuantity Where BookId = @BookId;
								Delete From CartInfo Where BookId = @BookId and UserId = @UserId;
								Select * From OrderInfo;
								Commit Transaction
						End Try
						Begin Catch
								rollback;
						End Catch
				END
		Else
				Begin
						Select 3;
				End
		End
	Else
		Begin
				Select 2;
		End
End;
Select * From OrderInfo

---------- Stored Procedure For Retrieving Order ----------
Create Procedure spRetrieveOrder
(
@UserId int
)
As
Begin
	Select O.OrderId, O.UserId, O.AddressId, B.BookId,
	O.TotalPrice, O.OrderQuantity, O.OrderDate,
	B.BookName, B.Author, B.BookImage
	From BookInfo B
	Inner Join OrderInfo O ON O.BookId = B.BookId
	Where O.UserId = @UserId
End