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
           
            static void DisplayDictionary<T>(Dictionary<string, T> dictionary)
            {
                Console.WriteLine($"Dictionary<{typeof(string).Name}, {typeof(T).Name}>:");
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
                }
                Console.WriteLine();
            }
            */
            FileManager shoukran = new FileManager();
            shoukran.FillAllDictionnaries();
            shoukran.DisplayDictionaries();

        }

    }
}