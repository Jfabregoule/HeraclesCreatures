using HeraclesCreatures;
using System;

namespace HeraclesCreatures
{

    class Program
    {
        public static void Main(string[] args)
        {

            //FileManager shoukran = new FileManager();
            //shoukran.FillAllDictionnaries();
            //Dictionary<string, Scene> scenes = shoukran.ScenesData;
            //scenes["FirstScene"].DisplayScene();
            

            GameManager gameManager = new GameManager();
            
            Console.WriteLine();
           
            //static void DisplayDictionary<T>(Dictionary<string, T> dictionary)
            //{
            //    Console.WriteLine($"Dictionary<{typeof(string).Name}, {typeof(T).Name}>:");
            //    foreach (var kvp in dictionary)
            //    {
            //        Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            //    }
            //    Console.WriteLine();
            //}
            gameManager.GameLoop();

        }
    }
}