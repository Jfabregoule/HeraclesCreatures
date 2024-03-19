using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.GameObject.Creatures.Moves
{
    internal class SpellStats
    {

        /*------------------------------------------------------------------------------------------*\
      |                                                                                            |
      |                                                                                            |
      |                                          Fields                                            |
      |                                                                                            |
      |                                                                                            |
      \*------------------------------------------------------------------------------------------*/

        #region Fields

        int     _power;
        int     _accuracy;
        int     _critRate;
        int     _manaCost;
        string  _type;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public int Power { get => _power; private set => _power = value; }

        public int Accuracy { get => _accuracy; private set => _accuracy = value; }

        public int CritRate { get => _critRate; private set => _critRate = value; }

        public int ManaCost { get => _manaCost; private set => _manaCost = value; }

        public string Type { get => _type; private set => _type = value; }

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

        public SpellStats() 
        {
            _power = 0;
            _accuracy = 0;
            _critRate = 0;
            _manaCost = 0;
            _type = string.Empty;
        }

        #endregion Methods

    }
}
