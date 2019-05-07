using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using OOADWingLibrary;

namespace OOADWings
{
    class Program
    {
        public delegate void Obavijesti();


        public static void IspisiPoruku()
        {
            Console.WriteLine("Pretraga nema rezultata");
        }

        public static int IzborA(List<IAvion> avions)
        {
            Console.WriteLine("Prikaz rezultata: ");
            
            foreach(IAvion a in avions)
            {
                a.ispisi();
            }

            Console.WriteLine("Pritisnite redni broj aviona koji zelite iznajmiti: ");

            int i = Int32.Parse(Console.ReadLine());

            if (i > avions.Capacity)
                throw new Exception("Izbor nepostojeceg aviona");

            return i;
        }

        public static int  IspisiMeni()
        {
            Console.Clear();
            Console.WriteLine("_________________________________________________");
            Console.WriteLine("Unesite redni broj opcije koju zelite: ");
            Console.WriteLine("1. unos vozila");
            Console.WriteLine("2. unos klijenta");
            Console.WriteLine("3. iznajmljivanje");
            Console.WriteLine("4. povrat aviona i placanje");
            Console.WriteLine("5. ispis liste obavijesti");
            Console.WriteLine("6. za kraj");
            Console.WriteLine("_________________________________________________");
            Console.WriteLine("Vas izbor: ");
            return Int32.Parse(Console.ReadLine());
        }
        
        
        public static void IznajmljivanjeAviona(ref Obavijesti obavijest, ref KontenerskaKlasa kontenerskaKlasa)
        {
            Boolean Izbor = false;

            string jib;
            Console.WriteLine("Unesite indentifikacioni broj:");
            jib = Console.ReadLine();
            

            if (kontenerskaKlasa.ProvjeraKlijenta(jib))
            {
                Console.WriteLine("Unesite podatke o avionu koji zelite iznajmiti " +
                    "(za pretragu po indentifikacionom broju " +
                    "aviona pritisnite 1, odnosno za pretragu" +
                    "po atributima pritisnite 2):");
                int izbor = Int32.Parse(Console.ReadLine());
                List<IAvion> rez = new List<IAvion>();

                if (izbor == 1)
                {
                    //pretraga po jibA
                    Console.WriteLine("Unesite indetifikacioni broj aviona:");
                    string jibA = Console.ReadLine();

                    rez.Add(kontenerskaKlasa.PretragaPrekoJibAviona(jibA));

                    if (rez[0].Equals(null))
                    {
                        //nije pronadjen trazeni avion
                        obavijest = new Obavijesti(IspisiPoruku);
                        obavijest();

                    }
                    else
                    {
                        Izbor = true;
                    }

                }
                else if (izbor == 2)
                {
                    Console.WriteLine("Da li zelite iznajmiti 1.avion za let unutar zemlje, " +
                        "2.avion za internacionalne letove, 3.teretni avion" +
                        " (pritisnite odgovarajuci broj):");
                    int iz = Int32.Parse(Console.ReadLine());
                    IAvion av = new TeretniAvion();
                    string[] zemlje = new string[] { };

                    if (iz == 1)
                    {
                        av = new PutnickiAvionInterni();
                    }
                    else if (iz == 2)
                    {
                        av = new PutnickiAvionInternacionalni();
                        Console.WriteLine("Unesite zemlje (nakon unosa svake pritisnite enter, K za kraj: ");
                        string s;
                        string[] drzave = new string[] { };
                        int k = 0;

                        while ((s = Console.ReadLine()) != "K")
                        {
                            drzave[k++] = s;
                        }

                        av.Drzave = drzave;
                    }
                    else if (iz == 3)
                    {
                        av = new TeretniAvion();

                        Console.WriteLine("unesite zeljeni kapacitet:");
                        av.kapacitet = Int32.Parse(Console.ReadLine());
                    }
                    else
                    {
                        Console.WriteLine("Pogresan unos");
                    }

                    Console.WriteLine("unesite vrstu aviona kojeg zelite iznajmiti:");
                    av.Vrsta = Console.ReadLine();

                    Console.WriteLine("unesite broj sjedista aviona: ");
                    av.BrojSjedista = Int32.Parse(Console.ReadLine());

                    rez.AddRange(kontenerskaKlasa.PretragaPoAtributima(av));
                }
                else
                {
                    Console.WriteLine("Nepravilan izbor");
                    Console.Read();
                }

                if (Izbor)
                {
                    CultureInfo MyCultureInfo = new CultureInfo("de-DE");

                    Console.WriteLine("Unesite datum kraja najama (u formatu primjera 12 Juni 2008 : ");
                    DateTime DatumNajama = DateTime.Parse(Console.ReadLine(), MyCultureInfo);

                    Console.WriteLine("Unesite datum kraja najama (u formatu primjera 12 Juni 2008 : ");
                    DateTime DatumKrajaNajama = DateTime.Parse(Console.ReadLine(), MyCultureInfo);

                    Klijent k = kontenerskaKlasa.nadjiKlijenta(jib);

                    int izabrani = IzborA(rez);
                    int kaucija = 50;
                    int UkupanBrojKilograma = 0;

                    if (rez[izabrani].GetType().Equals(typeof(TeretniAvion)))
                    {
                        Console.WriteLine("Unesite ukupan broj kilograma koji se prenosi:");
                        UkupanBrojKilograma = Int32.Parse(Console.ReadLine());
                    }


                    if (k.GetType().Equals(typeof(StraniKlijent)))
                        kaucija = 100;

                    int trosak = ((DatumKrajaNajama - DatumNajama).Days) * rez[izabrani].Cijena;

                    if (DatumNajama.Equals(DayOfWeek.Saturday) ||
                                       DatumNajama.Equals(DayOfWeek.Sunday))
                        trosak += rez[izabrani].FTrosak;

                    double ukB = (Double)UkupanBrojKilograma;
                    ukB *= 0.02;
                    trosak += Convert.ToInt32(ukB);

                    if (trosak >= kaucija)
                        trosak -= kaucija;
                    else
                        trosak = 0;

                    kontenerskaKlasa.dodajNajam(rez[izabrani].DajIdBr(), DatumNajama,
                                                   DatumKrajaNajama, jib, kaucija, trosak);
                    Console.WriteLine("Ukupni trosak je: {0}", trosak);

                    kontenerskaKlasa.staviObavijestNaStack("iznajmljen"+ rez[izabrani].DajIdBr(),
                                            jib, DatumNajama.ToString(), DateTime.Now.ToString());
                }

            }
            else
            {
                Console.WriteLine("Klijent sa ovim indentifikacionim brojem ne postoji!");
                Console.Read();
            }
        }

