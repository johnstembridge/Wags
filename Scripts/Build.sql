/****** Create Db and populate  ******/
--:setvar scriptPath "c:\users\jstembridge\documents\Visual Studio 2013\Projects\WAGS\Scripts"
:setvar scriptPath "d:\Wags\Scripts"
:r $(scriptPath)\CreateDb.sql
:r $(scriptPath)\LoadPlayers.sql
:r $(scriptPath)\LoadPlayers_Member.sql
:r $(scriptPath)\LoadClubs.sql
:r $(scriptPath)\LoadCourses.sql
:r $(scriptPath)\LoadTrophies.sql
:r $(scriptPath)\LoadEvents.sql
:r $(scriptPath)\LoadHistory.sql
:r $(scriptPath)\LoadRounds.sql
:r $(scriptPath)\LoadEventOrganiser.sql
:r $(scriptPath)\LoadScores.sql
:r $(scriptPath)\LoadTransactions.sql
:r $(scriptPath)\LoadBookings.sql
:r $(scriptPath)\LoadGuests.sql