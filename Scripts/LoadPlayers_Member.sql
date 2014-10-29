BULK
INSERT Players_Member
FROM 'c:\data\players_member.txt'
WITH
(
FIELDTERMINATOR = '\t',
ROWTERMINATOR = '\r'
)