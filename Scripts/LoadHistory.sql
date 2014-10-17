BULK
INSERT Wags.dbo.Histories
FROM 'c:\data\hcaps.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)