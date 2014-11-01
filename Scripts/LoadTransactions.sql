USE Wags
BULK
INSERT Transactions
FROM 'c:\Data\transactions.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)