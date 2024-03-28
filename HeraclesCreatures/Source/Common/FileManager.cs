using System.Reflection.Metadata.Ecma335;
using static System.Formats.Asn1.AsnWriter;

namespace HeraclesCreatures
{
    public class FileManager
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
        Dictionary<string, OpponentData> _opponentsData;
        Dictionary<string, TileData> _tilesData;
        Dictionary<string, DoorData> _doorsData;
        Dictionary<string, ChestData> _chestsData;
        Dictionary<string, GrassData> _grassesData;
        Dictionary<string, NpcData> _npcsData;
        Dictionary<string, HealingStandData> _healingStandsData;
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
        public Dictionary<string, OpponentData> OpponentsData { get => _opponentsData; set => _opponentsData = value; }
        public Dictionary<string, TileData> TilesData { get => _tilesData; set => _tilesData = value; }
        public Dictionary<string, DoorData> DoorsData { get => _doorsData; set => _doorsData = value; }
        public Dictionary<string, ChestData> ChestsData { get => _chestsData; set => _chestsData = value; }
        public Dictionary<string, GrassData> GrassesData { get => _grassesData; set => _grassesData = value; }
        public Dictionary<string, NpcData> NpcsData { get => _npcsData; set => _npcsData = value; }
        public Dictionary<string, HealingStandData> HealingStandsData { get => _healingStandsData; set => _healingStandsData = value; }
        public Dictionary<string, CharacterData> CharactersData { get => _charactersData; set => _charactersData = value; }
        public Dictionary<string, Scene> Scenes { get => _scenes; set => _scenes = value; }

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
            _opponentsData = new();
            _grassesData = new();
            _npcsData = new();
            _healingStandsData = new();
            _scenes = new();
            FillAllDictionnaries();
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

        private List<string> GetDialogue(string[] dialogueLines, int index)
        {
            List<string> dialogue = new List<string> {};
            while (dialogueLines[index] != "")
            {
                dialogue.Add(dialogueLines[index]);
                index++;
            }
            return dialogue;
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
            widthHeight[0] = sceneLines[index + 1].Count(c => c == ',') + 1;
            int height = 0;
            while (sceneLines[index + 1] != "")
            {
                height++;
                index++;
            }
            widthHeight[1] = height;
            return widthHeight;
        }

