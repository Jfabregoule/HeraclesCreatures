using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    internal class Grass : MapObject
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        List<string> _possibleEnemies;
        GrassData _data;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public List<string> Encounters { get => _possibleEnemies; set => _possibleEnemies = value; }
        public GrassData Data { get => _data; set => _data = value; }

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

        public Grass()
        {
            _possibleEnemies = new();
            _data = new GrassData();
        }

        public Grass(int x, int y, List<string> possibleEnemies, GrassData data)
        {
            X = x;
            Y = y;
            _possibleEnemies = possibleEnemies;
            Data = data;
        }

        public bool IsEncounter()
        {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 100);

            return randomNumber < _data.EncounterRate;
        }

        public string GetRandomCreature()
        {
            Random rand = new Random();
            int index = rand.Next(0, Encounters.Count);

            return Encounters[index];
        }
        public override void PlayDialogue(Scene currentScene)
        {
            base.PlayDialogue(currentScene);
            Console.Write("You Are On Grass");
        }

        #endregion Methods

    }
}
