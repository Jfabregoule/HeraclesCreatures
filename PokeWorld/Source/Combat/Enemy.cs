using HeraclesCreatures.Source.GameObject.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.Combat
{
    internal class Enemy
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        List<Creatures> _team;
        int             _difficulty;

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
        |                                          Methods                                           |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Enemy(List<Creatures> team, int difficulty) 
        {
            _team = team;
            _difficulty = difficulty;
        }

        public void Turn ()
        {
            switch (_difficulty)
            {
                case 1:
                    EasyTurn();
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

        public void EasyTurn()
        {
            Random random = new Random();

            int randomIndex = random.Next(0, _team[0].Moves.Count);
            _team[0].Moves[randomIndex].Use();
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
