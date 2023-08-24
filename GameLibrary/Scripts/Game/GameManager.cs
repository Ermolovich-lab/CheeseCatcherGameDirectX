using GameEngineLibrary.Scripts;
using GameLibrary.Components;
using GameLibrary.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameLibrary.Scripts.Game
{
    public class GameManager : Script
    {
        private PlayerComponent _playerComponent;

        private float _timer = 0;
        private float _timeEndGame;

        public Action<float> OnTimerUpdate;

        public GameManager(PlayerComponent playerComponent, float timeEndGame) 
        { 
            _playerComponent = playerComponent;
            _timeEndGame = timeEndGame;
        }

        public override void Update(float delta)
        {
            _timer += delta;

            OnTimerUpdate?.Invoke(_timeEndGame - _timer);

            if (_timer >= _timeEndGame)
            {
                var pointsText = _playerComponent.NumberСaughtСheeses.ToString();

                GameObject.Scene.Game.ChangeScene(new EndGameScene($"Красава, ты заработал {pointsText} очков!")) ;
            }
        }
    }
}
