using System;
using System.Collections.Generic;
using System.Text;
using OOADWingLibrary;

namespace OOADWings
{
    class TeretniAvion : IAvion
    {
        private int _kapacitet;

        public override int kapacitet
        {
            get { return _kapacitet; }
            set { _kapacitet = value; }
        }

    
        public override void ispisi()
        {
            base.ispisi();
            Console.WriteLine("Kapacitet: {0} ", kapacitet);      
        }

        private int cijena = 350;
        private int FiksnaCijena = 0;

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

        public override string[] Drzave
        {
            get { return null; }
            set { }
        }
    }
}
