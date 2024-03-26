using HeraclesCreatures.Source.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    enum GamePhase
    {
        Beginning,
        Checkpoint1,
        Checkpoint2,
        Checkpoint3,
        Checkpoint4,
        Checkpoint5,
        Checkpoint6,
        Checkpoint7,
        Checkpoint8,
        Checkpoint9,
        Checkpoint10,
        Checkpoint11,
        Ending,
    }

    internal class GameManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        bool                                _isRunning;
        GamePhase                           _gamePhase;

        InputManager                        _inputManager;
        CombatManager                       _currentFight;

        Scene                               _scene;

        Dictionary<string, CreatureStats>   _creaturesStats;
        Dictionary<string, MoveStats>       _moveStats;
        Dictionary<string, List<string>>    _movePools;
        Dictionary<string, Moves>           _moves;
        List<string>                        _types;
        float[,]                            _typeTable;

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public GameManager()
        {
            _isRunning = true;
            _gamePhase = GamePhase.Beginning;
            _inputManager = new InputManager();
            _creaturesStats = new Dictionary<string, CreatureStats>();
            _moveStats = new Dictionary<string, MoveStats>();
            _movePools = new Dictionary<string, List<string>>();
            _moves = new Dictionary<string, Moves>();

            GenerateTypes();
            GenerateMoves();
            GenerateCreatures();

            Enemy hydra = GenerateEnemy("Hydra");
            Player Hercule = new Player("Hercule");
            Potion popo = new Potion();
            AttackPlus attP = new AttackPlus();
            Creatures lion = new Creatures("Nemean Lion", _creaturesStats["Nemean Lion"], GenerateCreatureMovePool("Nemean Lion"));
            Creatures heracles = new Creatures("Heracles", _creaturesStats["Heracles"], GenerateCreatureMovePool("Heracles"));
            Hercule.AddCreature(lion);
            Hercule.AddCreature(heracles);
            Hercule.AddItems(popo);
            Hercule.AddItems(attP);
            CombatManager test = new CombatManager(Hercule, hydra, _types, _typeTable);
            _currentFight = test;
            test.StartFight();

            int i = 0;
            // Sauvegarder les données
            GameData gameData = new GameData(Hercule, _gamePhase);
            SaveManager.Save(gameData, "savegame.dat");

            Console.WriteLine("Données sauvegardées.");

            // Charger les données
            GameData loadedData = SaveManager.Load("savegame.dat");
            if (loadedData != null)
            {
                Console.WriteLine("Données chargées :");
                Console.WriteLine("CheckPoint : " + loadedData.Phase.ToString());
                Console.WriteLine("Name : " + loadedData.Player.Name);
            }
        }

        public void GameLoop()
        {
            while (_isRunning)
            {
                _inputManager.Update();
                //FileManager fileManager = new FileManager();
                //fileManager.GetTileData("");

                if (_inputManager.IsAnyKeyPressed())
                {
                    Console.WriteLine("Oueoue");
                }
                if (_currentFight != null && _currentFight.IsOver == false)
                {
                    _currentFight.Fighting();
                }
            }

        }

        private void GenerateTypes()
        {
            _typeTable = new float[,]
            {
                { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 0.0f },
                { 1.0f, 0.5f, 0.5f, 2.0f, 2.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 2.0f, 0.5f, 0.5f, 1.0f, 1.0f, 2.0f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 0.5f, 2.0f, 0.5f, 0.5f, 0.5f, 2.0f, 1.0f, 2.0f, 0.5f, 1.0f },
                { 1.0f, 0.5f, 1.0f, 0.5f, 0.5f, 1.0f, 1.0f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 2.0f, 0.5f, 0.5f, 2.0f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 2.0f, 1.0f, 0.5f, 2.0f, 0.0f, 0.5f, 1.0f, 2.0f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 2.0f, 0.5f, 1.0f, 0.5f, 1.0f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 2.0f, 0.0f, 1.0f, 0.5f, 1.0f, 0.5f, 0.5f, 1.0f },
                { 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.5f, 1.0f, 1.0f, 2.0f }
            };
            _types = new List<string>
            {
                "Normal",
                "Fire",
                "Water",
                "Plant",
                "Steel",
                "Flying",
                "Ground",
                "Dark",
                "Rock",
                "Poison",
                "Ghost"
            };

        }

        private List<Moves> GenerateCreatureMovePool(string creatureName)
        {
            List<Moves> moves = new List<Moves>();
            for (int i = 0; i < _movePools[creatureName].Count; i++)
            {
                if (_moveStats[_movePools[creatureName][i]].ManaCost > 0)
                {
                    Spell spell = new Spell(_movePools[creatureName][i], _moveStats[_movePools[creatureName][i]]);
                    moves.Add(spell);
                }
                else
                {
                    Attack attack = new Attack(_movePools[creatureName][i], _moveStats[_movePools[creatureName][i]]);
                    moves.Add(attack);
                }
            }
            return moves;
        }

        private void GenerateMoves()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\Moves";
            if (Directory.Exists(folderPath))
            {
                string[] subDirectories = Directory.GetDirectories(folderPath);

                foreach (string subDirectory in subDirectories)
                {
                    string[] files = Directory.GetFiles(subDirectory, "*.txt");

                    foreach (string filePath in files)
                    {
                        try
                        {
                            string[] lines = File.ReadAllLines(filePath);

                            MoveStats moveStats = new MoveStats();

                            foreach (string line in lines)
                            {
                                string[] parts = line.Split(':');
                                if (parts.Length == 2)
                                {
                                    string key = parts[0].Trim();
                                    string value = parts[1].Trim();

                                    switch (key)
                                    {
                                        case "POWER":
                                            moveStats.Power = float.Parse(value);
                                            break;
                                        case "ACCURACY":
                                            moveStats.Accuracy = float.Parse(value);
                                            break;
                                        case "CRITRATE":
                                            moveStats.CritRate = float.Parse(value);
                                            break;
                                        case "MAXPP":
                                            moveStats.MaxPP = float.Parse(value);
                                            break;
                                        case "PP":
                                            moveStats.PP = float.Parse(value);
                                            break;
                                        case "MANACOST":
                                            moveStats.ManaCost = float.Parse(value);
                                            break;
                                        case "TYPE":
                                            moveStats.Type = value;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }

                            string moveName = Path.GetFileNameWithoutExtension(filePath);
                            moveName = moveName.Replace('_', ' ');

                            _moveStats.Add(moveName, moveStats);
                            Moves move = new Moves(moveName, moveStats);
                            _moves.Add(moveName, move);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"An error occurred while reading the file {filePath}: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine($"The specified directory does not exist: {folderPath}");
            }
        }


        private void GenerateCreatures()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\Creatures";

            // Vérifier si le dossier existe
            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.txt");

                // Parcourir tous les fichiers
                foreach (string filePath in files)
                {
                    try
                    {
                        string[] lines = File.ReadAllLines(filePath);

                        CreatureStats creatureStats = new CreatureStats();

                        List<string> movePool = new List<string>();

                        bool inMovePoolSection = false;

                        foreach (string line in lines)
                        {
                            if (line.Trim().Equals("MovePool :", StringComparison.OrdinalIgnoreCase))
                            {
                                inMovePoolSection = true;
                            }
                            else if (inMovePoolSection && !string.IsNullOrWhiteSpace(line))
                            {
                                movePool.Add(line.Trim());
                            }
                            else
                            {
                                string[] parts = line.Split(':');
                                if (parts.Length == 2)
                                {
                                    string key = parts[0].Trim();
                                    string value = parts[1].Trim();

                                    switch (key)
                                    {
                                        case "HP":
                                            creatureStats.health = float.Parse(value);
                                            break;
                                        case "MAXHP":
                                            creatureStats.maxHealth = float.Parse(value);
                                            break;
                                        case "ATTACK":
                                            creatureStats.attack = float.Parse(value);
                                            break;
                                        case "MAGICPOWER":
                                            creatureStats.magicpower = float.Parse(value);
                                            break;
                                        case "DEFENSE":
                                            creatureStats.defense = float.Parse(value);
                                            break;
                                        case "MANA":
                                            creatureStats.mana = float.Parse(value);
                                            break;
                                        case "MAXMANA":
                                            creatureStats.maxMana = float.Parse(value);
                                            break;
                                        case "SPEED":
                                            creatureStats.AttackSpeed = float.Parse(value);
                                            break;
                                        case "TYPE":
                                            creatureStats.type = value;
                                            break;
                                    }
                                }
                            }
                        }

                        string creatureName = Path.GetFileNameWithoutExtension(filePath);
                        creatureName = creatureName.Replace('_', ' ');

                        _creaturesStats.Add(creatureName, creatureStats);

                        _movePools.Add(creatureName, movePool);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Une erreur s'est produite lors de la lecture du fichier {filePath}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"Le dossier spécifié n'existe pas : {folderPath}");
            }
        }

        Enemy GenerateEnemy(string enemyName)
        {
            Enemy enemy = null;

            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\Enemies";

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.txt");

                foreach (string filePath in files)
                {
                    if (Path.GetFileNameWithoutExtension(filePath).Replace('_', ' ') == enemyName)
                    {
                        int difficulty = 1;
                        List<Creatures> enemyTeam = new List<Creatures>();

                        try
                        {
                            string[] lines = File.ReadAllLines(filePath);

                            bool inDifficultySection = false;
                            bool inCreaturesPoolSection = false;
                            bool inItemsPoolSection = false;

                            foreach (string line in lines)
                            {
                                if (line.Trim().Equals("Difficulty :", StringComparison.OrdinalIgnoreCase))
                                {
                                    inDifficultySection = true;
                                    inCreaturesPoolSection = false;
                                    inItemsPoolSection = false;
                                }
                                else if (line.Trim().Equals("Creatures :", StringComparison.OrdinalIgnoreCase))
                                {
                                    inDifficultySection = false;
                                    inCreaturesPoolSection = true;
                                    inItemsPoolSection = false;
                                }
                                else if (line.Trim().Equals("Items :", StringComparison.OrdinalIgnoreCase))
                                {
                                    inDifficultySection = false;
                                    inCreaturesPoolSection = false;
                                    inItemsPoolSection = true;
                                }
                                else if (inDifficultySection && !string.IsNullOrWhiteSpace(line))
                                {
                                    difficulty = Int32.Parse(line.Trim());
                                }
                                else if (inCreaturesPoolSection && !string.IsNullOrWhiteSpace(line))
                                {
                                    Creatures creature = new Creatures(line.Trim(), _creaturesStats[line.Trim()], GenerateCreatureMovePool(line.Trim()));
                                    enemyTeam.Add(creature);
                                }
                                else if (inItemsPoolSection && !string.IsNullOrWhiteSpace(line))
                                {
                                    // Faire la liste items et l'ajouter a l'enemy
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Une erreur s'est produite lors de la lecture du fichier {filePath}: {ex.Message}");
                        }
                        enemy = new Enemy(enemyName, enemyTeam, difficulty, _types, _typeTable);
                    }
                    
                }
            }
            else
            {
                Console.WriteLine($"Le dossier spécifié n'existe pas : {folderPath}");
            }
            return enemy;
        }

        #endregion Methods

    }
}
