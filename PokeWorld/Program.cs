using HeraclesCreatures.FilesReaders;
using HeraclesCreatures.Map;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        MapObjectReader coubeh = new MapObjectReader();
        CellData_ apagnan = coubeh.GetMapObjectCellData("Ressources/MapObjects/Chest1.txt");
        apagnan.printCellData();

    }
}
