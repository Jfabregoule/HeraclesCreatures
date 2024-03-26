using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class Enemy : Fighter
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

        public Enemy(string name,List<Creatures> team, int difficulty, List<string> types, float[,] typeTable)
        {
            Name = name;
            Creatures = team;
            Difficulty = difficulty;
            _types = types;
            _typeTable = typeTable;
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
                    HardTurn();
                    break;
                default:
                    MediumTurn(EnemyCreature, AllyCreature);
                    break;
            }
        }

        private void EasyTurn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            Random random = new Random();

            int randomIndex = random.Next(0, EnemyCreature.Moves.Count);
            float effectiveness = EnemyCreature.Moves[randomIndex].GetEffectiveness(AllyCreature.Stats.type, _types, _typeTable);
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
                    if ((EnemyCreature.Moves[i] is Attack && EnemyCreature.Moves[i].PP > 0) || (EnemyCreature.Moves[i] is Spell && EnemyCreature.Mana > EnemyCreature.Moves[i].Stats.ManaCost))
                    bestPower = power;
                    bestMoveIndex = i;
                    canMove = true;
                }
            }
            if (canMove)
            {
                float effectiveness = EnemyCreature.Moves[bestMoveIndex].GetEffectiveness(EnemyCreature.Stats.type, _types, _typeTable);
                EnemyCreature.Moves[bestMoveIndex].Use(EnemyCreature, AllyCreature, effectiveness);
            }
            else
            {
                Console.WriteLine("{0} has no PP and not enough mana remaining and couldn't move", EnemyCreature.CreatureName);
            }
        }

        private void HardTurn()
        {

        }

        #endregion Methods

    }
}
