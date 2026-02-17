-- Alla lärare
SELECT *
FROM Personal
WHERE Befattning = 'Lärare';

-- Alla studenter i bokstavsordning
SELECT *
FROM Student
ORDER BY Efternamn;

-- Studenter i en specifik klass
SELECT S.StudentId, S.Fornamn, S.Efternamn, K.KlassNamn
FROM Student S
JOIN Klass K ON S.KlassID = K.KlassID
WHERE K.KlassNamn = 'EK22';

-- Betyg satta senaste månaden
SELECT B.BetygID, S.Fornamn, S.Efternamn, K.KursNamn, B.Betyg, B.BetygsDatum
FROM Betyg B
JOIN Student S ON B.StudentID = S.StudentId
JOIN Kurs K ON B.KursID = K.KursID
WHERE B.BetygsDatum >= DATEADD(MONTH, -1, GETDATE());