using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.Common
{
    internal class SaveManager
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

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public static GameData Load(string filename)
        {
            string saveFolderPath = Path.Combine("..", "..", "..", "Resources", "Save");
            string fullPath = Path.Combine(saveFolderPath, filename);

            if (File.Exists(fullPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    return (GameData)formatter.Deserialize(stream);
                }
            }
            else
            {
                Console.WriteLine("Aucun fichier de sauvegarde trouvé.");
                return null;
            }
        }

        #endregion Methods

    }
}
