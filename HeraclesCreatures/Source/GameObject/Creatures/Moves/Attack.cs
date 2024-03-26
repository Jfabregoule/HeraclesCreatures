﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures
{
    [Serializable]
    internal class Attack : Moves
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
        |                                         Methods                                            |
        |                                                                                            |
        |                                                                                            |
        \*------------------------------------------------------------------------------------------*/

        #region Methods

        public Attack(string name)
        {
            MoveName = name;
        }

        public Attack(string name, MoveStats stats) : base(name, stats)
        {
        }

        public override void Use(Creatures sender, Creatures receiver, float effectiveness)
        {
            Random random = new Random();
            float odds;
            switch (sender.State)
            {
                case CreatureState.DIZZY:
                    odds = (75.0f / 100 * Stats.Accuracy / 100) * 100;
                    break;
                case CreatureState.SHAKEN:
                    odds = (75.0f / 100 * Stats.Accuracy / 100) * 100;
                    break;
                case CreatureState.BLINDED:
                    odds = (60.0f / 100 * Stats.Accuracy / 100) * 100;
                    break;
                case CreatureState.SCARED:
                    odds = (50.0f / 100 * Stats.Accuracy / 100) * 100;
                    break;
                default:
                    odds = (100.0f / 100 * Stats.Accuracy / 100) * 100;
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
                int critChance = random.Next(1, 101);
                if (critChance < Stats.CritRate)
                {
                    damage *= 2;
                    Console.WriteLine("{0} was a critical hit", MoveName);
                }
                PP -= 1;
                receiver.TakeDamage(damage);
                Console.WriteLine(sender.CreatureName + " attacks " + receiver.CreatureName + " with " + MoveName);
                Console.WriteLine(receiver.CreatureName + " HP : " + receiver.Stats.health);
                base.Use(sender, receiver, effectiveness);
            }
            else
            {
                Console.WriteLine("Attack missed.");
            }
        }

        #endregion Methods

    }
}
