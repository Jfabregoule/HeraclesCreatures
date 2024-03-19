using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.GameObject
{
    internal class Heroes : GameObject
    {
        //Fields
        HeroeStats _stats;
        //Properties
        public HeroeStats Stats { get { return _stats; } }
        //Events

        //Methods
        Heroes() { }  
    }
}
