using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using OOADWingLibrary;

namespace OOADWings
{
    abstract class IAvion
    {
        private string _vrsta;

        public string Vrsta
        {
            get { return _vrsta; }
            set { _vrsta = value; }
        }

        private int _brojSjedista;
        public int BrojSjedista
        {
            get { return _brojSjedista; }
            set { _brojSjedista = value; }
        }

        private string _ib;
        public abstract int Cijena { get; set; }
        public abstract int FTrosak { get; set; }
        public abstract int kapacitet { get; set; }
        public abstract string[] Drzave { get; set; }

        public virtual void ispisi()
        {
            Console.WriteLine($"Vrsta:{Vrsta}, Broj sjedista:{BrojSjedista}," +
                $" Identifikacijski Broj:{IdentifikacijskiBroj}");
        }

        public string DajIdBr()
        {
            return IdentifikacijskiBroj;
        }

        public string IdentifikacijskiBroj
        {
            get
            {
                return _ib;
            }
            set
            {
                if (value.Length == 9 && value.All(Char.IsLetterOrDigit))
                    _ib = value;
                else
                    throw new Exception("nepravilan identifikacioni broj aviona!");
            }
        }
    }
}
