BULK
INSERT Wags.dbo.Transactions
FROM 'c:\data\transactions.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)