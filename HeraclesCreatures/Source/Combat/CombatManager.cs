using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HeraclesCreatures
{
    enum PlayerChoices
    {
        Combat,
        Item,
        Swap
    }
    public class CombatManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        Player              _player;
        Enemy               _enemy;
        int                 _currentTurn;
        bool                _isOver;
        List<string>        _types;
        float[,]            _typeTable;
        PlayerChoices       _choiceType;
        int                 _choiceIndex;
        InputManager        _inputManager;
        List<(int, int)>    _pos;
        List<(int, int)>    _Movespos;
        List<(int, int)>    _Teampos;
        List<(int, int)>    _Itemspos;
        (int, int)          _currentMousePos;
        bool                _isWin;
        int                 _offSet;


        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public bool IsOver { get => _isOver; private set => _isOver = value; }
        public int CurrentTurn { get => _currentTurn; private set => _currentTurn = value; }
        public bool IsWin { get => _isWin; set => _isWin = value; }


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

        public CombatManager(Player player, Enemy enemy, List<string> types, float[,] typeTable) 
        {
            _player = player;
            _enemy = enemy;
            _isOver = false;
            CurrentTurn = 0;
            _enemy.CurrentCreature = enemy.CurrentCreature;
            _types = types;
            _typeTable = typeTable;
            _pos = new List<(int, int)>();
            initPos(4,_pos);
            _Itemspos = new List<(int, int)>();
            initPos(_player.Items.Count,_Itemspos);
            _Movespos = new List<(int, int)>();
            initPos(_player.CurrentCreature.Moves.Count,_Movespos);
            _Teampos = new List<(int, int)>();
            initPos(_player.Creatures.Count,_Teampos);
            _currentMousePos = _pos[0];
            _inputManager = new InputManager();
            _offSet = 8;
        }


        public void initPos(int max,List<(int,int)> list)
        {
            for (int i = 1; i <= max; i++)
            {
                if (i % 2 == 1)
                {
                    list.Add((22, i + 3 + _offSet));
                }
                else if (i % 2 == 0)
                {
                    list.Add((71, i + 2 + _offSet));
                }
            }
        }

        public void Fighting()
        {
            bool playerSwap = false;
            bool enemySwap = false;
            Console.SetCursorPosition(8,3);
            Console.WriteLine("Ally Creature :" + _player.CurrentCreature.Stats.health + " / " + _player.CurrentCreature.Stats.maxHealth);
            HealthBar(_player,(9,4));
            Console.SetCursorPosition(73, 3);
            Console.WriteLine("Enemy Creature :" + _enemy.CurrentCreature.Stats.health + " / " + _enemy.CurrentCreature.Stats.maxHealth);
            HealthBar(_enemy, (74, 4));
            CurrentTurn += 1;
            PlayerChoice();
            if (_player.CurrentCreature.Stats.AttackSpeed > _enemy.CurrentCreature.Stats.AttackSpeed)
            {
                PlayerTurn();
                if (_enemy.CurrentCreature.State == CreatureState.DEAD)
                {
                    Console.WriteLine("{0} is dead.", _enemy.CurrentCreature.CreatureName);
                    Console.WriteLine();
                    enemySwap = true;
                }
                else
                {
                    EnemyTurn();
                }
            }
            else
            {
                EnemyTurn();
                if (_player.CurrentCreature.State == CreatureState.DEAD)
                {
                    Console.WriteLine("{0} is dead.", _player.CurrentCreature.CreatureName);
                    Console.WriteLine();
                    playerSwap = true;
                }
                else
                {
                    PlayerTurn();
                }
            }
            if (playerSwap == true)
            {
                AutoSwap(_player, _player.CurrentCreature);
            }
            if (enemySwap == true)
            {
                AutoSwap(_enemy, _enemy.CurrentCreature);
            }
            CheckForStatusEffects(_player.CurrentCreature, _enemy.CurrentCreature);
            FightEnd();
        }

        public void HealthBar(Fighter fighter,(int,int) pos)
        {
            float hp = (fighter.CurrentCreature.Stats.health / fighter.CurrentCreature.Stats.maxHealth) * 100 * 2;
            int nbCarre = (int)hp / 10;
            Console.SetCursorPosition(pos.Item1,pos.Item2);
            Console.WriteLine("--------------------");
            Console.SetCursorPosition(pos.Item1,pos.Item2+1);
            for (int i = 0; i < nbCarre; i++)
            {
                Console.Write("■");
            }
            Console.SetCursorPosition(pos.Item1, pos.Item2+2);
            Console.WriteLine("--------------------");
        }

        public void CheckForStatusEffects(Creatures allyCreature, Creatures enemyCreature)
        {
            if (allyCreature.State != CreatureState.ALIVE && allyCreature.State != CreatureState.DEAD) 
            {
                Random random = new Random();
                int chance = random.Next(1, 101);

                if (chance > 20)
                {
                    if (allyCreature.Stats.health > 0)
                    {
                        allyCreature.State = CreatureState.ALIVE;
                        Console.WriteLine("{0} is now cured.", allyCreature.CreatureName);
                        Console.WriteLine();
                    }
                    else
                    {
                        allyCreature.State = CreatureState.DEAD;
                    }
                }
            }
            switch (allyCreature.State)
            {
                case CreatureState.BURNED:
                    CreatureStats creatureStats = allyCreature.Stats;
                    creatureStats.health -= allyCreature.Stats.maxHealth * 0.10f;
                    allyCreature.Stats = creatureStats;
                    Console.Write(allyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = allyCreature.Stats;
                    creatureStats2.health -= allyCreature.Stats.maxHealth * 0.10f;
                    allyCreature.Stats = creatureStats2;
                    Console.Write(allyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
                    break;
            }
            if (enemyCreature.State != CreatureState.ALIVE && enemyCreature.State != CreatureState.DEAD)
            {
                Random random = new Random();
                int chance = random.Next(1, 101);

                if (chance > 20)
                {
                    if (enemyCreature.Stats.health > 0)
                    {
                        enemyCreature.State = CreatureState.ALIVE;
                        Console.WriteLine("{0} is now cured.", enemyCreature.CreatureName);
                        Console.WriteLine();
                    }
                    else
                    {
                        enemyCreature.State = CreatureState.DEAD;
                    }
                }
            }
            switch (enemyCreature.State)
            {
                case CreatureState.BURNED:
                    CreatureStats creatureStats = enemyCreature.Stats;
                    creatureStats.health -= enemyCreature.Stats.maxHealth * 0.10f;
                    enemyCreature.Stats = creatureStats;
                    Console.Write(enemyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
                    break;
                case CreatureState.POISONED:
                    CreatureStats creatureStats2 = enemyCreature.Stats;
                    creatureStats2.health -= enemyCreature.Stats.maxHealth * 0.10f;
                    enemyCreature.Stats = creatureStats2;
                    Console.Write(enemyCreature.CreatureName);
                    Console.WriteLine(" lost 10% HP due to burn.");
                    Console.WriteLine();
                    break;
            }
        }

        public void SwapCreature(Fighter fighter,int index) 
        {
           if(index <= fighter.Creatures.Count)
            {
                fighter.CurrentCreature = fighter.Creatures[index];
            }
        }

        public void FightEnd()
        {
            if (_enemy.Creatures.All(Creatures => Creatures.State == CreatureState.DEAD))
            {
                IsOver = true;
                IsWin = true;
                Win(_player, _enemy);
            }
            else if (_player.Creatures.All(Creatures => Creatures.State == CreatureState.DEAD))
            {
                IsOver = true;
                IsWin = false;
                Win(_enemy, _player);
            }

        }

        public bool AreAllDigits(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }
            return true;
        }

        public void Draw((int, int) currentPos,List<string> text,List<(int,int)> pos)
        {
            Console.SetCursorPosition(0, 0+_offSet);
            Console.CursorVisible = false;
            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            for (int i = 0; i < text.Count; i++)
            {
                Console.WriteLine("\\                                                                                                               /");
                Console.Write("\\");
                Console.SetCursorPosition(pos[i].Item1 + 1, pos[i].Item2+ _offSet);
                Console.WriteLine(text[i]);
            }
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
            Console.SetCursorPosition(currentPos.Item1, currentPos.Item2 + _offSet);
            Console.Write(">");

        }

        public void DrawClear()
        {
            Console.SetCursorPosition(0, 0 + _offSet);
            Console.CursorVisible = false;
            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\                                                                                                               /");
            Console.WriteLine("\\---------------------------------------------------------------------------------------------------------------/");
        }

        public void CleanCursor((int, int) currentPos)
        {
            Console.SetCursorPosition(currentPos.Item1, currentPos.Item2+ _offSet);
            Console.Write(" ");
        }
        public bool ChangePos(List<(int,int)> pos)
        {
            for (int i = 0; i < pos.Count;i++)
            {
                if(_inputManager.GetKeyDown(ConsoleKey.DownArrow)&& _currentMousePos == pos[i])
                {
                    if(i+2 < pos.Count)
                    {
                        CleanCursor(_currentMousePos);
                        _currentMousePos = pos[i+2];
                        return true;
                    }
                }
                else if(_inputManager.GetKeyDown(ConsoleKey.UpArrow) && _currentMousePos == pos[i])
                {
                    if(i-2 >= 0)
                    {
                        CleanCursor(_currentMousePos);
                        _currentMousePos = pos[i - 2];
                        return true;
                    }
                }
                else if(_inputManager.GetKeyDown(ConsoleKey.RightArrow) && _currentMousePos == pos[i])
                {
                    if(i+1 < pos.Count)
                    {
                        CleanCursor(_currentMousePos);
                        _currentMousePos = pos[i + 1];
                        return true;
                    }
                }
                else if (_inputManager.GetKeyDown(ConsoleKey.LeftArrow) && _currentMousePos == pos[i])
                {
                    if (i - 1 >= 0)
                    {
                        CleanCursor(_currentMousePos);
                        _currentMousePos = pos[i - 1];
                        return true;
                    }
                }
            }
            return false;
        }

        public void GetChoiceIndex()
        {
            _choiceIndex = _Movespos.IndexOf(_currentMousePos);
        }

        public void PlayerChoice()
        {
            DrawClear();
            string val = string.Empty;
            bool chosen = false;
            _inputManager.Update();
            List<string> FirstChoice = new List<string>();
            FirstChoice.Add("Combat");
            FirstChoice.Add("Items");
            FirstChoice.Add("Swap");
            FirstChoice.Add("Fuite");
            List<string> Combat = new List<string>();
            for (int i = 0; i < _player.CurrentCreature.Moves.Count(); i++)
            {
                if (_player.CurrentCreature.Moves[i] is Attack)
                {
                    Combat.Add(_player.CurrentCreature.Moves[i].MoveName + " - " + _player.CurrentCreature.Moves[i].PP + " / " + _player.CurrentCreature.Moves[i].Stats.MaxPP);
                }
                else
                {
                    Combat.Add(_player.CurrentCreature.Moves[i].MoveName + " - " + "Manacost : " + _player.CurrentCreature.Moves[i].Stats.ManaCost);
                }
            }

            List<string> Items = new List<string>();
            for (int i = 0; i < _player.Items.Count; i++)
            {
                Items.Add(_player.Items[i].name);
            }
            List<string> Team = new List<string>();
            for (int i = 0; i < _player.Creatures.Count; i++)
            {
                Team.Add(_player.Creatures[i].CreatureName);
            }
            Draw(_currentMousePos, FirstChoice,_pos);
            while (chosen == false)
            {
                if(val == string.Empty)
                {
                    
                    if (_inputManager.GetKeyDown(ConsoleKey.Enter) && _currentMousePos == _pos[0])
                    {
                        val = "Combat";
                        Console.Clear();
                        _currentMousePos = _Movespos[0];
                        Draw(_currentMousePos, Combat, _Movespos);
                    }
                    else if (_inputManager.GetKeyDown(ConsoleKey.Enter)&& _currentMousePos == _pos[1])
                    {
                        val = "Items";
                        Console.Clear();
                        _currentMousePos= _Itemspos[0];
                        Draw(_currentMousePos, Items, _Itemspos);
                    }
                    else if (_inputManager.GetKeyDown(ConsoleKey.Enter) && _currentMousePos == _pos[2])
                    {
                        val = "Swap";
                        Console.Clear();
                        _currentMousePos = _Teampos[0];
                        Draw(_currentMousePos, Team, _Teampos);
                    }
                    else if (_inputManager.GetKeyDown(ConsoleKey.Enter) && _currentMousePos == _pos[3])
                    {
                        val = "Fuite";
                        Console.Clear();
                    }
                    if (ChangePos(_pos))
                        Draw(_currentMousePos,FirstChoice, _pos);
                }
                else if(val == "Combat")
                {
                    ChangePos(_Movespos);
                    Draw(_currentMousePos, Combat,_Movespos);
                    if(_inputManager.GetKeyDown(ConsoleKey.Escape))
                    {
                        val = string.Empty;
                        Console.Clear();
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }
                    if(_inputManager.GetKeyDown(ConsoleKey.Enter))
                    {

                        _choiceType = PlayerChoices.Combat;
                        _choiceIndex = _Movespos.IndexOf(_currentMousePos);
                        chosen = true;
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }
                }
                else if(val =="Items")
                {
                    if(ChangePos(_Itemspos))
                    {
                        Draw(_currentMousePos, Items, _Itemspos);
                    }
                    if (_inputManager.GetKeyDown(ConsoleKey.Escape))
                    {
                        val = string.Empty;
                        Console.Clear();
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }
                    if (_inputManager.GetKeyDown(ConsoleKey.Enter))
                    {

                        _choiceType = PlayerChoices.Item;
                        _choiceIndex = _Movespos.IndexOf(_currentMousePos);
                        chosen = true;
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }

                }
                else if(val == "Swap")
                {
                    if (ChangePos(_Teampos))
                    {
                        Draw(_currentMousePos, Team, _Teampos);
                    }
                    if (_inputManager.GetKeyDown(ConsoleKey.Escape))
                    {
                        val = string.Empty;
                        Console.Clear();
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }
                    if (_inputManager.GetKeyDown(ConsoleKey.Enter))
                    {

                        _choiceType = PlayerChoices.Swap;
                        _choiceIndex = _Movespos.IndexOf(_currentMousePos);
                        chosen = true;
                        _currentMousePos = _pos[0];
                        Draw(_currentMousePos, FirstChoice, _pos);
                    }
                }
                else if(val == "Fuite")
                {
                    IsOver = true;
                }
                _inputManager.Update();
            }
        }

        public void PlayerTurn()
        {
            Console.SetCursorPosition(0, 15 +_offSet);
            if (_choiceType == PlayerChoices.Combat)
            {

                if (_player.CurrentCreature.Moves[_choiceIndex].PP > 0 || (_player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost != 0.0f && _player.CurrentCreature.Mana >= _player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost))
                {
                    float effectiveness = Moves.GetEffectiveness(_enemy.CurrentCreature.Stats.type, _player.CurrentCreature.Moves[_choiceIndex].Stats.Type, _types, _typeTable);
                    _player.CurrentCreature.Moves[_choiceIndex].Use(_player.CurrentCreature, _enemy.CurrentCreature, effectiveness);
                }
                else
                {
                    if (_player.CurrentCreature.Moves[_choiceIndex].PP <= 0 && _player.CurrentCreature.Moves[_choiceIndex].Stats.ManaCost == 0.0f)
                    {
                        Console.WriteLine("{0} has no PP left.", _player.CurrentCreature.Moves[_choiceIndex].MoveName);
                    }
                    else
                    {
                        Console.WriteLine("{0} has not enough mana to cast {1}.", _player.CurrentCreature.CreatureName, _player.CurrentCreature.Moves[_choiceIndex].MoveName);
                    }
                }
            }
            else if (_choiceType == PlayerChoices.Item)
            {
                _player.Items[_choiceIndex].Use(_player.CurrentCreature);

            }
            else if (_choiceType == PlayerChoices.Swap)
            {
                SwapCreature(_player, _choiceIndex);
            }
        }

        public void EnemyTurn()
        {
            Console.SetCursorPosition(0, 9 + _offSet);
            Console.WriteLine();
            _enemy.Turn(_enemy.CurrentCreature, _player.CurrentCreature);
        }

        public void AutoSwap(Fighter fighter,Creatures currentCreature)
        {
            for (int i = 0; i < fighter.Creatures.Count(); i++)
            {
                if (currentCreature == fighter.Creatures[i] && i + 1 < fighter.Creatures.Count() && fighter.Creatures[i + 1].State != CreatureState.DEAD)
                {
                    Console.WriteLine("{0} swapped {1} with {2}.", fighter.Name, currentCreature.CreatureName, fighter.Creatures[i + 1].CreatureName);
                    Console.WriteLine();
                    SwapCreature(fighter, i+1);
                    break;
                }
            }
        }

        public void Win(Fighter Winner, Fighter Looser)
        {
            Console.WriteLine("The winner is " +  Winner.Name);
            
            Console.WriteLine(Looser.Name + " has lost.");
        }



        #endregion Methods

    }
}
