Use BookStoreDB

Create Table BookInfo(
BookId int Primary Key identity,
BookName varchar(100) Unique Not Null,
Author varchar(200) Unique Not Null,
Description varchar(max) Not Null,
Quantity int Not Null,
Price money Not Null,
DiscountedPrice money Not Null,
Rating float,
RatingCount int,
BookImage varchar(255)
)
Select * From BookInfo

------------ Stored Procedure For Adding Book ---------
Create Procedure spAddBook
(
@BookName varchar(100),
@Author varchar(200),
@Description varchar(max),
@Quantity int,
@Price money,
@DiscountedPrice money,
@Rating float,
@RatingCount int,
@BookImage varchar
)
As
Begin
	Insert Into BookInfo(BookName, Author, Description, Quantity, Price, DiscountedPrice, Rating, RatingCount, BookImage)
	Values(@BookName, @Author, @Description, @Quantity, @Price, @DiscountedPrice, @Rating, @RatingCount, @BookImage)
End

------------- Stored Procedur For Retrieving Books -------------

Create Procedure spGetAllBook
As
Begin
	Select * From BookInfo
End