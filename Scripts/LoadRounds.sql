BULK
INSERT Wags.dbo.Rounds
FROM 'c:\data\rounds.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)