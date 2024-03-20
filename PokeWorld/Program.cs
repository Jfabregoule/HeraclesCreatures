using HeraclesCreatures.FilesReaders;
using HeraclesCreatures.Map;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        FileManager coubeh = new FileManager();
        coubeh.CreateIDToFile();

        string[] map = coubeh.ReadFile("Ressources/Scenes/Scene1.txt");
        

    }
}
