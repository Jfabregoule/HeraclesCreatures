using HeraclesCreatures.Source.GameObject.Creatures.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.GameObject.Creatures
{

    #region Creatures Class

    internal class Creatures
    {
        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        string              _creatureName;
        List<Moves.Moves>   _moves; 
        CreatureStats       _Stats;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public string CreatureName { get => _creatureName; private set => _creatureName = value; }
        internal List<Moves.Moves> Moves { get => _moves; private set => _moves = value; }
        public CreatureStats Stats { get => _Stats; private set => _Stats = value; }

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

        public Creatures(string CreatureName)
        {
            _creatureName = CreatureName;
            _Stats = new CreatureStats();
        }

        public void TakeDamage(int damage)
        {
            _Stats.health -= damage;
        }

        public void Heal(int value)
        {
            if(_Stats.health + value >= _Stats.maxHealth)
            {
                _Stats.health = _Stats.maxHealth;
            }
            else
            {
                _Stats.health += value; 
            }
        }

        public void AddMove(ref Moves.Moves move) 
        {
            _moves.Add(move);
        }

        public bool IsDead()
        {
            if(_Stats.health<= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion Methods

    }

    #endregion Creatures Class

}
