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

        InputManager _inputManager;
        CombatManager _currentFight;
        Scene _scene;
        Dictionary<string, CreatureStats> _creaturesStats;
        Dictionary<string, MoveStats> _moveStats;
        Dictionary<string, List<string>> _movePools;
        bool _isRunning;
        List<string> _types;
        float[,] _typeTable;

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
            _inputManager = new InputManager();
            _creaturesStats = new Dictionary<string, CreatureStats>();
            _moveStats = new Dictionary<string, MoveStats>();
            _movePools = new Dictionary<string, List<string>>();

            GenerateTypes();
            GenerateMoves();
            GenerateCreatures();

            CreatureStats OrangOutanStats = new CreatureStats();
            OrangOutanStats.type = "Plant";
            Creatures OrangOutant = new Creatures("OrangOutant", OrangOutanStats);
            List<Creatures> Singe = new List<Creatures>();
            Attack COUPDECAILLOU = new Attack("COUPDECAILLOU");
            Spell COUP2TETE = new Spell("COUP2TETE");
            OrangOutant.AddMove(COUPDECAILLOU);
            Singe.Add(OrangOutant);
            Enemy Ougabouga = new Enemy("Ougabouga", Singe, 2, _types, _typeTable);
            CreatureStats TigerStats = new CreatureStats();
            Creatures Tiger = new Creatures("Tiger", TigerStats);
            Tiger.AddMove(COUPDECAILLOU);
            Tiger.AddMove(COUP2TETE);
            CreatureStats ViperStats = new CreatureStats();
            Creatures Viper = new Creatures("Viper", ViperStats);
            Viper.AddMove(COUPDECAILLOU);
            Potion popo = new Potion();
            AttackPlus attP = new AttackPlus();
            Player Hercule = new Player("Hercule");
            Hercule.AddCreature(Tiger);
            Hercule.AddCreature(Viper);
            Hercule.AddItems(popo);
            Hercule.AddItems(attP);
            CombatManager test = new CombatManager(Hercule, Ougabouga, _types, _typeTable);
            _currentFight = test;
            test.StartFight();

            Enemy hydra = GenerateEnemy("Hydra");
            int i = 0;
        }

        public void GameLoop()
        {
            while (_isRunning)
            {
                _inputManager.Update();

                if (_inputManager.IsAnyKeyPressed())
                {
                    Console.WriteLine("Oueoue");
                }
                if (_currentFight != null)
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

        private void GenerateMoves()
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\Resources\\Moves";
            if (Directory.Exists(folderPath))
            {

                string[] files = Directory.GetFiles(folderPath, "*.txt");

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
                                    Creatures creature = new Creatures(line.Trim(), _creaturesStats[line.Trim()]);
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
