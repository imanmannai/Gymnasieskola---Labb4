USE Gymnasieskola

SELECT 
    Fornamn + ' ' + Efternamn AS Namn,
    Befattning,
    DATEDIFF(YEAR, AnstallningsDatum, GETDATE()) AS AntalAr
FROM Personal;

INSERT INTO Personal (Fornamn, Efternamn, Personnummer, Befattning, Avdelning, Lon, AnstallningsDatum)
VALUES ('Sandra', 'Joansson', '200001010001', 'Lärare', 'SA21', 30000, '2023-08-01');

INSERT INTO Student (Fornamn, Efternamn, Personnummer, KlassID)
VALUES ('Oskar', 'Lund', '200305050002', 1);

INSERT INTO Betyg (StudentID, KursID, LarareID, Betyg, BetygsDatum)
VALUES (1, 1, 1, 'A', GETDATE());  -- StudentID, KursID, LarareID och Betyg

SELECT 
    Avdelning,
    SUM(Lon) AS TotalLon
FROM Personal
GROUP BY Avdelning;

SELECT 
    Avdelning,
    AVG(Lon) AS Medellon
FROM Personal
GROUP BY Avdelning;

CREATE PROCEDURE GetStudentInfo
    @StudentID INT
AS
BEGIN
    SELECT 
        s.Fornamn,
        s.Efternamn,
        k.KlassNamn,
        b.Betyg,
        c.KursNamn,
        p.Fornamn + ' ' + p.Efternamn AS Larare,
        b.BetygsDatum
    FROM Student s
    JOIN Klass k ON s.KlassID = k.KlassID
    LEFT JOIN Betyg b ON s.StudentId = b.StudentID
    LEFT JOIN Kurs c ON b.KursID = c.KursID
    LEFT JOIN Personal p ON b.LarareID = p.PersonalID
    WHERE s.StudentId = @StudentID;
END;

EXEC GetStudentInfo @StudentID = 3;

