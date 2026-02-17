using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GymnasieskolaDb
    {
        public class Betyg
        {
        public int BetygID { get; set; }
        public int StudentID { get; set; }
        public int KursID { get; set; }
        public int LarareID { get; set; }
        [Column("Betyg")] 
        public string BetygsVarde{ get; set; } = null!;
        public DateTime BetygsDatum { get; set; }

        // Lägg till dessa navigation properties:
        public Student Student { get; set; } = null!;
        public Kurs Kurs { get; set; } = null!;
        public Personal Larare { get; set; } = null!;
    }
}
