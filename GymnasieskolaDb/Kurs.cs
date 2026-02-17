using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieskolaDb
{
    public class Kurs
    {
        public int KursID { get; set; }
        public string KursNamn { get; set; } = null!;
        public int Poang { get; set; }
        public bool Aktiv { get; set; }

        // Navigation properties 
        public List<Betyg> Betyg { get; set; } = new();
    }
}
