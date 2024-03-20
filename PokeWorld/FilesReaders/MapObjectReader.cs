using HeraclesCreatures.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.FilesReaders
{
    internal class MapObjectReader
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields



        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties



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

        string[] ReadFile(string filePath)
        {
            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string relativePath = $@"Map\MapObjects\{filePath}";
            string mapObjectFilePath = Path.Combine(solutionDirectory, relativePath);
            string[] mapObjectLines = File.ReadAllLines(mapObjectFilePath);
            return mapObjectLines;
        }

        static object CreateInstanceFromString(string typeName, params object[] args)
        {
            Type type = Type.GetType(typeName);
            if (type != null)
            {
                return Activator.CreateInstance(type, args);
            }
            return null;
        }

        bool GetWalkable(string[] mapObjectLines, int index)
        {
            return bool.Parse(mapObjectLines[index + 1]);
        }

        char[,] GetDrawing(string[] mapObjectLines, int index)
        {
            char[,] mapObjectDrawing = new char[5, 5];

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    mapObjectDrawing[j, k] = mapObjectLines[index + j + 1][k];
                }
            }

            return mapObjectDrawing;
        }

        ConsoleColor[,] GetColor(string[] mapObjectLines, int index)
        {
            ConsoleColor[,] mapObjectColor = new ConsoleColor[5, 5];
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

        public CellData_ GetMapObjectCellData(string file)
        {

            CellData_ cellData = new CellData_();
            cellData.CellContent = new MapObject();
            string[] mapObjectLines = ReadFile(file);
            
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

        #endregion Methods

    }
}
