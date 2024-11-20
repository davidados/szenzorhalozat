﻿using System;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
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

            Ertekek randomValues = Szenzoros.Randomertekes();

            Console.WriteLine($"Hőmérséklet: {randomValues.Homerseklet}°C");
            Console.WriteLine($"Páratartalom: {randomValues.Paratartalom}%");
            Console.WriteLine($"Túlfolyó vízszint: {randomValues.TulfolyoVizszint} cm");
            Console.WriteLine($"Állapotjelző: {randomValues.Allapotjell}");
            Console.WriteLine($"Folyó vízszint: {randomValues.Folyovizszint} cm");


            var szenzor = new Ertekek
            {
                Homerseklet = randomValues.Homerseklet,
                Paratartalom = randomValues.Paratartalom,
                TulfolyoVizszint = randomValues.TulfolyoVizszint,
                Allapotjell = randomValues.Allapotjell,
                Folyovizszint = randomValues.Folyovizszint
            };
            string json = JsonConvert.SerializeObject(szenzor, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(json);

            Felvegezve.Invoke("A Json konvertálás sikeresen megtörtént!");                //Esemény meghívása


            XmlTextWriter writer = new XmlTextWriter("szenzorok.xml", Encoding.UTF8);
             writer.Formatting = System.Xml.Formatting.Indented; // A behúzásos szerkezethez
             writer.WriteStartDocument(true);
             writer.WriteStartElement("SzenzorAdatok");
            
             writer.WriteStartElement("Szenzor");
             writer.WriteElementString("Homerseklet", randomValues.Homerseklet.ToString());
             writer.WriteElementString("Paratartalom", randomValues.Paratartalom.ToString());
             writer.WriteElementString("TulfolyoVizszint", randomValues.TulfolyoVizszint.ToString());
             writer.WriteElementString("Allapotjelzo", randomValues.Allapotjell.ToString());
             writer.WriteElementString("Folyovizszint", randomValues.Folyovizszint.ToString());
             writer.WriteEndElement();
            
             writer.WriteEndElement(); // SzenzorAdatok lezárása
             writer.Flush();
             writer.Close();
            
             Console.WriteLine("Adatok XML fájlba írása befejeződött.");

            

            Console.ReadKey();
        }
        private static void OnFelvegezve(string message)                                  //Esemény üzenete
        {
            Console.WriteLine($"\n{message}");
        }
    }
}
