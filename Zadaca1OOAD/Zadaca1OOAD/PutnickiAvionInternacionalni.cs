using System;
using System.Collections.Generic;
using System.Text;
using OOADWingLibrary;

namespace OOADWings
{
    class PutnickiAvionInternacionalni : IAvion
    {
        private string[] drzave = new string[] { };

        public override string[] Drzave
        {
            get { return drzave; }
            set { drzave = value; }
        }

        public int Duzina
        {
            get { return drzave.Length; }
        }
        
        public string this[int index]
        {
            get
            {
                return drzave[index];
            }
            set
            {
                drzave[index] = value;
            }
        }

        public override void ispisi()
        {
            base.ispisi();
            Console.WriteLine("Lista podrzanih zemalja:");

            foreach (string s in drzave)
                Console.Write(s + " ");

            Console.WriteLine();
        }

        private int cijena = 200;
        private int FiksnaCijena = 1000;

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
    }
}
