BULK
INSERT Members
FROM 'c:\data\members.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)