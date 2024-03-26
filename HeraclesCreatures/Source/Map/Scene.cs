using HeraclesCreatures;
using System.ComponentModel;

namespace HeraclesCreatures
{
    public class Scene
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        string _name;
        int _width;
        int _height;
        int _defaultX;
        int _defaultY;
        Cell[,] _cells;
        List<MapObject> _sceneObjects;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public string Name { get => _name; set => _name = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public int DefaultX { get => _defaultX; set => _defaultX = value; }
        public int DefaultY { get => _defaultY; set => _defaultY = value; }
        public Cell[,] Cells { get => _cells; set => _cells = value; }
        public List<MapObject> SceneObjects { get => _sceneObjects; set => _sceneObjects = value; }

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

        public Scene()
        {
            _name = "";
            _width = 0;
            _height = 0;
            _defaultX = 0;
            _defaultY = 0;
            _cells = new Cell[,] {};
            _sceneObjects = new();
        }

        public Scene(string name, int width, int height, int defaultX, int defaultY, Cell[,] cells, List<MapObject> sceneObjects)
        {
            _name = name;
            _width = width;
            _height = height;
            _defaultX = defaultX;
            _defaultY = defaultY;
            _cells = cells;
            _sceneObjects = sceneObjects;
        }

        private void SetCell(int x, int y, Cell cell)
        {
            _cells[x, y] = cell;
        }

        public void DisplayScene()
        {
            for (int i = 0; i < Height*5; i++)
            {
                for(int j = 0; j < Width*10; j++)
                {
                    Cell cell = Cells[(int)Math.Floor(i * 0.2), (int)Math.Floor(j * 0.1)];
                    Console.ForegroundColor = cell.Tile.ForegroundColor[i % 5, j % 10];
                    Console.BackgroundColor = cell.Tile.BackgroundColor[i % 5, j % 10];
                    Console.Write(cell.Tile.Drawing[i % 5, j % 10]);
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = 0;
        }

        #endregion Methods

    }
}
