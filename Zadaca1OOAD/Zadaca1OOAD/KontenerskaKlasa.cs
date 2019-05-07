using System;
using System.Collections.Generic;
using System.Text;
using OOADWingLibrary;

namespace OOADWings
{
    public struct Najam
    {
        public string JibA;
        public DateTime DatumIznajmljivanja;
        public DateTime DatumIstekaNajama;
        public string Jib;
        public int kaucija;
        public int UkupniTrosak;
    }

    class KontenerskaKlasa : Ipretraga
    {
        List<string> zajednickeObavijesti = new List<string>();
        List<Najam> iznajmljivanja = new List<Najam>();

        List<IAvion> Avioni = new List<IAvion>
            {
                 new PutnickiAvionInterni()
            {
                Vrsta = "Airbus",
                BrojSjedista = 122,
                IdentifikacijskiBroj = "abcdk1234"
            },
                new PutnickiAvionInterni()
            {
                Vrsta = "Boeing",
                BrojSjedista = 90,
                IdentifikacijskiBroj = "ab122c224"
            },
            new PutnickiAvionInterni()
            {
                Vrsta = "Cessna",
                BrojSjedista = 222,
                IdentifikacijskiBroj = "akjsl3333"
            },
            new PutnickiAvionInternacionalni()
            {
                Vrsta = "Cessna",
                BrojSjedista = 222,
                IdentifikacijskiBroj = "akjsl3343",
                Drzave = new string[] {"BIH", "Turkey", "Qatar"}
            },
            new PutnickiAvionInternacionalni()
            {
                Vrsta = "Boeing",
                BrojSjedista = 150,
                IdentifikacijskiBroj = "akjsl33jh",
                Drzave = new string[] { "BIH", "Sweden", "Berlin" }
            }
            };


         List<Klijent> klijenti = new List<Klijent>{
                new DomaciKlijent()
                {
                    Ime = "Adi",
                    Prezime = "Pilav",
                    DatumRodjenja = new DateTime(1996, 10, 21),
                    Jib = "21109A"
                }
            };

        public List<IAvion> PretragaPoAtributima(IAvion putnickiAvion)
        {
            List<IAvion> avions = new List<IAvion>();
            foreach (IAvion i in Avioni)
                if (i.Equals(putnickiAvion))
                    avions.Add(i);
            return avions;
        }

        
        public IAvion PretragaPrekoJibAviona(string JibA)
        {
            foreach(IAvion i in Avioni)
            {
                if (i.IdentifikacijskiBroj.Equals(JibA))
                    return i;
            }
            return null;
        }

        public Boolean ProvjeraKlijenta(string id)
        {
            foreach(Klijent k in klijenti)
                if (k.Jib.Equals(id))
                    return true;
            return false;
        }
        
        public Klijent nadjiKlijenta(string jib)
        {
            foreach (Klijent k in klijenti)
                if (k.Jib.Equals(jib))
                    return k;
            return null;
        }
        public void staviObavijestNaStack(string poruka, string sifraKlijenta
                                            ,string datum, string vrijemeObavijesti)
        {
            zajednickeObavijesti.Add(poruka + " " + sifraKlijenta + " " +
                                                datum + " " + vrijemeObavijesti);
        }

        public void dodajNajam(string jibA, DateTime date1, DateTime date2,
                                                string jib, int k, int trosak)
        {
            iznajmljivanja.Add(new Najam() {JibA = jibA, DatumIznajmljivanja = date1,
                                            DatumIstekaNajama = date2, Jib = jib,
                                            kaucija = k, UkupniTrosak = trosak});
        }

        public void dodajKlijenta(Klijent k)
        {
            klijenti.Add(k);
        }

        public void dodajVozilo(IAvion a)
        {
            Avioni.Add(a);
        }

        public void ispisiZajednickeObavijesti()
        {
            foreach(string s in zajednickeObavijesti)
            {
                Console.WriteLine(s);
            }
        }

        public int PovratAviona(string jib)
        {
            int trosak = -1;

            for(int k=0; k < iznajmljivanja.Capacity; k++)
            {
                if (iznajmljivanja[k].Jib.Equals(jib))
                {
                    trosak = iznajmljivanja[k].UkupniTrosak;
                    iznajmljivanja.RemoveAt(k);
                    break;
                }
            }
            return trosak;
        }
    }
}
