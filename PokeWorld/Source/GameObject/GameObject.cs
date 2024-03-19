using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeraclesCreatures.Source.GameObject
{
    internal class GameObject
    {
        //Field
        string _name;

        //Property

        public string Name { get => _name; private set => _name = value; }

        //Event

        //Methods
        public GameObject() { }

    }
}
