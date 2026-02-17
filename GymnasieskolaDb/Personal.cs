using System;
using System.Collections.Generic;
using System.Text;

namespace GymnasieskolaDb
{
    public class Personal
    {
        public int PersonalID { get; set; }
        public string Fornamn { get; set; } = null!;
        public string Efternamn { get; set; } = null!;
        public string Personnummer { get; set; } = null!;
        public string Befattning { get; set; } = null!;
        public string Avdelning { get; set; } = null!;
        public int Lon { get; set; }
        public DateTime AnstallningsDatum { get; set; }

        //Navigation properties
        public List<Betyg> SattaBetyg { get; set; } = new();
    }
}

