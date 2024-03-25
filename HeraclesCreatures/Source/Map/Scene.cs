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

        string name;
        int _width;
        int _height;
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

        public string Name { get => name; set => name = value; }
        public int Width { get => _width; set => _width = value; }
        public int Height { get => _height; set => _height = value; }
        public Cell[,] Cells { get => _cells; set => _cells = value; }
        internal List<MapObject> SceneObjects { get => _sceneObjects; set => _sceneObjects = value; }

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
            _width = 0;
            _height = 0;
            _cells = new Cell[,] {};
        }

        public Scene(int width, int height, Cell[,] cells)
        {
            _width = width;
            _height = height;
            _cells = cells;
        }

        private void SetCell(int x, int y, Cell cell)
        {
            _cells[x, y] = cell;
        }

        #endregion Methods

    }
}
