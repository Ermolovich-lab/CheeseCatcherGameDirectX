using GameEngineLibrary.Components;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Components
{
    public class FoodComponent : ObjectComponent
    {
        public int Points { get; private set; }

        public Vector3 Direction { get; set; }

        private Vector3 _speedMove;
        public Vector3 SpeedMove { 
            get 
            { 
                return _speedMove; 
            } 
            set 
            {
                _speedMove = value;

                if (_speedMove.X < 0)
                {
                    _speedMove.X = 0;
                }
            } 
        }

        private Vector3 _speedRotation;

        public Vector3 SpeedRotation
        {
            get
            {
                return _speedRotation;
            }
            set
            {
                _speedRotation = value;
            }
        }

        public FoodComponent(int points)
        {
            Points = points;
            Direction = Vector3.Zero;
        }
    }
}
