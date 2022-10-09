Use BookStoreDB

Create Table FeedbackInfo(
FeedbackId int Identity(1,1) Primary Key,
Comments varchar(max),
TotalRating float,
BookId int Foreign Key References BookInfo(BookId),
UserId int Foreign Key References UserInfo(UserId)
)
Select * From FeedbackInfo

------- Stored Procedure For Adding Feedback --------
Create Procedure spAddFeedback
(
@TotalRating float,
@Comments varchar(max),
@BookId int,
@UserId int
)
As
Declare @AvgRating float;
Begin
	If(Not Exists(Select * From FeedbackInfo Where BookId = @BookId and UserId = @UserId))
		Begin
				Insert Into FeedbackInfo(TotalRating, Comments, BookId, UserId)
				Values(@TotalRating, @Comments, @BookId, @UserId)
				Set @AvgRating = (Select AVG(Rating) From BookInfo Where BookId = @BookId);
				Update BookInfo Set Rating = @AvgRating, RatingCount =(RatingCount+1) Where BookId = @BookId;
		End
End

Select * From FeedbackInfo
Select * From BookInfo