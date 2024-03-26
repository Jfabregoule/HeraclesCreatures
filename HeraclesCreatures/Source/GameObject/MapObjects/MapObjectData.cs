using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public struct MapObjectData
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        char[,] _drawing;
        ConsoleColor[,] _foregroundColor;
        ConsoleColor[,] _backgroundColor;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public char[,] Drawing { get => _drawing; set => _drawing = value; }
        public ConsoleColor[,] ForegroundColor { get => _foregroundColor; set => _foregroundColor = value; }
        public ConsoleColor[,] BackgroundColor { get => _backgroundColor; set => _backgroundColor = value; }

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

        public MapObjectData()
        {
            _drawing = new char[4, 4];
            _foregroundColor = new ConsoleColor[4, 4];
            _backgroundColor = new ConsoleColor[4, 4];
        }

        #endregion Methods

    }
}
