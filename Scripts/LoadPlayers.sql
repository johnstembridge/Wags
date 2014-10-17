BULK
INSERT Players
FROM 'c:\data\players.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)