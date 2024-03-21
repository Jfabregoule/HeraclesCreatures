using HeraclesCreatures;

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
        Dictionary<string, SpellStats> _spellsData;
        Dictionary<string, AttackStats> _attacksData;
        Dictionary<string, MapObject> _tilesData;
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
        internal Dictionary<string, SpellStats> SpellsData { get => _spellsData; set => _spellsData = value; }
        internal Dictionary<string, AttackStats> AttacksData { get => _attacksData; set => _attacksData = value; }
        public Dictionary<string, MapObject> TilesData { get => _tilesData; set => _tilesData = value; }
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
            _spellsData = new Dictionary<string, SpellStats>();
            _attacksData = new Dictionary<string, AttackStats>();
            _tilesData = new Dictionary<string, MapObject>();
            _playersData = new Dictionary<string, Player>();
            _scenesData = new Dictionary<string, Scene>();
        }

        public string[] ReadFile(string filePath)
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

        public Spell GetSpellData(string filePath)
        {

            Spell spell = new Spell();
            SpellStats stats = new SpellStats();

            string[] spellLines = ReadFile(filePath);
            if (spellLines != null)
            {

                for (int i = 0; i < spellLines.Length; i++)
                {

                    // Name
                    if (spellLines[i] == "Name:")
                    {
                        spell.MoveName = spellLines[i+1];
                    }

                    // Stats
                    else if (spellLines[i] == "Stats:")
                    {
                        
                    }

                }

                return spell;
            }
            else { 
                Console.WriteLine(); 
                return new Spell();
            }
        }


        #endregion Methods

    }
}
