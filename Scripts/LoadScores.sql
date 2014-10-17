BULK
INSERT Wags.dbo.Scores
FROM 'c:\data\scores.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)