BULK
INSERT courses
FROM 'c:\data\courses.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)