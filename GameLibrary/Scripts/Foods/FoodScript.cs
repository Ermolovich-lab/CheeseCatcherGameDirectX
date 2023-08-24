using GameEngineLibrary.Collisions;
using GameEngineLibrary.Game;
using GameEngineLibrary.Scripts;
using SharpDX.XAudio2;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLibrary.Components;
using GameEngineLibrary.Graphics;
using SoundLibrary;

namespace GameLibrary.Scripts.Foods
{
    public class FoodScript : Script
    {
        private ColliderComponent _colliderComponent;
        private FoodComponent _foodComponent;

        private Vector3 _resistance;

        SharpAudioVoice _voice;

        public FoodScript(SharpAudioVoice voice)
        {
            _voice = voice;
        }

        public override void Init()
        {
            _foodComponent = GameObject.GetComponent<FoodComponent>();
            _colliderComponent = GameObject.GetComponent<ColliderComponent>();
            _resistance = new Vector3(2, 9.81f, 0);
        }

        public override void Update(float delta)
        {
            if (_colliderComponent.CheckIntersects(out Game3DObject gameObject, "player"))
            {
                gameObject.GetComponent<PlayerComponent>().NumberСaughtСheeses += _foodComponent.Points;
                _colliderComponent.RemoveCollisionFromScene();
                GameObject.Scene.RemoveGameObject(GameObject);

                _voice.Stop();
                _voice.Play();
            }

            _foodComponent.SpeedMove -= _resistance * delta;

            var newPosition = _foodComponent.Direction * _foodComponent.SpeedMove * delta;
            GameObject.MoveBy(newPosition);

            GameObject.RotateX(newPosition.X);
        }
    }
}
