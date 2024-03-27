using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    public class Spell : Moves
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

        public event Action<int> UsedSpell;

        #endregion Events

        /*------------------------------------------------------------------------------------------*\
        |                                                                                            |
        |                                                                                            |
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Spell(string name)
        {
            MoveName = name;
        }
        public Spell(string name, MoveStats stats) : base(name, stats)
        {
            _stats = stats;
            _moveName = name;
        }

        public Spell(MoveData moveData) : base(moveData)
        {
            Stats = moveData._stats;
            MoveName = moveData._moveName;
        }

        public override void Use(Creatures sender, Creatures receiver, float effectiveness)
        {
            Random random = new Random();
            int odds;
            switch (sender.State)
            {
                case CreatureState.DIZZY:
                    odds = 75;
                    break;
                case CreatureState.SHAKEN:
                    odds = 75;
                    break;
                case CreatureState.BLINDED:
                    odds = 60;
                    break;
                case CreatureState.SCARED:
                    odds = 50;
                    break;
                default:
                    odds = 100;
                    break;
            }

            int chance = random.Next(1, 100 + 1);
            if (chance <= odds)
            {
                float damage = (sender.Stats.attack + Stats.Power * 2) * effectiveness;
                switch (sender.State)
                {
                    case CreatureState.NUMB:
                        damage *= 0.8f;
                        break;
                    case CreatureState.WITHERED:
                        damage *= 0.75f;
                        break;
                }
                switch (receiver.State)
                {
                    case CreatureState.SOAKED:
                        damage *= 1.1f;
                        break;
                    case CreatureState.RUSTED:
                        damage *= 1.2f;
                        break;
                    case CreatureState.ERODED:
                        damage *= 1.25f;
                        break;
                }
                receiver.TakeDamage(damage);
                Console.WriteLine(sender.CreatureName + " attacks " + receiver.CreatureName + " with " + MoveName);
                Console.WriteLine(receiver.CreatureName + " HP : " + receiver.Stats.health);
                base.Use(sender, receiver, effectiveness);
                UsedSpell?.Invoke(_stats.ManaCost);
            }
            else
            {
                Console.WriteLine("Attack missed.");
            }
        }

        #endregion Methods

    }
}
