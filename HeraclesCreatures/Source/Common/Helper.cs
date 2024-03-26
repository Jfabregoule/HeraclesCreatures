using HeraclesCreatures;

namespace HeraclesCreatures
{
    public static class Helper
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

        public static ConsoleColor toColor(string color)
        {
            switch (color)
            {
                case "Black ":
                    return ConsoleColor.Black;
                case "DarkBlue ":
                    return ConsoleColor.DarkBlue;
                case "DarkGreen":
                    return ConsoleColor.DarkGreen;
                case "DarkCyan ":
                    return ConsoleColor.DarkCyan;
                case "DarkRed ":
                    return ConsoleColor.DarkRed;
                case "DarkMagenta ":
                    return ConsoleColor.DarkMagenta;
                case "DarkYellow ":
                    return ConsoleColor.DarkYellow;
                case "Gray":
                    return ConsoleColor.Gray;
                case "DarkGray ":
                    return ConsoleColor.DarkGray;
                case "Blue ":
                    return ConsoleColor.Blue;
                case "Green ":
                    return ConsoleColor.Green;
                case "Cyan ":
                    return ConsoleColor.Cyan;
                case "Red ":
                    return ConsoleColor.Red;
                case "Magenta ":
                    return ConsoleColor.Magenta;
                case "Yellow  ":
                    return ConsoleColor.Yellow;
                case "White  ":
                    return ConsoleColor.White;
                default:
                    return 0;
            }
        }

        #endregion Methods

    }
}
