USE [ReservationSystem]
GO

/****** Object:  StoredProcedure [dbo].[ValidateReservation]    Script Date: 3/31/2023 11:18:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE OR ALTER   PROCEDURE [dbo].[ValidateReservation] @LocationTimeSlotID nvarchar(100), @ReservationDate Date, @NumberOfGuests int
AS

DECLARE @Capacity AS int 
DECLARE @CurrentReservedGuests as int

SELECT	@Capacity = l.Capacity 
	FROM Reservations AS r 
	INNER JOIN LocationTimeSlots as lts
	ON r.LocationTimeSlotID = lts.LocationTimeSlotID
	INNER JOIN Locations as l
	on lts.LocationID = l.LocationID
	WHERE lts.LocationTimeSlotID = @LocationTimeSlotID 

SELECT	@CurrentReservedGuests = SUM(r.NumberOfGuests)
	FROM Reservations AS r 
	WHERE r.LocationTimeSlotID = @LocationTimeSlotID 
		AND ReservationDate = @ReservationDate

RETURN (
	SELECT CASE
		WHEN (@CurrentReservedGuests + @NumberOfGuests) > @Capacity THEN 0
		ELSE 1
	END)

GO


