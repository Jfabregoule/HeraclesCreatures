using HeraclesCreatures;
using System.ComponentModel;
using System.Xml.Linq;

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
        List<int[]> _toRemove;

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
        public List<int[]> ToRemove { get => _toRemove; set => _toRemove = value; }

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
            _toRemove = new List<int[]> { };
        }

        public Scene(string name, int width, int height, Cell[,] cells, MapObject[,] sceneObjects)
        {
            _name = name;
            _width = width;
            _height = height;
            _cells = cells;
            _sceneObjects = sceneObjects;
            _toRemove = new List<int[]> { };
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

        public void RemoveMapObject(MapObject obj)
        {
            foreach (MapObject mapObject in _sceneObjects) 
            { 
                if (mapObject == null) continue;
                if (mapObject == obj)
                {
                    _sceneObjects[obj.X, obj.Y] = null;
                }
            }
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
        public bool IsMapObject(int x, int y)
        {
            return _sceneObjects[x,y] != null;
        }

        public void DisplayScene()
        {
            
            for (int i = 0; i < Height*5; i++)
            {
                for(int j = 0; j < Width*10; j++)
                {
                    Cell cell = Cells[(int)Math.Floor(i * 0.2), (int)Math.Floor(j * 0.1)];
                    Console.BackgroundColor = cell.Tile.BackgroundColor[i % 5, j % 10];
                    Console.ForegroundColor = cell.Tile.ForegroundColor[i % 5, j % 10];
                    Console.Write(cell.Tile.Drawing[i % 5, j % 10]);
 
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayMapObjects()
        {
            foreach (int[] coords in _toRemove)
            {
                DrawMapObject(coords[0] * 5, coords[1] * 10, Cells[coords[0], coords[1]].Tile.Drawing, Cells[coords[0], coords[1]].Tile.ForegroundColor, Cells[coords[0], coords[1]].Tile.BackgroundColor);
            }
            for (int i = 0; i < _sceneObjects.GetLength(0); i++)
            {
                for (int j = 0; j < _sceneObjects.GetLength(1); j++)
                {
                    MapObject obj = _sceneObjects[i, j];
                    if (obj != null)
                    {
                        if (obj is Character)
                        {
                            Character child = (Character)obj;
                            DrawMapObject(i * 5, j * 10, child.Data.MapData.Drawing, child.Data.MapData.ForegroundColor, Cells[i,j].Tile.BackgroundColor);
                        }
                        else if (obj is Opponent)
                        {
                            Opponent child = (Opponent)obj;
                            DrawMapObject(i * 5, j * 10, child.Data.MapData.Drawing, child.Data.MapData.ForegroundColor, Cells[i, j].Tile.BackgroundColor);
                        }
                        else if (obj is Chest)
                        {
                            Chest child = (Chest)obj;
                            DrawMapObject(i * 5, j * 10, child.Data.MapData.Drawing, child.Data.MapData.ForegroundColor, Cells[i, j].Tile.BackgroundColor);
                        }
                        else if (obj is Door)
                        {
                            Door child = (Door)obj;
                            DrawMapObject(i * 5, j * 10, child.Data.MapData.Drawing, child.Data.MapData.ForegroundColor, Cells[i, j].Tile.BackgroundColor);
                        }
                        else
                        {
                            Console.WriteLine("This object is not a child class.");
                        }
                    }
                }
            }
            _toRemove = new();
        }

        public void DrawMapObject(int startX, int startY, char[,] drawing, ConsoleColor[,] fgcolor, ConsoleColor[,] bgcolor)
        {
            // Set initial cursor position
            Console.SetCursorPosition(startY, startX);

            // Draw the map
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Console.BackgroundColor = bgcolor[i, j];
                    Console.ForegroundColor = fgcolor[i,j];
                    Console.Write(drawing[i,j]);
                }
                Console.SetCursorPosition(startY, Console.CursorTop + 1);
            }
        }

        public void ResetDisplay()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            DisplayScene();
            DisplayMapObjects();
        }

        #endregion Methods

    }
}