        private Scene CreateCellGrid(string[] sceneLines, int index, Dictionary<string, TileData> tilesIDs)
        {
            Scene scene = new Scene();

            int[] widthHeight = GetWidthHeight(sceneLines, index);
            scene.Width = widthHeight[0];
            scene.Height = widthHeight[1];
            scene.SceneObjects = new MapObject[scene.Height, scene.Width];

            Cell[,] cells = new Cell[scene.Height, scene.Width];

            for (int i = 0; i < scene.Height; i++)
            {
                string[] values = sceneLines[i + index + 1].Split(',');
                for (int j = 0; j < scene.Width; j++)
                {
                    Cell cell = new();
                    cell.X = i;
                    cell.Y = j;
                    cell.Tile = tilesIDs[values[j]];
                    cells[i,j] = cell;
                }
            }
            scene.Cells = cells;
            return scene;
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

        private void FillGrassDictionnary(List<string[]> grassesLines)
        {
            foreach (string[] grassLines in grassesLines)
            {
                GrassData grassData = new GrassData();
                MapObjectData mapData = new MapObjectData();
                string name = "";
                for (int i = 0; i < grassLines.Length; i++)
                {

                    if (grassLines[i] == "Name:")
                    {
                        name = grassLines[i + 1];
                    }

                    else if (grassLines[i] == "Rate:")
                    {
                        grassData.EncounterRate = GetInt(grassLines[i + 1]);
                    }

                    else if (grassLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(grassLines, i);
                    }

                    else if (grassLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(grassLines, i);
                    }

                    else if (grassLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(grassLines, i);
                    }
                }

                grassData.MapData = mapData;
                _grassesData.Add(name, grassData);
            }
        }

        private void FillNpcDictionnary(List<string[]> npcsLines)
        {
            foreach (string[] npcLines in npcsLines)
            {
                NpcData npcData = new();
                MapObjectData mapData = new();
                string name = "";
                for (int i = 0; i < npcLines.Length; i++)
                {
                    if (npcLines[i] == "Name:")
                    {
                        name = npcLines[i + 1];
                    }

                    else if (npcLines[i] == "Dialogue:")
                    {
                        npcData.Dialogue = GetDialogue(npcLines, i + 1);
                    }

                    else if (npcLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(npcLines, i);
                    }

                    else if (npcLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(npcLines, i);
                    }

                    else if (npcLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(npcLines, i);
                    }
                }

                npcData.MapData = mapData;
                _npcsData.Add(name, npcData);
            }
        }

        private void FillHealingStandDictionnary(List<string[]> healingStandsLines)
        {
            foreach (string[] healingStandLines in healingStandsLines)
            {
                HealingStandData healingStandData = new();
                MapObjectData mapData = new();
                string name = "";
                for (int i = 0; i < healingStandLines.Length; i++)
                {
                    if (healingStandLines[i] == "Name:")
                    {
                        name = healingStandLines[i + 1];
                    }

                    else if (healingStandLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(healingStandLines, i);
                    }

                    else if (healingStandLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(healingStandLines, i);
                    }

                    else if (healingStandLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(healingStandLines, i);
                    }
                }

                healingStandData.MapData = mapData;
                _healingStandsData.Add(name, healingStandData);
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

        private void FillOpponentsDictionnary(List<string[]> opponentsLines)
        {
            foreach (string[] opponentLines in opponentsLines)
            {
                OpponentData opponentData = new();
                MapObjectData mapData = new();
                string name = "";
                for (int i = 0; i < opponentLines.Length; i++)
                {
                    if (opponentLines[i] == "Name:")
                    {
                        name = opponentLines[i + 1];
                    }

                    else if (opponentLines[i] == "Drawing:")
                    {
                        mapData.Drawing = GetDrawing(opponentLines, i);
                    }

                    else if (opponentLines[i] == "FGColor:")
                    {
                        mapData.ForegroundColor = GetColor(opponentLines, i);
                    }

                    else if (opponentLines[i] == "BGColor:")
                    {
                        mapData.BackgroundColor = GetColor(opponentLines, i);
                    }
                }

                opponentData.MapData = mapData;
                _opponentsData.Add(name, opponentData);
            }
        }

        private void FillSceneDictionnary(List<string[]> scenesLines)
        {
            foreach (string[] sceneLines in scenesLines)
            {

                Scene scene = new();
                string name = "";
                Dictionary<string, TileData> tilesIDs = new();

                for (int i = 0; i < sceneLines.Length; i++)
                {
                    if (sceneLines[i] == "Name:")
                    {
                        name = sceneLines[i + 1];
                    }

                    else if (sceneLines[i] == "IDs:")
                    {
                        tilesIDs = TilesIDs(sceneLines, i);
                    }

                    else if (sceneLines[i] == "Board:")
                    {
                        scene = CreateCellGrid(sceneLines, i, tilesIDs);
                    }

                }

                scene.Name = name;
                _scenes.Add(scene.Name , scene);
            }
        }

        public void FillAllDictionnaries()
        {
            Dictionary<string, List<string[]>> ressourcesLines = GetRessources();
            foreach (string key in ressourcesLines.Keys)
            {
                if (key == "Tiles")
                {
                    FillTilesDictionnary(ressourcesLines[key]);
                    FillSceneDictionnary(ressourcesLines["Scenes"]);
                }
                else if (key == "Moves")
                {
                    //FillMovesDictionnaries();
                }
                else if (key == "Creatures")
                {
                    //FillCreaturesDictionnary();
                }
                else if (key == "MapObjects")
                {
                    List<string[]> charactersLines = new List<string[]> { };
                    List<string[]> chestsLines = new List<string[]> { };
                    List<string[]> doorsLines = new List<string[]> { };
                    List<string[]> opponentsLines = new List<string[]> { };
                    List<string[]> grassesLines = new List<string[]> { };
                    List<string[]> npcsLines = new List<string[]> { };
                    List<string[]> healingStandsLines = new List<string[]> { };
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
                            case "Opponent":
                                opponentsLines.Add(mapObject);
                                break;
                            case "Grass":
                                grassesLines.Add(mapObject);
                                break;
                            case "Npc":
                                npcsLines.Add(mapObject);
                                break;
                            case "HealingStand":
                                healingStandsLines.Add(mapObject);
                                break;
                        }
                    }
                    FillCharacterDictionnary(charactersLines);
                    FillChestDictionnary(chestsLines);
                    FillDoorDictionnary(doorsLines);
                    FillOpponentsDictionnary(opponentsLines);
                    FillGrassDictionnary(grassesLines);
                    FillNpcDictionnary(npcsLines);
                    FillHealingStandDictionnary(healingStandsLines);
                }
            }
        }

        #endregion Methods

    }
}
