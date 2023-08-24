using GameEngineLibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Components
{
    public class PlayerComponent : ObjectComponent
    {
        private int _numberСaughtСheeses;

        public event Action<int> OnComponentUpdate;

        public int NumberСaughtСheeses
        {
            get { return _numberСaughtСheeses; }
            set
            {
                _numberСaughtСheeses = ValidationProperty(value);
                OnComponentUpdate?.Invoke(_numberСaughtСheeses);
            }
        }

        public PlayerComponent(int numberСaughtСheeses) 
        {
            _numberСaughtСheeses = numberСaughtСheeses;
        }

        private int ValidationProperty(int property)
        {
            if (property < 0)
            {
                property = 0;
            }

            return property;
        }
    }
}
