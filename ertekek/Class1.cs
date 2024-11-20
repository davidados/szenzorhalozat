namespace Randomszamos
{
    public class Ertekek
    {
        public int Homerseklet { get; set; }
        public int Paratartalom { get; set; }
        public int TulfolyoVizszint { get; set; }
        public int Allapotjell { get; set; }
        public int Folyovizszint { get; set; }

    }
    public static class Szenzoros
    {
        public static Ertekek Randomertekes()
        {
            Random random = new Random();

            return new Ertekek
            {
                Homerseklet = random.Next(-5, 40),
                Paratartalom = random.Next(0, 100),
                TulfolyoVizszint = random.Next(0, 150),
                Allapotjell = random.Next(0, 5),
                Folyovizszint = random.Next(0, 100)
            };
        }
    }
}
