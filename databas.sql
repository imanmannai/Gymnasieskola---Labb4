   -- Skapa en databas
CREATE DATABASE Gymnasieskola;
USE Gymnasieskola;

--Skapa tabell för personalen
CREATE TABLE Personal (
      PersonalID INT PRIMARY KEY IDENTITY (1,1),
      Fornamn NVARCHAR(50),
      Efternamn NVARCHAR(50),
      Personnummer CHAR(12),
      Befattning NVARCHAR(50),
)

--Skapa tabell för klasser
CREATE TABLE Klass (
      KlassID INT PRIMARY KEY IDENTITY(1,1) ,
      KlassNamn NVARCHAR(20),
      AnsvarigLarareID INT,
FOREIGN KEY (AnsvarigLarareID) REFERENCES Personal(PersonalID)
)

--Skapa tabell för studenter 
CREATE TABLE Student (
      StudentId INT PRIMARY KEY IDENTITY(1,1),
      Fornamn NVARCHAR(30),
      Efternamn NVARCHAR(30),
      Personnummer CHAR(12),
      KlassID INT, 
      FOREIGN KEY (KlassID) REFERENCES Klass(KlassID)
)
--Skapa tabell för kurser 
CREATE TABLE Kurs (
      KursID INT IDENTITY PRIMARY KEY,
      KursNamn NVARCHAR(40),
      Poang INT
)
--Skapa tabell för betygen
CREATE TABLE Betyg (
      BetygID INT PRIMARY KEY IDENTITY (1,1),
      StudentID INT,
      KursID INT,
      LarareID INT,
      Betyg CHAR(1),
      BetygsDatum DATE,
      FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
      FOREIGN KEY (KursID) REFERENCES Kurs(KursID),
      FOREIGN KEY (LarareID) REFERENCES Personal(PersonalID)
      )

      --Skapa testdata i databasen
      INSERT INTO Personal (Fornamn, Efternamn, Personnummer, Befattning)
      Values 
      ('Sara', 'Petersson', '198904121234', 'Lärare'),
      ('Alex', 'Berg', '197509305678', 'Lärare'),
      ('Ahmed', 'Ezzedine', '197306175555', 'Rektor'),
      ('Samira', 'Mohammed', '199003104321', 'Administratör');

      INSERT INTO Klass (KlassNamn, AnsvarigLarareID)
      VALUES
      ('EK21', 1),
      ('SA22', 2),
      ('NK23', 3),
      ('EK22', 4);

-- Lägger till studenter 
INSERT INTO Student (Fornamn, Efternamn, Personnummer, KlassID)
VALUES 
('David', 'Krets', '200802022222', 1),
('Sandra', 'Fors', '200704246622', 2),
('Alexandra', 'Morad', '200702022266', 3),
('Mikael', 'Nilsson', '200803030000', 4),
('Amani', 'Gatoui', '200706031122', 1),
('Mikail', 'Ahmet', '200807084433', 2),
('Mäläk', 'Mannai', '200705063344', 3),
('Cheima', 'Monsi', '200801022233', 4);

-- Lägger till kurs & poäng
INSERT INTO Kurs (KursNamn, Poang)
VALUES
('Matematik 1', 100),
('Svenska 1', 100),
('Engelska 1', 100);

-- Lägger till data i Betyg samt kopplar tabellen med Student - Kurs - Lärare - Betyg och BetygsDatum
INSERT INTO Betyg (StudentID, KursID, LarareID, Betyg, BetygsDatum)
VALUES 
-- Student 1
(1, 1, 1, 'A', GETDATE()),  -- Matematik 1
(1, 2, 2, 'B', GETDATE()),  -- Svenska 1
(1, 3, 3, 'C', GETDATE()),  -- Engelska 1

-- Student 2
(2, 1, 1, 'B', GETDATE()),  -- Betug idag
(2, 2, 2, 'A', GETDATE()),
(2, 3, 3, 'B', GETDATE()),

-- Student 3
(3, 1, 1, 'C', GETDATE()),
(3, 2, 2, 'B', GETDATE()),
(3, 3, 3, 'A', GETDATE()),

-- Student 4
(4, 1, 1, 'A', GETDATE()),
(4, 2, 2, 'C', GETDATE()),
(4, 3, 3, 'B', GETDATE()),

-- Student 5
(5, 1, 1, 'B', DATEADD(DAY, -5, GETDATE())), -- 5 dagar sedan
(5, 2, 2, 'B', DATEADD(DAY, -5, GETDATE())),
(5, 3, 3, 'C', DATEADD(DAY, -5, GETDATE())),

-- Student 6
(6, 1, 1, 'C', DATEADD(DAY, -10, GETDATE())), -- 10 dagar sedan
(6, 2, 2, 'A', DATEADD(DAY, -10, GETDATE())),
(6, 3, 3, 'B', DATEADD(DAY, -10, GETDATE())),

-- Student 7
(7, 1, 1, 'A', DATEADD(DAY, -10, GETDATE())), -- 10 dagar sedan
(7, 2, 2, 'B', DATEADD(DAY, -10, GETDATE())),
(7, 3, 3, 'A', DATEADD(DAY, -10, GETDATE())),

-- Student 8
(8, 1, 1, 'B', DATEADD(DAY, -8, GETDATE())),
(8, 2, 2, 'C', DATEADD(DAY, -8, GETDATE())),
(8, 3, 3, 'B', DATEADD(DAY, -8, GETDATE()));

-- Hämtar all personal som är lärare 
SELECT * FROM Personal
WHERE befattning = 'Lärare';

-- Hämta alla studenter i bokstavsordning, sorterat på efternamn
SELECT * FROM Student 
ORDER BY Efternamn;

SELECT S.StudentId, S.Fornamn, S.Efternamn, K.KlassNamn
FROM Student S
JOIN Klass K ON S.KlassID = K.KlassID
WHERE K.KlassNamn = 'EK22'

SELECT B.BetygID, S.Fornamn, S.Efternamn, K.KursNamn, B.Betyg, B.BetygsDatum
FROM Betyg B
JOIN Student S ON B.StudentID = S.StudentId
JOIN Kurs K ON B.KursID = K.KursID
WHERE B.BetygsDatum >= DATEADD(MONTH, -1, GETDATE());