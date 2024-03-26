using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{

    #region Moves Class
    [Serializable]
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

        protected string        _moveName;
        protected MoveStats     _stats;
        private int             _pp;

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
        public int PP { get => _pp; set => _pp = value; }

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
            _pp = stats.MaxPP;
        }

        public virtual void Use(Creatures sender, Creatures receiver, float effectiveness)
        {
            Random random = new Random();
            int chance = random.Next(1, 101);

            if (chance <= 20)
            {
                if (_stats.Type == "Normal")
                    receiver.State = CreatureState.NUMB;
                else if (_stats.Type == "Fire")
                    receiver.State = CreatureState.BURNED;
                else if (_stats.Type == "Water")
                    receiver.State = CreatureState.SOAKED;
                else if (_stats.Type == "Grass")
                    receiver.State = CreatureState.WITHERED;
                else if (_stats.Type == "Steel")
                    receiver.State = CreatureState.RUSTED;
                else if (_stats.Type == "Flying")
                    receiver.State = CreatureState.DIZZY;
                else if (_stats.Type == "Ground")
                    receiver.State = CreatureState.SHAKEN;
                else if (_stats.Type == "Dark")
                    receiver.State = CreatureState.BLINDED;
                else if (_stats.Type == "Rock")
                    receiver.State = CreatureState.ERODED;
                else if (_stats.Type == "Poison")
                    receiver.State = CreatureState.POISONED;
                else if (_stats.Type == "Ghost")
                    receiver.State = CreatureState.SCARED;
                Console.Write(sender.CreatureName);
                Console.WriteLine(receiver.State.ToString().ToLower());
                Console.Write(receiver.CreatureName);
                Console.WriteLine();
            }
        }

        public static float GetEffectiveness(string enemyType, string allyType, List<string> types, float[,] typeTable)
        {
            int j = types.IndexOf(enemyType);
            int i = types.IndexOf(allyType);
            return typeTable[i, j];
        }

        #endregion Methods

    }

    #endregion Moves Class

}
