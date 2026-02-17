using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieskolaDb
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Fornamn { get; set; } = null!;
        public string Efternamn { get; set; } = null!;
        public string Personnummer { get; set; } = string.Empty;
        public int KlassID { get; set; }

        // Navigation propreties
        public Klass Klass { get; set; } = null!;
        public List<Betyg> Betyg { get; set; } = new();
    }
}

