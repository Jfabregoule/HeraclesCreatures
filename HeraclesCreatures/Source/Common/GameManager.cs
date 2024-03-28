using HeraclesCreatures.Source.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Metadata;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HeraclesCreatures
{
    public enum GamePhase
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

    public class GameManager
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
        Menu                                _menu;
        FileManager                         _fileManager;
        CombatManager                       _currentFight;

        Player                              _player;
        Scene                               _currentScene;

        Dictionary<string, CreatureStats>   _creaturesStats;
        Dictionary<string, MoveStats>       _moveStats;
        Dictionary<string, List<string>>    _movePools;
        Dictionary<string, Moves>           _moves;
        List<string>                        _types;
        float[,]                            _typeTable;

        Character _heracles;

        int _consoleHeight;
        int _consoleWidth;

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
            _menu = new Menu(_inputManager);
            _fileManager = new FileManager();
            _creaturesStats = new Dictionary<string, CreatureStats>();
            _moveStats = new Dictionary<string, MoveStats>();
            _movePools = new Dictionary<string, List<string>>();
            _moves = new Dictionary<string, Moves>();

            _consoleWidth = Console.WindowWidth;
            _consoleHeight = Console.WindowHeight;

            GenerateTypes();
            GenerateMoves();
            GenerateCreatures();

            InitializeScenes();

            //SaveGame();

            //LoadGame();
        }

        public void InitializeScenes()
        {

            // Set Scene
            _currentScene = _fileManager.Scenes["Scene0"];

            // Create Character
            List<Creatures> creatures = new List<Creatures>();
            Creatures lion = new Creatures("Nemean Lion", _creaturesStats["Nemean Lion"], GenerateCreatureMovePool("Nemean Lion"));
            Creatures heracles = new Creatures("Heracles", _creaturesStats["Heracles"], GenerateCreatureMovePool("Heracles"));
            creatures.Add(lion);
            creatures.Add(heracles);
            Player Hercule = new Player("Hercule", creatures); 
            _heracles = new Character(4, 11,_currentScene, _fileManager.CharactersData["Heracles"], Hercule);
            _currentScene.AddMapObject(_heracles);

            // Default Game Objects
            Door door = new Door();
            Grass grass = new Grass();
            Chest chest = new Chest();


            // SCENE 0

            door = new Door(4, 1, _fileManager.DoorsData["Door0-1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(1, 1, _fileManager.DoorsData["Door0-2"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(1, 6, _fileManager.DoorsData["Door0-3"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(1, 11, _fileManager.DoorsData["Door0-4"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(1, 16, _fileManager.DoorsData["Door0-5"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(1, 21, _fileManager.DoorsData["Door0-6"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            door = new Door(4, 21, _fileManager.DoorsData["Door0-7"]);
            _fileManager.Scenes["Scene0"].AddMapObject(door);

            grass = new Grass(2, 2,new List<string> {"GrassLion", "GrassHydra"}, _fileManager.GrassesData["Grass1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(grass);
            grass = new Grass(2, 3, new List<string> { "GrassLion", "GrassHydra" }, _fileManager.GrassesData["Grass1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(grass);
            grass = new Grass(3, 2, new List<string> { "GrassLion", "GrassHydra" }, _fileManager.GrassesData["Grass1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(grass);
            grass = new Grass(3, 3, new List<string> { "GrassLion", "GrassHydra" }, _fileManager.GrassesData["Grass1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(grass);

            chest = new Chest(5, 20, new List<Items> { new Potion(), new Revive() }, new List<int> { 2, 4 }, _fileManager.ChestsData["Chest1"]);
            _fileManager.Scenes["Scene0"].AddMapObject(chest);


            // SCENE 1


            door = new Door(4, 21, _fileManager.DoorsData["Door1-0"]);
            _fileManager.Scenes["Scene1"].AddMapObject(door);
            Opponent NemeanLion = new Opponent(4, 11, _fileManager.Scenes["Scene1"], _fileManager.OpponentsData["Nemean Lion"], GenerateEnemy("Nemean Lion"));
            _fileManager.Scenes["Scene1"].AddMapObject(NemeanLion);


            // SCENE 2


            door = new Door(7, 21, _fileManager.DoorsData["Door2-0"]);
            _fileManager.Scenes["Scene2"].AddMapObject(door);
            Opponent LerneanHydra = new Opponent(4, 11, _fileManager.Scenes["Scene2"], _fileManager.OpponentsData["Lernean Hydra"], GenerateEnemy("Lernean Hydra"));
            _fileManager.Scenes["Scene2"].AddMapObject(LerneanHydra);


            // SCENE 3


            door = new Door(7, 6, _fileManager.DoorsData["Door3-0"]);
            _fileManager.Scenes["Scene3"].AddMapObject(door);
            Opponent ErymantheanBoar = new Opponent(4, 11, _fileManager.Scenes["Scene3"], _fileManager.OpponentsData["Erymanthean Boar"], GenerateEnemy("Erymanthean Boar"));
            _fileManager.Scenes["Scene3"].AddMapObject(ErymantheanBoar);


            // SCENE 4


            door = new Door(7, 11, _fileManager.DoorsData["Door4-0"]);
            _fileManager.Scenes["Scene4"].AddMapObject(door);
            Opponent CeryneiasHind = new Opponent(4, 11, _fileManager.Scenes["Scene4"], _fileManager.OpponentsData["Ceryneia's Hind"], GenerateEnemy("Ceryneia's Hind"));
            _fileManager.Scenes["Scene4"].AddMapObject(CeryneiasHind);


            // SCENE 5


            door = new Door(7, 16, _fileManager.DoorsData["Door5-0"]);
            _fileManager.Scenes["Scene5"].AddMapObject(door);
            Opponent StymphalianBirds = new Opponent(4, 11, _fileManager.Scenes["Scene5"], _fileManager.OpponentsData["Stymphalian Birds"], GenerateEnemy("Stymphalian Birds"));
            _fileManager.Scenes["Scene5"].AddMapObject(StymphalianBirds);


            // SCENE 6


            door = new Door(7, 1, _fileManager.DoorsData["Door6-0"]);
            _fileManager.Scenes["Scene6"].AddMapObject(door);
            Opponent CretanBull = new Opponent(4, 11, _fileManager.Scenes["Scene6"], _fileManager.OpponentsData["Cretan Bull"], GenerateEnemy("Cretan Bull"));
            _fileManager.Scenes["Scene6"].AddMapObject(CretanBull);


            // SCENE 7


            door = new Door(4, 1, _fileManager.DoorsData["Door7-0"]);
            _fileManager.Scenes["Scene7"].AddMapObject(door);
            Opponent DiomedesHorses = new Opponent(4, 11, _fileManager.Scenes["Scene7"], _fileManager.OpponentsData["Diomedes Horse"], GenerateEnemy("Diomedes Horse"));
            _fileManager.Scenes["Scene7"].AddMapObject(DiomedesHorses);


            Potion popo = new Potion();
            AttackPlus attP = new AttackPlus();

            Hercule.AddItems(popo);
            Hercule.AddItems(attP);
            _player = Hercule;
        }

        public void SaveGame()
        {
            GameData gameData = new GameData(_player, _gamePhase);
            SaveManager.Save(gameData, "savegame.json");

            Console.WriteLine("Données sauvegardées.");
        }

        public void LoadGame()
        {
            GameData data = SaveManager.Load("savegame.json");
            _player = new Player(data._playerData);
            _gamePhase = data._phase;
            Console.WriteLine("Données chargées.");
        }

        public void CallMenu()
        {
            int currentOption = _menu.CheckMenu();
            
            if (currentOption != -1)
            {
                switch (currentOption)
                {
                    case 0:
                        CallInventory();
                        break;
                    case 1:
                        SaveGame();
                        break;
                    case 2:
                        LoadGame();
                        break;
                    case 3:
                        _isRunning = false;
                        break;

                }
                if (_isRunning == true)
                {
                    _currentScene.ResetDisplay();
                }
            }
            _inputManager.Update();
        }

        public void CallInventory()
        {
            int inventoryOption = _menu.Checkinventory();
            if (inventoryOption != -1)
            {
                switch (inventoryOption)
                {
                    case 0:
                        _menu.Creatures(_player);
                        break;
                    case 1:
                        break;
                }
                if (_isRunning == true)
                {
                    _currentScene.ResetDisplay();
                }
            }
            _inputManager.Update();
        }

        public void CheckMove()
        {
            int dir = -1;
            if (_inputManager.GetKeyDown(ConsoleKey.Z))
            {
                dir = 0;
            }
            else if (_inputManager.GetKeyDown(ConsoleKey.D))
            {
                dir = 1;
            }
            else if (_inputManager.GetKeyDown(ConsoleKey.S))
            {
                dir = 2;
            }
            else if (_inputManager.GetKeyDown(ConsoleKey.Q))
            {
                dir = 3;
            }
            MapObject interaction = _heracles.Move(dir);
            _currentScene.UpdateCharacter(_heracles);
            _currentScene.DisplayMapObjects();
            if (interaction != null)
            {
                interaction.PlayDialogue(_currentScene);
                if (interaction is Chest) { while (!_inputManager.GetKeyDown(ConsoleKey.Enter)) {} }
                object interactionResult = _heracles.Interact(interaction, _fileManager.Scenes, _types, _typeTable);
                if ((interactionResult is CombatManager || interactionResult is Grass ) && _player.Creatures.All(Creatures => Creatures.State == CreatureState.DEAD) == false)
                {
                    Console.Clear();
                    Enemy enemy = new();
                    if (interactionResult is Grass)
                    {
                        Grass grass = (Grass)interactionResult;
                        enemy = GenerateEnemy(grass.GetRandomCreature());
                        CombatManager combatManager = new CombatManager(_heracles.Data.Player, enemy, _types, _typeTable);
                        _currentFight = combatManager;
                    }
                    else
                    {
                        _currentFight = (CombatManager)interactionResult;
                    }
                    while (_currentFight.IsOver == false)
                    {
                        _currentFight.Fighting();
                    }
                    if (_currentFight.IsWin)
                    {
                        _currentFight = null;
                        if (interaction is Opponent)
                        {
                            _currentScene.ToRemove.Add(new int[] { interaction.X, interaction.Y });
                            _currentScene.RemoveMapObject(interaction);
                        }
                        else
                        {
                            enemy.CurrentCreature.FullHeal();
                        }
                    }
                    _currentScene.ResetDisplay();
                }
                else if (interactionResult is CombatManager)
                {
                    int x = 0;
                    int y = _currentScene.Height * 5 + 1;
                    Console.SetCursorPosition(x, y);
                    Console.WriteLine("All your team members are KO. Heal them before you try again.");
                }    
                else if (interactionResult is Scene)
                {
                    _currentScene = (Scene)interactionResult;
                    _currentScene.AddMapObject(_heracles);
                    _currentScene.ResetDisplay();
                }
            }
            if (Console.WindowWidth != _consoleWidth || Console.WindowHeight != _consoleHeight)
            {
                _consoleWidth = Console.WindowWidth;
                _consoleHeight = Console.WindowHeight;
                _currentScene.ResetDisplay();
            }
            _currentScene.UpdateCharacter(_heracles);
            _currentScene.DisplayMapObjects();
            Console.SetCursorPosition(0, 0);
            
        }

        public void GameLoop()
        {
            _currentScene.DisplayScene();
            _currentScene.DisplayMapObjects();
            while (_isRunning)
            {
                _inputManager.Update();
                if (_inputManager.IsAnyKeyPressed())
                {
                    CheckMove();
                    CallMenu();
                }
            }
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Thanks for playing our demo");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
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

        public List<Moves> GenerateCreatureMovePool(string creatureName)
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
                            int PP = 0;
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
                                            moveStats.Power = int.Parse(value);
                                            break;
                                        case "ACCURACY":
                                            moveStats.Accuracy = float.Parse(value);
                                            break;
                                        case "CRITRATE":
                                            moveStats.CritRate = int.Parse(value);
                                            break;
                                        case "MAXPP":
                                            moveStats.MaxPP = int.Parse(value);
                                            break;
                                        case "PP":
                                            PP = int.Parse(value);
                                            break;
                                        case "MANACOST":
                                            moveStats.ManaCost = int.Parse(value);
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

            if (Directory.Exists(folderPath))
            {
                string[] files = Directory.GetFiles(folderPath, "*.txt");

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
                                        case "MAXMANA":
                                            creatureStats.maxMana = int.Parse(value);
                                            break;
                                        case "MANA":
                                            creatureStats.Mana = int.Parse(value);
                                            break;
                                        case "LVL":
                                            creatureStats.Level= int.Parse(value);
                                            break;
                                        case "CURRENTXP":
                                            creatureStats.CurrentXp = int.Parse(value);
                                            break;
                                        case "XPNEEDED":
                                            creatureStats.XpNeeded = int.Parse(value);
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
