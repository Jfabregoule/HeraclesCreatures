using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    public class SaveManager
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields



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

        public static void Save(GameData data, string filename)
        {
            string saveFolderPath = Path.Combine("..", "..", "..", "Resources", "Save");
            string fullPath = Path.Combine(saveFolderPath, filename);

            var content = JsonSerializer.Serialize(data);

            File.WriteAllText(fullPath, content);
        }

        public static GameData Load(string filename)
        {
            string saveFolderPath = Path.Combine("..", "..", "..", "Resources", "Save");
            string fullPath = Path.Combine(saveFolderPath, filename);

            if (!File.Exists(fullPath))
            {
                throw new ArgumentException("There is no file named as {0}.", filename);
            }

            // Lire le contenu JSON du fichier
            string content = File.ReadAllText(fullPath);

            // Désérialiser le contenu JSON en un objet GameData
            GameData data = JsonSerializer.Deserialize<GameData>(content);

            return data;
        }

        #endregion Methods

    }
}
