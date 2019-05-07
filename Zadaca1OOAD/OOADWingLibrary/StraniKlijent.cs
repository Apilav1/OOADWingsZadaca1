using System;
using System.Collections.Generic;
using System.Text;

namespace OOADWingLibrary
{
    public class StraniKlijent : Klijent
    {
        Drzava drzava;

        public Drzava Drzava
        {
            get { return drzava; }
            set { drzava = value; }
        }
    }
}
