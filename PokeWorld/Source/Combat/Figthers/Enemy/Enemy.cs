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

        int _difficulty;


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

        public Enemy(List<Creatures> team, int difficulty)
        {
            Creatures = team;
            Difficulty = difficulty;
        }

        public void Turn(Creatures EnnemyCreature, Creatures AllyCreature)
        {
            switch (Difficulty)
            {
                case 1:
                    EasyTurn(EnnemyCreature, AllyCreature);
                    break;
                case 2:
                    MediumTurn();
                    break;
                case 3:
                    HardTurn();
                    break;
                default:
                    MediumTurn();
                    break;
            }
        }

        public void EasyTurn(Creatures EnnemyCreature, Creatures AllyCreature)
        {
            Random random = new Random();

            int randomIndex = random.Next(0, EnnemyCreature.Moves.Count);
            EnnemyCreature.Moves[randomIndex].Use(EnnemyCreature, AllyCreature);
        }

        public void MediumTurn()
        {

        }

        public void HardTurn()
        {

        }

        #endregion Methods

    }
}
