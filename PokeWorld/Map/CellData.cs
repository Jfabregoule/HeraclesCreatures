using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Map
{
    internal struct CellData_
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        MapObject _cellContent;
        bool _isWalkable;
        char[] _drawing;
        ConsoleColor[] _foregroundColor;
        ConsoleColor[] _backgroundColor;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public MapObject CellContent { get; set; }
        public bool IsWalkable { get; set; }
        public char[] Drawing 
        { 
            get
            {
                return _drawing;
            }
            set
            {
                _drawing = value;
            }
        }

        public ConsoleColor[] ForegroundColor
        {
            get
            {
                return _foregroundColor;
            }
            set
            {
                _foregroundColor = value;
            }
        }

        public ConsoleColor[] BackgroundColor
        {
            get
            {
                return _backgroundColor;
            }
            set
            {
                _backgroundColor = value;
            }
        }

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

        // Constructor
        public CellData_(MapObject mapObejct)
        {
            _cellContent = mapObejct;
            _isWalkable = true;
            _drawing = new char[24];
            for (int i = 0; i < _drawing.Length; i++)
            {
                _drawing[i] = '#';
            }

            _foregroundColor = new ConsoleColor[8];
            for (int i = 0; i < _foregroundColor.Length; i++)
            {
                _foregroundColor[i] = ConsoleColor.White;
            }
            _backgroundColor = new ConsoleColor[8];
            for (int i = 0; i < _backgroundColor.Length; i++)
            {
                _backgroundColor[i] = ConsoleColor.Black;
            }
        }

        #endregion Methods


    }
}
