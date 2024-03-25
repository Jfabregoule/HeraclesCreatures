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

        protected string      _moveName;
        protected MoveStats   _stats;

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
                Console.Write(" made ");
                Console.Write(receiver.CreatureName);
                Console.Write(" feel ");
                Console.WriteLine(receiver.State.ToString().ToLower());
            }
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
