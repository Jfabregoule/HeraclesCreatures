using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;

namespace HeraclesCreatures
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

        Dictionary<string, CreatureStats> _creaturesData;
        Dictionary<string, TileData> _tilesData;
        Dictionary<string, DoorData> _doorsData;
        Dictionary<string, ChestData> _chestsData;
        Dictionary<string, CharacterData> _charactersData;
        Dictionary<string, Scene> _scenes;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public Dictionary<string, CreatureStats> CreaturesData { get => _creaturesData; set => _creaturesData = value; }
        public Dictionary<string, TileData> TilesData { get => _tilesData; set => _tilesData = value; }
        internal Dictionary<string, DoorData> DoorsData { get => _doorsData; set => _doorsData = value; }
        internal Dictionary<string, ChestData> ChestsData { get => _chestsData; set => _chestsData = value; }
        internal Dictionary<string, CharacterData> CharactersData { get => _charactersData; set => _charactersData = value; }
        public Dictionary<string, Scene> ScenesData { get => _scenes; set => _scenes = value; }

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

        public FileManager()
        {
            _creaturesData = new();
            _tilesData = new();
            _doorsData = new();
            _chestsData = new();
            _charactersData = new();
            _scenes = new();
        }

        private List<string> GetFolderBranches(string folderPath)
        {
            List<string> branches = new List<string>();

            string[] subdirectories = Directory.GetDirectories(folderPath);

            branches.Add(folderPath);

            foreach (string subdirectory in subdirectories)
            {
                branches.AddRange(GetFolderBranches(subdirectory));
            }

            return branches;
        }

        private Dictionary<string, List<string[]>> GetRessources()
        {
            string GetDictionnaryID(string fullPath)
            {
                const string resourcesIdentifier = "Resources";

                char[] separators = { '\\' };
                string[] parts = fullPath.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                int index = Array.IndexOf(parts, resourcesIdentifier);
                if (index >= 0 && index < parts.Length - 1)
                {
                    return parts[index + 1];
                }
                return null;
            }

            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string relativePath = "Resources";
            string rootFolderPath = Path.Combine(solutionDirectory, relativePath);

            List<string> branches = GetFolderBranches(rootFolderPath);
            Dictionary<string, List<string[]>> resources = new Dictionary<string, List<string[]>> {};

            foreach (string branch in branches)
            {
                string[] files = Directory.GetFiles(branch, "*.txt");
                foreach (string filePath in files)
                {
                    try
                    {
                        if (File.Exists(filePath))
                        {
                            string[] resource = File.ReadAllLines(filePath);
                            string mapID = GetDictionnaryID(branch);
                            if (resources.ContainsKey(mapID))
                            {
                                resources[mapID].Add(resource);
                            }
                            else
                            {
                                resources.Add(mapID, new List<string[]> { resource });
                            }
                        }
                        else
                        {
                            Console.WriteLine("File not found.");
                            return resources;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred while reading the files: {ex.Message}");
                        return resources;
                    }
                }

            }
            return resources;
        }

        private bool GetBool(string line)
        {
            return bool.Parse(line);
        }

        private int GetInt(string line)
        {
            return int.Parse(line);
        }

        private char[,] GetDrawing(string[] drawingLines, int index)
        {
            char[,] drawing = new char[5, 10];

            for (int j = 0; j < 5; j++)
            {
                for (int k = 0; k < 10; k++)
                {
                    drawing[j, k] = drawingLines[index + j + 1][k];
                }
            }

            return drawing;
        }

        private ConsoleColor[,] GetColor(string[] colorLines, int index)
        {
            ConsoleColor[,] color = new ConsoleColor[5, 10];
            string colorCode = "";
            int counter;
            for (int j = 0; j < 5; j++)
            {
                counter = 0;
                foreach (char k in colorLines[index + j + 1])
                {
                    if (k != ',')
                    {
                        colorCode += k;
                    }
                    else
                    {
                        color[j, counter] = (ConsoleColor)int.Parse(colorCode);
                        colorCode = "";
                        counter++;
                    }
                }
            }

            return color;
        }

        private Dictionary<string, TileData> TilesIDs(string[] sceneLines, int index)
        {
            Dictionary<string, TileData> tilesIDs = new();
            string separator = " = ";

            while (sceneLines[index + 1] != "")
            {
                string[] parts = sceneLines[index + 1].Split(new[] { separator }, StringSplitOptions.None);
                tilesIDs.Add(parts[0].Trim(), _tilesData[parts[1].Trim()]);
                index++;
            }

            return tilesIDs;
        }

        private int[] GetWidthHeight(string[] sceneLines, int index)
        {
            int[] widthHeight = new int[2];
            widthHeight[0] = sceneLines[index + 1].Count(c => c == ',');
            int height = 0;
            while (sceneLines[index + 1] != "")
            {
                height++;
                index++;
            }
            widthHeight[1] = height;
            return widthHeight;
        }

        private Cell[,] CreateCellGrid(string[] sceneLines, int index)
        {
            int[] widthHeight = GetWidthHeight(sceneLines, index);
            int width = widthHeight[0];
            int height = widthHeight[1];

            Cell[,] cells = new Cell[width, height];
            Cell cell = new();

            string[,] stringGrid = new string[height, width];

            for (int i = index + 1; i < index + height + 1; i++)
            {
                string[] values = sceneLines[index].Split(',');
                for (int j = 0; j < width; j++)
                {
                    stringGrid[i, j] = values[j];
                }
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    
                }
            }
            return cells;
        }

        private void FillTilesDictionnary(List<string[]> TilesLines)
        {
            foreach (string[] tileLines in TilesLines)
            {
                TileData tileData = new TileData();
                string name = "";
                for (int i = 0; i < tileLines.Length; i++)
                {
                    if (tileLines[i] == "Name:")
                    {
                        name = tileLines[i + 1];
                    }

                    // Walkable
                    else if (tileLines[i] == "Walkable:")
                    {
                        tileData.IsWalkable = GetBool(tileLines[i + 1]);
                    }

                    // Drawing
                    else if (tileLines[i] == "Drawing:")
                    {
                        tileData.Drawing = GetDrawing(tileLines, i);
                    }

                    // Foreground Color
                    else if (tileLines[i] == "FGColor:")
                    {
                        tileData.ForegroundColor = GetColor(tileLines, i);
                    }

                    // Background Color
                    else if (tileLines[i] == "BGColor:")
                    {
                        tileData.BackgroundColor = GetColor(tileLines, i);
                    }
                }

                _tilesData.Add(name, tileData);
            }
        }

        private void FillDoorDictionnary(List<string[]> doorsLines)
        {
            foreach (string[] doorLines in doorsLines)
            {
                DoorData doorData = new DoorData();
                MapObjectData mapData = new MapObjectData();
                string name = "";
                for (int i = 0; i < doorLines.Length; i++)
                {

                    if (doorLines[i] == "Name:")
                    {
                        name = doorLines[i + 1];
                    }

                    else if (doorLines[i] == "TargetScene:")
                    {
                        doorData.TargetSceneName = doorLines[i + 1];
                    }

                    else if (doorLines[i] == "IsLocked:")
                    {
                        doorData.IsLocked = GetBool(doorLines[i + 1]);
                    }

                    else if (doorLines[i] == "ArrivalX:")
                    {
                        doorData.ArrivalX = GetInt(doorLines[i + 1]);
                    }

                    else if (doorLines[i] == "ArrivalY:")
                    {
                        doorData.ArrivalY = GetInt(doorLines[i + 1]);
                    }

                    else if (doorLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(doorLines, i);
                    }

                    else if (doorLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(doorLines, i);
                    }

                    else if (doorLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(doorLines, i);
                    }
                }

                doorData.MapData = mapData;
                _doorsData.Add(name, doorData);
            }
        }

        private void FillChestDictionnary(List<string[]> chestsLines)
        {
            foreach (string[] chestLines in chestsLines)
            {
                ChestData chestData = new ChestData();
                MapObjectData mapData = new MapObjectData();
                string name = "";
                for (int i = 0; i < chestLines.Length; i++)
                {

                    if (chestLines[i] == "Name:")
                    {
                        name = chestLines[i + 1];
                    }

                    else if (chestLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(chestLines, i);
                    }

                    else if (chestLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(chestLines, i);
                    }

                    else if (chestLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(chestLines, i);
                    }
                }

                chestData.MapData = mapData;
                _chestsData.Add(name, chestData);
            }
        }

        private void FillCharacterDictionnary(List<string[]> charactersLines)
        {
            foreach (string[] characterLines in charactersLines)
            {
                CharacterData characterData = new();
                MapObjectData mapData = new();
                string name = "";
                for (int i = 0; i < characterLines.Length; i++)
                {
                    if (characterLines[i] == "Name:")
                    {
                        name = characterLines[i + 1];
                    }

                    else if (characterLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(characterLines, i);
                    }

                    else if (characterLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(characterLines, i);
                    }

                    else if (characterLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(characterLines, i);
                    }
                }

                characterData.MapData = mapData;
                _charactersData.Add(name, characterData);
            }
        }

        private void FillSceneDictionnary(List<string[]> scenesLines)
        {
            foreach (string[] sceneLines in scenesLines)
            {

                Scene scene = new();

                for (int i = 0; i < sceneLines.Length; i++)
                {
                    if (sceneLines[i] == "Name:")
                    {
                        scene.Name = sceneLines[i + 1];
                    }

                    else if (sceneLines[i] == "IDs:")
                    {
                        Dictionary<string, TileData> tilesIDs = TilesIDs(sceneLines, i);
                    }

                    else if (sceneLines[i] == "Board:")
                    {
                        scene.Width = GetWidthHeight(sceneLines, i)[0];
                        scene.Height = GetWidthHeight(sceneLines, i)[1];
                       // scene.Cells = CreateCellGrid(scene.Width, scene.Height);
                    }

                    else if (sceneLines[i] == "MapObjects:")
                    {
                        
                    }

                }

                _scenes.Add(scene.Name , scene);
            }
        }

        public void FillAllDictionnaries()
        {
            Dictionary<string, List<string[]>> ressourcesLines = GetRessources();
            foreach (string key in ressourcesLines.Keys)
            {
                bool tilesDone = false;
                bool mapObjectsDone = false;
                if (key == "Tiles")
                {
                    FillTilesDictionnary(ressourcesLines[key]);
                }
                else if (key == "MapObjects")
                {
                    List<string[]> charactersLines = new List<string[]> { };
                    List<string[]> chestsLines = new List<string[]> { };
                    List<string[]> doorsLines = new List<string[]> { };
                    foreach (string[] mapObject in ressourcesLines[key])
                    {
                        switch (mapObject[1])
                        {
                            case "Character":
                                charactersLines.Add(mapObject);
                                break;
                            case "Chest":
                                chestsLines.Add(mapObject);
                                break;
                            case "Door":
                                doorsLines.Add(mapObject);
                                break;
                        }
                    }
                    FillCharacterDictionnary(charactersLines);
                    FillChestDictionnary(chestsLines);
                    FillDoorDictionnary(doorsLines);
                }
                else if (key == "Scenes" && tilesDone && mapObjectsDone)
                {
                    FillSceneDictionnary(ressourcesLines[key]);
                }
            }
        }

        public void DisplayDictionaries()
        {
            void DisplayDrawingWithColors(char[,] drawing, ConsoleColor[,] foregroundColor, ConsoleColor[,] backgroundColor)
            {
                for (int i = 0; i < drawing.GetLength(0); i++)
                {
                    for (int j = 0; j < drawing.GetLength(1); j++)
                    {
                        Console.ForegroundColor = foregroundColor[i, j];
                        Console.BackgroundColor = backgroundColor[i, j];
                        Console.Write(drawing[i, j]);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
            }

            void DisplayMapObjectData(MapObjectData mapData)
            {
                DisplayDrawingWithColors(mapData.Drawing, mapData.ForegroundColor, mapData.BackgroundColor);
            }

            void DisplayDictionary<T>(Dictionary<string, T> dictionary)
            {
                foreach (var kvp in dictionary)
                {
                    Console.WriteLine($"Key: {kvp.Key}");

                    // If the value is a DoorData struct
                    if (typeof(T) == typeof(DoorData))
                    {
                        var door = (DoorData)(object)kvp.Value;
                        DisplayMapObjectData(door.MapData);
                    }
                    else if (typeof(T) == typeof(CharacterData))
                    {
                        var character = (CharacterData)(object)kvp.Value;
                        DisplayMapObjectData(character.MapData);
                    }
                    else if (typeof(T) == typeof(ChestData))
                    {
                        var chest = (ChestData)(object)kvp.Value;
                        DisplayMapObjectData(chest.MapData);
                    }
                    // If the value is a TileData struct
                    else if (typeof(T) == typeof(TileData))
                    {
                        var tile = (TileData)(object)kvp.Value;
                        DisplayDrawingWithColors(tile.Drawing, tile.ForegroundColor, tile.BackgroundColor);
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine("Tiles Data:");
            DisplayDictionary(_tilesData);

            Console.WriteLine("Doors Data:");
            DisplayDictionary(_doorsData);

            Console.WriteLine("Chests Data:");
            DisplayDictionary(_chestsData);

            Console.WriteLine("Characters Data:");
            DisplayDictionary(_charactersData);
        }

        #endregion Methods

    }
}
