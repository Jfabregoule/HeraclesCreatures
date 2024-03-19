using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {
            // Get the current directory (solution directory)
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            // Specify the relative path to the Map.txt file
            string relativePath = @"Maps\Map.txt";

            // Combine the solution directory path with the relative path
            string mapFilePath = Path.Combine(solutionDirectory, relativePath);

            // Read all lines from the Map.txt file
            string[] mapLines = File.ReadAllLines(mapFilePath);

            // Output the content to the console
            Console.WriteLine("Content of Map.txt:");

            // Output each line to the console and store it in the array
            foreach (string line in mapLines)
            {
                Console.WriteLine(line);
            }
        }
        catch (FileNotFoundException)
        {
            // Handle the case where the file is not found
            Console.WriteLine("Map.txt file not found.");
        }
        catch (IOException ex)
        {
            // Handle other IO exceptions
            Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
        }
    }
}
