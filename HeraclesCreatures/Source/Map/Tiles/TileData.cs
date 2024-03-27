namespace HeraclesCreatures
{
    public struct TileData
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

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
        public TileData()
        {
            _isWalkable = true;
            _drawing = new char[4, 4];
            _foregroundColor = new ConsoleColor[4, 4];
            _backgroundColor = new ConsoleColor[4, 4];
        }

        public void printCellData()
        {
            Console.WriteLine("Cell Data:");
            Console.WriteLine($"Is Walkable: {IsWalkable}");

            Console.WriteLine("Drawing with Colors:");
            for (int i = 0; i < _drawing.GetLength(0); i++)
            {
                for (int j = 0; j < _drawing.GetLength(1); j++)
                {
                    Console.ForegroundColor = _foregroundColor[i, j];
                    Console.BackgroundColor = _backgroundColor[i, j];
                    Console.Write(_drawing[i, j]);
                }
                Console.ResetColor(); 
                Console.WriteLine(); 
            }
        }

        #endregion Methods

    }
}
