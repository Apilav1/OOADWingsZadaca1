using System;
using System.Collections.Generic;
using System.Text;


namespace OOADWingLibrary
{
    public abstract class Klijent
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string jib;
        public string Jib
        {
            get
            {
                return jib;
            }
            set
            {
                if (value.Length == 6)
                    jib = value;
                else
                    throw new Exception("nevalidan jib klijenta");
            }
        }
    }
}
