using Assimp;
using GameEngineLibrary.Scripts;
using GameEngineLibrary.Utils;
using GameLibrary.Components;
using GameLibrary.Scenes;
using SharpDX;
using SoundLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLibrary.Scripts.Game
{
    public class FoodLauncherScript : Script
    {
        private List<CopyableGameObject> _foods;
        private Vector3 _direction;

        private float _reloadSpawn;
        private float _timer = 0;

        private Random rnd = new Random();

        SharpAudioVoice _voice;

        public FoodLauncherScript(List<CopyableGameObject> foods, Vector3 direction, float reloadSpawn, float delay, SharpAudioVoice voice)
        {
            _voice = voice;
            _foods = foods;
            _direction = direction;
            _reloadSpawn = reloadSpawn;
            _timer -= delay;
        }

        public override void Update(float delta)
        {
            _timer += delta;

            var index = rnd.Next(0, _foods.Count);

            if (_timer >= _reloadSpawn)
            {
                _timer = 0;

                var go = _foods[index].Copy();
                go.MoveTo(GameObject.Position);
                var component = go.GetComponent<FoodComponent>();
                component.Direction = _direction;
                component.SpeedMove = new Vector3(rnd.Next(3, 9), rnd.Next(6, 16), 0);

                GameObject.Scene.AddGameObject(go);

                _voice.Stop();
                _voice.Play();
            }
        }
    }
}
