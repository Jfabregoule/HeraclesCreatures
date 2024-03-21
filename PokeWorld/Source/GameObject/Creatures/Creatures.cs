using HeraclesCreatures;

namespace HeraclesCreatures
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

        string _creatureName;
        //List<Moves>   _Moves;
        //List<Spells>  _Spells;
        CreatureStats     _Stats;

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
        public CreatureStats stats { get => _Stats; }

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
