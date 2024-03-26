using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public class Potion : Items
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        int _health;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int health { get {return _health; } }

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


        public Potion() 
        {
            name = "Potion";
            _health = 10;
        }
        public override void Use(Creatures creatures)
        {
            creatures.Heal(_health);
            Console.WriteLine("Vous venez de soigner" + creatures.CreatureName);
            Console.WriteLine(creatures.CreatureName + " HP : " +creatures.Stats.health);
        }

        #endregion Methods
    }
}
