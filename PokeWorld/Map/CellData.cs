using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Map
{
    public struct CellData_
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

        public MapObject CellContent { get => _cellContent; set => _cellContent = value; }

        public bool IsWalkable { get => _isWalkable; set => _isWalkable = value; }

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

        // Constructors
        public CellData_()
        {
            _cellContent = new MapObject();
            _isWalkable = true;
            _drawing = new char[4, 4];
            _foregroundColor = new ConsoleColor[4, 4];
            _backgroundColor = new ConsoleColor[4, 4];
        }
        public CellData_(MapObject mapObejct, bool isWalkable, char[,] drawing, ConsoleColor[,] foregroundColor, ConsoleColor[,] backgroundColor)
        {
            _cellContent = mapObejct;
            _isWalkable = isWalkable;
            _drawing = drawing;
            _foregroundColor = foregroundColor;
            _backgroundColor = backgroundColor;
        }

        public void printCellData()
        {
            Console.WriteLine("Cell Data:");
            Console.WriteLine($"Cell Content: {CellContent}");
            Console.WriteLine($"Is Walkable: {IsWalkable}");

            // Print the drawing with foreground and background colors
            Console.WriteLine("Drawing with Colors:");
            for (int i = 0; i < _drawing.GetLength(0); i++)
            {
                for (int j = 0; j < _drawing.GetLength(1); j++)
                {
                    Console.ForegroundColor = _foregroundColor[i, j];
                    Console.BackgroundColor = _backgroundColor[i, j];
                    Console.Write(_drawing[i, j]);
                }
                Console.ResetColor(); // Reset colors after each row
                Console.WriteLine(); // Move to the next line
            }
        }

        #endregion Methods

    }
}
