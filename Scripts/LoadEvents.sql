BULK
INSERT wags.dbo.Events
FROM 'c:\data\events.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)