using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Map
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

        int _width;
        int _height;
        List<Cell> _cells;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties



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

        public Scene(int width, int height)
        {

            string solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string relativePath = @"Map\Scenes\RandomScene.txt";
            string mapFilePath = Path.Combine(solutionDirectory, relativePath);
            string[] mapLines = File.ReadAllLines(mapFilePath);

            string solutionDirectory2 = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string relativePath2 = @"Map\MapObjects\Player.txt";
            string mapFilePath2 = Path.Combine(solutionDirectory2, relativePath2);
            string[] playerLines = File.ReadAllLines(mapFilePath2);
            string[] playerDrawing = new string[4];
            for (int i = 0; i < 5; i++)
            {
                playerDrawing[i] = playerLines[i + 1];
            }

            _width = mapLines[0].Length;
            _height = mapLines.Length;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    CellData_ cellData = new CellData_();
                    switch (mapLines[i][j])
                    {
                        case 'P':
                            cellData.IsWalkable = true;
                            cellData.CellContent = new MapObject();
                            cellData.Drawing = playerDrawing;
                            break;
                        default:
                            break;
                    }
                    _cells.Add(new Cell(i, j, new CellData_()));
                }
            }
        }

        #endregion Methods


    }
}
