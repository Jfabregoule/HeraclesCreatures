using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

    #region Moves Class

    internal class Moves
    {

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                          Fields                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Fields

        string      _moveName;
        MoveStats   _stats;

        #endregion Fields

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                        Properties                                          |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Properties

        public string MoveName { get => _moveName; protected set => _moveName = value; }

        public MoveStats Stats { get => _stats; private set => _stats = value; }

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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Moves() 
        {
            _stats = new MoveStats();
            _moveName = string.Empty;
        }

        public Moves(string name, MoveStats stats)
        {
            _stats = stats;
            _moveName = name;
        }

        public virtual void Use(Creatures sender, Creatures receiver, float effectiveness)
        {

        }

        public float GetEffectiveness(string enemyType, List<string> types, float[,] typeTable)
        {
            int j = types.IndexOf(enemyType);
            int i = types.IndexOf(_stats.Type);
            return typeTable[i, j];
        }

        #endregion Methods

    }

    #endregion Moves Class

}
