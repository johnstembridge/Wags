BULK
INSERT Clubs
FROM 'c:\data\clubs.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)