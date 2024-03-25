using HeraclesCreatures;

namespace HeraclesCreatures
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*
            GameManager gameManager = new GameManager();

            gameManager.GameLoop();
            Console.WriteLine();
            */
            void PrintDictionary(Dictionary<string, List<string[]>> dictionary)
            {
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"Key: {kvp.Key}");

                    foreach (var item in kvp.Value)
                    {
                        foreach (var value in item)
                        {
                            Console.Write($" {value}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                }
            }
            FileManager shoukran = new FileManager();
            Dictionary<string, List<string[]>> mabrouk = shoukran.GetRessources();
            PrintDictionary(mabrouk);

        }

    }
}