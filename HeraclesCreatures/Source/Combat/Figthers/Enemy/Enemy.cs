using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class Enemy : Fighter
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        int             _difficulty;
        List<string>    _types;
        float[,]        _typeTable;


        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int Difficulty { get => _difficulty; set => _difficulty = value; }


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

        public Enemy()
        {
            _difficulty = 0;
        }

        public Enemy(string name,List<Creatures> team, int difficulty, List<string> types, float[,] typeTable)
        {
            Name = name;
            Creatures = team;
            Difficulty = difficulty;
            _types = types;
            _typeTable = typeTable;
            if (team.Count > 0)
            {
                CurrentCreature = team[0];
            }
        }

        public void Turn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            switch (Difficulty)
            {
                case 1:
                    EasyTurn(EnemyCreature, AllyCreature);
                    break;
                case 2:
                    MediumTurn(EnemyCreature, AllyCreature);
                    break;
                case 3:
                    HardTurn(EnemyCreature, AllyCreature);
                    break;
                default:
                    MediumTurn(EnemyCreature, AllyCreature);
                    break;
            }
        }

        private void EasyTurn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            bool moveIsAvailable = false;
            int randomIndex = 0;
            while (moveIsAvailable == false)
            {
                Random random = new Random();

                randomIndex = random.Next(0, EnemyCreature.Moves.Count);
                if ((EnemyCreature.Moves[randomIndex] is Attack && EnemyCreature.Moves[randomIndex].PP > 0) || (EnemyCreature.Moves[randomIndex] is Spell && EnemyCreature.Stats.Mana > EnemyCreature.Moves[randomIndex].Stats.ManaCost))
                {
                    moveIsAvailable = true;
                }
            }
            float effectiveness = Moves.GetEffectiveness(AllyCreature.Stats.type, EnemyCreature.Moves[randomIndex].Stats.Type, _types, _typeTable);
            EnemyCreature.Moves[randomIndex].Use(EnemyCreature, AllyCreature, effectiveness);
        }

        private void MediumTurn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            bool canMove = false;
            int bestMoveIndex = 0;
            int bestPower = 0;
            for (int i = 0; i < EnemyCreature.Moves.Count; i++)
            {
                int power = EnemyCreature.Moves[i].Stats.Power;
                if (power > bestPower)
                {
                    if ((EnemyCreature.Moves[i] is Attack && EnemyCreature.Moves[i].PP > 0) || (EnemyCreature.Moves[i] is Spell && EnemyCreature.Stats.Mana > EnemyCreature.Moves[i].Stats.ManaCost))
                    bestPower = power;
                    bestMoveIndex = i;
                    canMove = true;
                }
            }
            if (canMove)
            {
                float effectiveness = Moves.GetEffectiveness(EnemyCreature.Stats.type, EnemyCreature.Moves[bestMoveIndex].Stats.Type, _types, _typeTable);
                EnemyCreature.Moves[bestMoveIndex].Use(EnemyCreature, AllyCreature, effectiveness);
            }
            else
            {
                Console.WriteLine("{0} has no PP and not enough mana remaining and couldn't move", EnemyCreature.CreatureName);
            }
        }

        private void HardTurn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            if (Moves.GetEffectiveness(AllyCreature.Stats.type, EnemyCreature.Stats.type, _types, _typeTable) == 2 || EnemyCreature.Stats.health < EnemyCreature.Stats.maxHealth * 0.8f)
            {
                Random random = new Random();
                int swapChance = random.Next(0, 101);
                if (swapChance < 50)
                {
                    HardSwap(AllyCreature);
                }
            }
            bool canMove = false;
            int bestMoveIndex = 0;
            int bestPower = 0;
            for (int i = 0; i < EnemyCreature.Moves.Count; i++)
            {
                int power = EnemyCreature.Moves[i].Stats.Power;
                if (power > bestPower)
                {
                    if ((EnemyCreature.Moves[i] is Attack && EnemyCreature.Moves[i].PP > 0) || (EnemyCreature.Moves[i] is Spell && EnemyCreature.Stats.Mana > EnemyCreature.Moves[i].Stats.ManaCost))
                        bestPower = power;
                    bestMoveIndex = i;
                    canMove = true;
                }
            }
            if (canMove)
            {
                float effectiveness = Moves.GetEffectiveness(EnemyCreature.Stats.type, EnemyCreature.Moves[bestMoveIndex].Stats.Type, _types, _typeTable);
                EnemyCreature.Moves[bestMoveIndex].Use(EnemyCreature, AllyCreature, effectiveness);
            }
            else
            {
                Console.WriteLine("{0} has no PP and not enough mana remaining and couldn't move", EnemyCreature.CreatureName);
            }
        }

        private void HardSwap(Creatures allyCreature)
        {
            for (int i = 0; i < Creatures.Count; i++)
            {
                if (Moves.GetEffectiveness(Creatures[i].Stats.type, allyCreature.Stats.type, _types, _typeTable) == 2)
                {
                    string oldName = CurrentCreature.CreatureName;
                    CurrentCreature = Creatures[i];
                    Console.WriteLine("{0} switched {1} to {2}.", Name, oldName, CurrentCreature.CreatureName);
                }
            }
        }

        public void Regenerate()
        {
            for (int i = 0; i < Creatures.Count - 1; i++)
            {
                Creatures[i].ResetCreature();
                for (int j = 0; Creatures[i].Moves.Count > j; j++)
                {
                    Creatures[i].Moves[j].MaximizePP();
                }
            }
            CurrentCreature = Creatures[0];
        }

        #endregion Methods

    }
}
