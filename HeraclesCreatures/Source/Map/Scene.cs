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
        Cell[,] _cells;
        MapObject[,] _sceneObjects;

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
        public Cell[,] Cells { get => _cells; set => _cells = value; }
        public MapObject[,] SceneObjects { get => _sceneObjects; set => _sceneObjects = value; }

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
            _cells = new Cell[,] {};
            _sceneObjects = new MapObject[,] {};
        }

        public Scene(string name, int width, int height, Cell[,] cells, MapObject[,] sceneObjects)
        {
            _name = name;
            _width = width;
            _height = height;
            _cells = cells;
            _sceneObjects = sceneObjects;
        }

        public void AddMapObject(MapObject obj)
        {
            foreach (MapObject mapObject in _sceneObjects)
            {
                if (mapObject == null) continue;
                if (mapObject.X == obj.X && mapObject.Y == obj.Y)
                    return;
            }
            _sceneObjects[obj.X,obj.Y] = obj;
        }

        public void UpdateCharacter(MapObject character)
        {
            for (int i = 0; i < _sceneObjects.GetLength(0); i++)
            {
                for (int j = 0; j < _sceneObjects.GetLength(1); j++)
                {
                    MapObject obj = _sceneObjects[i, j];

                    if (obj is Character)
                    {
                        _sceneObjects[i, j] = null;
                        _sceneObjects[character.X, character.Y] = character;
                    }
                }
            }
        }

        public void DisplayScene()
        {
            
            for (int i = 0; i < Height*5; i++)
            {
                for(int j = 0; j < Width*10; j++)
                {
                    Cell cell = Cells[(int)Math.Floor(i * 0.2), (int)Math.Floor(j * 0.1)];
                    Console.BackgroundColor = cell.Tile.BackgroundColor[i % 5, j % 10];

                    MapObject obj = _sceneObjects[(int)Math.Floor(i * 0.2), (int)Math.Floor(j * 0.1)];
                    if (obj != null)
                    {
                        if (obj is Character)
                        {
                            Character child = (Character)obj;
                            Console.ForegroundColor = child.Data.MapData.ForegroundColor[i % 5, j % 10];
                            Console.Write(child.Data.MapData.Drawing[i % 5, j % 10]);
                        }
                        else if (obj is Chest)
                        {
                            Chest child = (Chest)obj;
                            Console.ForegroundColor = child.Data.MapData.ForegroundColor[i % 5, j % 10];
                            Console.Write(child.Data.MapData.Drawing[i % 5, j % 10]);
                        }
                        else if (obj is Door)
                        {
                            Door child = (Door)obj;
                            Console.ForegroundColor = child.Data.MapData.ForegroundColor[i % 5, j % 10];
                            Console.Write(child.Data.MapData.Drawing[i % 5, j % 10]);
                        }
                        else
                        {
                            Console.WriteLine("This object is not a child class.");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = cell.Tile.ForegroundColor[i % 5, j % 10];
                        Console.Write(cell.Tile.Drawing[i % 5, j % 10]);
                    }
                    
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        #endregion Methods

    }
}
