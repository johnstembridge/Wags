BULK
INSERT wags.dbo.Trophies
FROM 'c:\data\trophies.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)