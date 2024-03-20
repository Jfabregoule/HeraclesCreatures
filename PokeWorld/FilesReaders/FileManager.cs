using HeraclesCreatures.Map;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.FilesReaders
{
    internal class FileManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Dictionary<string, string> _idToFile = new Dictionary<string, string>();

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public Dictionary<string, string> IdToFile { get => _idToFile; set => _idToFile = value; }

        #endregion Properties

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Events                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Events



        #endregion Events

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Methods                                           |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public string[] ReadFile(string filePath)
        {
            try
            {
                string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
                string relativePath = $@"{filePath}";
                string mapObjectFilePath = Path.Combine(solutionDirectory, relativePath);
                if (File.Exists(mapObjectFilePath))
                {
                    string[] mapObjectLines = File.ReadAllLines(mapObjectFilePath);
                    return mapObjectLines;
                }
                else
                {
                    Console.WriteLine("File not found.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return null;
            }
        }

        public void CreateIDToFile()
        {
            string[] fileIdentifiersLines = ReadFile("FilesReaders/Datas/FileIDs.txt");
            if (fileIdentifiersLines != null)
            {
                for (int i = 0; i < fileIdentifiersLines.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        _idToFile[fileIdentifiersLines[i]] = fileIdentifiersLines[i + 1];
                    }
                }
            }
            else
            {
                Console.WriteLine("Error occurred while reading the file.");
            }
        }

        bool GetWalkable(string[] mapObjectLines, int index)
        {
            return bool.Parse(mapObjectLines[index + 1]);
        }

        char[,] GetDrawing(string[] mapObjectLines, int index)
        {
            char[,] mapObjectDrawing = new char[5, 10];

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    mapObjectDrawing[j, k] = mapObjectLines[index + j + 1][k];
                }
            }

            return mapObjectDrawing;
        }

        ConsoleColor[,] GetColor(string[] mapObjectLines, int index)
        {
            ConsoleColor[,] mapObjectColor = new ConsoleColor[5, 10];
            string colorCode = "";
            int counter;
            for (int j = 0; j < 5; j++)
            {
                counter = 0;
                foreach (char k in mapObjectLines[index + j + 1])
                {
                    if (k != ',')
                    {
                        colorCode += k;
                    }
                    else
                    {
                        mapObjectColor[j, counter] = (ConsoleColor)int.Parse(colorCode);
                        colorCode = "";
                        counter++;
                    }
                }
            }

            return mapObjectColor;
        }

        public CellData_ GetMapObjectData(string filePath)
        {

            CellData_ cellData = new CellData_();
            cellData.CellContent = new MapObject();

            string[] mapObjectLines = ReadFile(filePath);
            if (mapObjectLines != null)
            {
                for (int i = 0; i < mapObjectLines.Length; i++)
                {
                    /*
                    // Map Object
                    if (mapObjectLines[i] == "Type:")
                    {
                        object mapObject = CreateInstanceFromString(mapObjectLines[i + 1]);
                        switch (mapObjectLines[i + 1]) {
                            case "Chest":
                                break;
                            case "Ground":
                                break;
                            case "Wall":
                                break;
                        }
                        cellData.CellContent = mapObject;
                    }
                    */

                    // Walkable
                    if (mapObjectLines[i] == "Walkable:")
                    {
                        cellData.IsWalkable = GetWalkable(mapObjectLines, i);
                    }

                    // Drawing
                    else if (mapObjectLines[i] == "Drawing:")
                    {
                        cellData.Drawing = GetDrawing(mapObjectLines, i);
                    }

                    // Foreground Color
                    else if (mapObjectLines[i] == "FGColor:")
                    {
                        cellData.ForegroundColor = GetColor(mapObjectLines, i);
                    }

                    // Background Color
                    else if (mapObjectLines[i] == "BGColor:")
                    {
                        cellData.BackgroundColor = GetColor(mapObjectLines, i);
                    }
                }

                return cellData;
            }
            else
            {
                Console.WriteLine("Error occurred while reading the file.");
                return new CellData_();
            }
            
            
        }

        public Cell[,] GetMapCellArray(string filePath)
        {
            


            
        }

        #endregion Methods

    }
}
