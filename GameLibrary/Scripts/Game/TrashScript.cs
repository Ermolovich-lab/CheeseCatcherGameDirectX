using GameEngineLibrary.Graphics;
using GameEngineLibrary.Scripts;
using GameLibrary.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts.Game
{
    public class TrashScript : Script
    {
        private ColliderComponent _colliderComponent;

        public TrashScript() 
        {
            
        }

        public override void Init()
        {
            _colliderComponent = GameObject.GetComponent<ColliderComponent>();
        }

        public override void Update(float delta)
        {
            if (_colliderComponent.CheckIntersects(out Game3DObject gameObject, "food"))
            {
                gameObject.GetComponent<ColliderComponent>().RemoveCollisionFromScene();
                GameObject.Scene.RemoveGameObject(gameObject);
            }
        }
    }
}
