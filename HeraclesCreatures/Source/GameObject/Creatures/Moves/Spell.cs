using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class Spell : Moves
    {

        /*------------------------------------------------------------------------------------------*\
       |                                                                                            |
       |                                                                                            |
       |                                          Fields                                            |
       |                                                                                            |
       |                                                                                            |
       \*------------------------------------------------------------------------------------------*/

        #region Fields

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Spell(string name)
        {
            MoveName = name;
        }
        public Spell(string name, MoveStats stats)
        {
            _stats = stats;
            _moveName = name;
        }

        #endregion Methods

    }
}
