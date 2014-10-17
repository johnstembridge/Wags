BULK
INSERT wags.dbo.Organisers
FROM 'c:\data\organisers.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)