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

        public Enemy(List<Creatures> team, int difficulty, List<string> types, float[,] typeTable)
        {
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
            EnemyCreature.Moves[randomIndex].Use(EnemyCreature, AllyCreature);
        }

        private void MediumTurn(Creatures EnemyCreature, Creatures AllyCreature)
        {
            string enemyType = EnemyCreature.Stats.type;
            int j = _types.IndexOf(enemyType);
            for (int i = 0; i < AllyCreature.Moves.Count; i++) 
            {
                
            }

        }

        private void HardTurn()
        {

        }

        #endregion Methods

    }
}