        static void Main(string[] args)
        {
            KontenerskaKlasa kontenerskaKlasa = new KontenerskaKlasa();
            Obavijesti obavijest = new Obavijesti(kontenerskaKlasa.ispisiZajednickeObavijesti);
            
            
            int t;
          while ((t = IspisiMeni()) != 6) {
            
            if (t == 1)
            {
                    //unos vozila

                    IAvion noviAvion;
                    Console.WriteLine("1.Interni ili 2.Internacionalni ili " +
                                      " 3. teretni(unesi redni broj):");
                    int izborAviona = Int32.Parse(Console.ReadLine());

                    if (izborAviona == 1)
                        noviAvion = new PutnickiAvionInterni();
                    else if (izborAviona == 2)
                        noviAvion = new PutnickiAvionInternacionalni();
                    else
                        noviAvion = new TeretniAvion();

                    Console.WriteLine("unesite vrstu");
                    noviAvion.Vrsta = Console.ReadLine();

                    Console.WriteLine("unesite broj sjedista: ");
                    noviAvion.BrojSjedista = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("unesite indentifikacioni broj: ");
                    string jibA2 = Console.ReadLine();

                    Console.WriteLine("Unesite cijenu za izdavanje aviona: ");
                    noviAvion.Cijena = Int32.Parse(Console.ReadLine());

                    if(izborAviona == 1 || izborAviona == 2)
                    {
                        Console.WriteLine("Unesite fiksni trosak za izdavanje aviona: ");
                        noviAvion.FTrosak = Int32.Parse(Console.ReadLine());
                    }

                    if(izborAviona == 3)
                    {
                        Console.WriteLine("Unesite kapacitet: ");
                        noviAvion.kapacitet = Int32.Parse(Console.ReadLine());
                    }

                    if(izborAviona == 2)
                    {
                        string drzava;
                        string[] drzave = new string[] { };
                        int l = 0;

                        Console.WriteLine("Unesite podrzave drzave (jednu po jednu, 'kraj' za kraj unos)");

                        while((drzava = Console.ReadLine()) != "kraj")
                        {
                            drzave[l++] = drzava;
                        }

                        noviAvion.Drzave = drzave;
                    }

                    if (jibA2.Length == 9 && jibA2.All(Char.IsLetterOrDigit))
                    {
                        noviAvion.IdentifikacijskiBroj = jibA2;
                        kontenerskaKlasa.dodajVozilo(noviAvion);
                    }
                    else
                    {
                        Console.WriteLine("Neispravn id aviona, pokusajte opet!");
                    }

                    System.Threading.Thread.Sleep(1000);
            }
            else if (t == 2)
            {
                    //unos klijenta

                    Klijent noviKlijent;
                    Console.WriteLine("1.Strani ili 2.Domaci klijent(unesi redni broj):");
                    if (Int32.Parse(Console.ReadLine()) == 1)
                        noviKlijent = new StraniKlijent();
                    else
                        noviKlijent = new DomaciKlijent();

                    Console.WriteLine("Unesite ime");
                    noviKlijent.Ime = Console.ReadLine();
                    Console.WriteLine("Unesite prezime");
                    noviKlijent.Prezime = Console.ReadLine();
                    Console.WriteLine("Unesite vlastiti identifikacioni broj:");
                    string jib2 = Console.ReadLine();

                    if (jib2.Length > 6)
                    {
                         Console.WriteLine("Neispravan format pokusajte opet!");
                    }
                    else
                        kontenerskaKlasa.dodajKlijenta(noviKlijent);

                    System.Threading.Thread.Sleep(1000);

            }
            else if (t == 3)
            {
                    //iznamljivanje aviona 

                    IznajmljivanjeAviona(ref obavijest, ref kontenerskaKlasa);
                    System.Threading.Thread.Sleep(1000);
            }
            else if(t == 4)
            {
                    //povrat aviona i placanje

                    Console.WriteLine("Unesite svoj identifikacioni broj:");
                    string jib1 = Console.ReadLine();

                    if (kontenerskaKlasa.ProvjeraKlijenta(jib1))
                    {
                        if (kontenerskaKlasa.PovratAviona(jib1) == -1)
                            Console.WriteLine("Na vase ime nije iznajmljen avion");
                        else
                            Console.WriteLine("Vas ukupan trosak: {0}", kontenerskaKlasa.PovratAviona(jib1));

                    }
                    else
                    {
                        Console.WriteLine("Pogresan identifikacioni broj, pokusajte opet");
                    }
                    System.Threading.Thread.Sleep(1000);
            }
            else
            {
                    //obavijesti 

                    obavijest = new Obavijesti(kontenerskaKlasa.ispisiZajednickeObavijesti);
                    obavijest();
                    System.Threading.Thread.Sleep(1000);
            }
          }
        }
    }
}
