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

        internal List<Creatures> Team { get => _team; set => _team = value; }


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
            Team = team;
            _difficulty = difficulty;
        }

        public void Turn (Creatures EnnemyCreature,Creatures AllyCreature)
        {
            switch (_difficulty)
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
            EnnemyCreature.Moves[randomIndex].Use(Team[0], AllyCreature);
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
