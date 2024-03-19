using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Map
{
    internal struct CellData_
    {
        MapObject _cellContent;
        bool _isWalkable;
        char[] _drawing;
        ConsoleColor[] _foregroundColor;
        ConsoleColor[] _backgroundColor;
        public CellData_(MapObject mapObejct)
        {
            _cellContent = mapObejct;
            _isWalkable = true;
            _drawing = new char[8];
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
    }
}
