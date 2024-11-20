using System;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using System.Linq;
using Randomszamos;

namespace beolvasas
{
    internal class Program
    {
        public delegate void Feladatugyelo(string message);
        public static event Feladatugyelo Felvegezve;                          //Delegált és event definiálása
        static void Main(string[] args)
        {
            Felvegezve += OnFelvegezve;                                        //Feliratkozás az OnFelvegezve-re

            List<Ertekek> szenzorAdatok = new List<Ertekek>();
            for (int i = 0; i < 50; i++)
            {
                Ertekek randomValues = Szenzoros.Randomertekes();
                szenzorAdatok.Add(randomValues);
            
                Console.WriteLine($"Szenzor {i + 1}:");
                Console.WriteLine($"Hőmérséklet: {randomValues.Homerseklet}°C");
                Console.WriteLine($"Páratartalom: {randomValues.Paratartalom}%");
                Console.WriteLine($"Túlfolyó vízszint: {randomValues.TulfolyoVizszint} cm");
                Console.WriteLine($"Állapotjelző: {randomValues.Allapotjell}");
                Console.WriteLine($"Folyó vízszint: {randomValues.Folyovizszint} cm");
                Console.WriteLine(new string('-', 20));
            }



    
                  // JSON fájl generálása
                   string json = JsonConvert.SerializeObject(szenzorAdatok, Newtonsoft.Json.Formatting.Indented);
                   Console.WriteLine("JSON adat:");
                   Console.WriteLine(json);
                    StreamWriter ki = new StreamWriter("adatok_json.txt"); //JSON fájl létrehozása
                    ki.WriteLine(json); //kiírjuk a fájlba
                    ki.Flush(); //puffer ürítése
                    ki.Close();
                    Felvegezve.Invoke("\nElkészült az adatok_json.txt fájl.");
          


                XmlTextWriter writer = new XmlTextWriter("szenzorok.xml", Encoding.UTF8);
                 writer.Formatting = System.Xml.Formatting.Indented; // A behúzásos szerkezethez
                 writer.WriteStartDocument(true);
                 writer.WriteStartElement("SzenzorAdatok");
            
             
            for (int i = 0; i < szenzorAdatok.Count; i++)
            {
                Ertekek adat = szenzorAdatok[i];
                writer.WriteStartElement("Szenzor");

                writer.WriteElementString("Homerseklet", adat.Homerseklet.ToString());
                writer.WriteElementString("Paratartalom", adat.Paratartalom.ToString());
                writer.WriteElementString("TulfolyoVizszint", adat.TulfolyoVizszint.ToString());
                writer.WriteElementString("Allapotjelzo", adat.Allapotjell.ToString());
                writer.WriteElementString("Folyovizszint", adat.Folyovizszint.ToString());

                writer.WriteEndElement(); 
            }

            writer.WriteEndElement(); 
            writer.Flush();
            writer.Close();


            Felvegezve.Invoke("Adatok generálása, JSON és XML fájlba írása befejeződött.");


            var maxho = szenzorAdatok.MaxBy(x => x.Homerseklet);
            Console.WriteLine("\nLinq 1.:");
            Console.WriteLine($"A legmagasabban mért hőmérséklet: {maxho.Homerseklet}°C");           //1.Linq max lekérdezés

            var atlagpara = szenzorAdatok.Average(x => x.Paratartalom);
            Console.WriteLine("\nLinq 2.:");
            Console.WriteLine($"Az átlagos páratartalom: {atlagpara}%");                    //2.Linq átlag pártartalom lekérdezés

            var ertekek = from number in szenzorAdatok
                          orderby number.Homerseklet
                          where number.Allapotjell == 1
                          where number.Homerseklet >= 20 && number.Homerseklet <= 30
                          select number;
            Console.WriteLine("\nLinq 3.:");                                               //3.Linq több feltételes lekérdezés
            foreach (var number in ertekek)
            {
                Console.WriteLine($"Túlfolyó tartályok vízszintje: {number.TulfolyoVizszint}cm, Hőmérséklet: {number.Homerseklet}°C");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
        private static void OnFelvegezve(string message)                                  //Esemény üzenete
        {
            Console.WriteLine($"\n{message}");
        }
    }
}
