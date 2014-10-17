BULK
INSERT Bookings
FROM 'c:\data\bookings.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)