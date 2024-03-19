using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Map
{
    internal class Cell
    {
        int _x;
        int _y;
        CellData_ _cellData;

        public Cell(int x, int y, CellData_ cellData)
        {
            _x = x;
            _y = y;
            _cellData = cellData;

        }
    }
}
