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
        Dictionary<string, MapObject> _mapObjectsData;
        Dictionary<string, Player> _playersData;
        Dictionary<string, Scene> _scenesData;

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
        internal Dictionary<string, Player> PlayersData { get => _playersData; set => _playersData = value; }
        public Dictionary<string, Scene> ScenesData { get => _scenesData; set => _scenesData = value; }

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
            _creaturesData = new Dictionary<string, CreatureStats>();
            _tilesData = new Dictionary<string, TileData>();
            _playersData = new Dictionary<string, Player>();
            _scenesData = new Dictionary<string, Scene>();
        }

        string[] ReadFile(string filePath)
        {
            string[] empty = new string[0];
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
                    
                    return empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the file: {ex.Message}");
                return empty;
            }
        }

        List<string> GetFolderBranches(string folderPath)
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

        public Dictionary<string, List<string[]>> GetRessources()
        {
            string GetMapID(string fullPath)
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
                            string mapID = GetMapID(branch);
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

        public TileData GetMapObjectData(string filePath)
        {

            TileData cellData = new TileData();

            string[] mapObjectLines = ReadFile(filePath);
            if (mapObjectLines != null)
            {
                for (int i = 0; i < mapObjectLines.Length; i++)
                {

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
                return new TileData();
            }

        }



        #endregion Methods

    }
}
