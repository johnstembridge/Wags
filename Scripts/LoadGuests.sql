BULK
INSERT Guests
FROM 'c:\data\guests.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)