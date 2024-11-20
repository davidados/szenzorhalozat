using Randomszamos;
using Newtonsoft.Json;

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
            string json = JsonConvert.SerializeObject(szenzor, Formatting.Indented);
            Console.WriteLine(json);

            Felvegezve.Invoke("A Json konvertálás sikeresen megtörtént!");                //Esemény meghívása

            Console.ReadKey();
        }
        private static void OnFelvegezve(string message)                                  //Esemény üzenete
        {
            Console.WriteLine($"\n{message}");
        }
    }
}