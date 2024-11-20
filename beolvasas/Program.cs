using Randomszamos;
using Newtonsoft.Json;

namespace beolvasas
{
    internal class Program
    {
        static void Main(string[] args)
        {
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


            Console.ReadKey();
        }
    }
}