BULK
INSERT EventOrganiser
FROM 'c:\data\eventorganiser.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)