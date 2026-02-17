using System;
using System.Linq;
using GymnasieskolaDb;
using Microsoft.EntityFrameworkCore;

bool running = true;

while (running)
{
    Console.WriteLine("\n--- GYMNASIESKOLA ---");
    Console.WriteLine("1. Visa alla studenter (sortering)");
    Console.WriteLine("2. Antal lärare per avdelning");
    Console.WriteLine("3. Visa alla studenter med klass, kurser och betyg");
    Console.WriteLine("4. Visa aktiva kurser");
    Console.WriteLine("5. Sätt betyg på en student");
    Console.WriteLine("0. Avsluta");

    Console.Write("Välj ett alternativ: ");
    string choice = Console.ReadLine() ?? "";
    switch (choice)
    {
        case "1":
            VisaAllaStudenter();
            break;

        case "2":
            AntalLararePerAvdelning();
            break;

        case "3":
            VisaStudentFullInfo();
            break;

        case "4":
            VisaAktivaKurser();
            break;

        case "5":
            SattBetyg();
            break;

        case "0":
            running = false;
            break;

        default:
            Console.WriteLine("Fel val, försök igen.");
            Console.ReadLine();
            break;
    }
}
// --- MenyVal 1 ---
static void VisaAllaStudenter()
{
    using var context = new GymnasieskolaContext();

    // Be användaren välja sorteringsfält
    Console.WriteLine("\nSortera efter: 1. Förnamn  2. Efternamn");
    Console.Write("Val: ");
    string sortChoice = Console.ReadLine() ?? "";

    // Be användaren välja sorteringsordning
    Console.WriteLine("Sorteringsordning: 1. Stigande  2. Fallande");
    Console.Write("Val: ");
    string orderChoice = Console.ReadLine() ?? "";

    // Hämta studenter från databasen
    var studenter = context.Student.AsQueryable();

    // Tillämpa sortering
    if (sortChoice == "1") // Förnamn
    {
        studenter = orderChoice == "1" ? studenter.OrderBy(s => s.Fornamn)
                                        : studenter.OrderByDescending(s => s.Fornamn);
    }
    else if (sortChoice == "2") // Efternamn
    {
        studenter = orderChoice == "1" ? studenter.OrderBy(s => s.Efternamn)
                                        : studenter.OrderByDescending(s => s.Efternamn);
    }

    // Kör frågan och skapa lista
    var lista = studenter.ToList();

    // Visa resultat
    Console.WriteLine($"\nAntal studenter i databasen: {lista.Count}");
    Console.WriteLine("\n--- STUDENTER ---");
    foreach (var s in lista)
    {
        Console.WriteLine($"{s.StudentId}: {s.Fornamn} {s.Efternamn}");
    }
        // Vänta **en gång** efter att alla studenter skrivits ut
        Console.WriteLine("\nTryck Enter för att återgå till menyn...");
        Console.ReadLine();
}

// --- MenyVal 2 ---
static void AntalLararePerAvdelning()
{
    using var context = new GymnasieskolaContext();

    var resultat = context.Personal
        .Where(p => p.Befattning == "Lärare")
        .GroupBy(p => p.Avdelning)
        .Select(g => new
        {
            Avdelning = g.Key,
            Antal = g.Count()
        })
        .ToList();

    Console.WriteLine("\n--- LÄRARE PER AVDELNING ---");
    foreach (var r in resultat)
    {
        Console.WriteLine($"{r.Avdelning}: {r.Antal} lärare");
    }
        Console.WriteLine("\nTryck Enter för att återgå till menyn...");
        Console.ReadLine();
    }

// --- MenyVal 3 ---
static void VisaAktivaKurser()
{
    using var context = new GymnasieskolaContext();

    var kurser = context.Kurs
        .Where(k => k.Aktiv)
        .ToList();

    Console.WriteLine("\n--- AKTIVA KURSER ---");
    foreach (var k in kurser)
    {
        Console.WriteLine($"{k.KursNamn} ({k.Poang}p)");
    }
        Console.WriteLine("\nTryck Enter för att återgå till menyn...");
        Console.ReadLine();
    }

// --- MenyVal 4 ---
static void SattBetyg()
{
    using var context = new GymnasieskolaContext();
    using var transaction = context.Database.BeginTransaction();

    try
    {
        // läsa in från användaren
        Console.WriteLine("\nAnge StudentID: ");
        int studentId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Ange KursID: ");
        int kursId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Ange LarareID: ");
        int larareId = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Ange Betyg (t.ex. A, B, C): ");
        string betygStr = Console.ReadLine()!;

        var betyg = new Betyg
        {
            StudentID = studentId,
            KursID = kursId,
            LarareID = larareId,
            BetygsVarde = betygStr,
            BetygsDatum = DateTime.Now
        };

        context.Betyg.Add(betyg);
        context.SaveChanges();
        transaction.Commit();

        Console.WriteLine("Betyg sparat!");
    }
    catch
    {
        transaction.Rollback();
        Console.WriteLine("Fel, inget sparades.");
    }

    Console.WriteLine("\nTryck Enter för att återgå till menyn...");
    Console.ReadLine();
}

// --- MenyVal 5 ---
static void VisaStudentFullInfo()
{
    using var context = new GymnasieskolaContext();

    var studenter = context.Student
        .Include(s => s.Klass)
        .Include(s => s.Betyg)
        .ThenInclude(b => b.Kurs)
        .ToList();

    Console.WriteLine("\n--- STUDENTER MED BETYG ---");

    foreach (var s in studenter)
    {
        Console.WriteLine($"\n{s.Fornamn} {s.Efternamn}");
        Console.WriteLine($"Klass: {s.Klass.KlassNamn}");

        foreach (var b in s.Betyg)
        {
            Console.WriteLine($"Kurs: {b.Kurs.KursNamn} - Betyg: {b.BetygsVarde}");
        }
    }
    Console.WriteLine("\nTryck Enter för att återgå till menyn...");
    Console.ReadLine();
    }
