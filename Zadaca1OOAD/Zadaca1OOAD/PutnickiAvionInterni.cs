using System;
using System.Collections.Generic;
using System.Text;
using OOADWingLibrary;

namespace OOADWings
{
    class PutnickiAvionInterni : IAvion
    {
        private int cijena = 120;
        private int FiksnaCijena = 500;

        public override int Cijena
        {
            get { return cijena; }
            set { }
        }

        public override int FTrosak
        {
            get { return FiksnaCijena; }
            set { }
        }

        public override int kapacitet
        {
            get { return 0; }
            set { }
        }
        public override string[] Drzave
        {
            get { return null; }
            set { }
        }
    }
}
