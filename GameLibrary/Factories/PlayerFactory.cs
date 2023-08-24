using GameEngineLibrary.Collisions;
using GameEngineLibrary.Components;
using GameEngineLibrary.Game;
using GameEngineLibrary.Graphics;
using GameEngineLibrary.Scripts;
using GameEngineLibrary.Utils;
using GameLibrary.Components;
using GameLibrary.Managers;
using GameLibrary.Scripts.Cat;
using GameLibrary.Scripts.Game;
using GameLibrary.Scripts.Utils;
using SharpDX;
using SoundLibrary;
using System;
using System.Collections.Generic;

namespace GameLibrary.Factories
{
    /// <summary>
    /// Фабрика для создания персонажей
    /// </summary>
    public class PlayerFactory
    {
        private Loader _loader;
        private SharpAudioDevice _audioDevice;
        private UIManager _uiManager;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="loader">Класс для загрузки объектов</param>
        /// <param name="audioDevice">Аудио устройство SharpDX</param>
        /// <param name="unitsController">Контроллер персонажей игрока</param>
        /// <param name="aIUnitsController">Контроллер персонажей под управлением ИИ</param>
        public PlayerFactory(Loader loader, SharpAudioDevice audioDevice, UIManager uIManager)
        {
            _audioDevice = audioDevice;
            _loader = loader;
            _uiManager = uIManager;
        }

        public Game3DObject CreatePlayer()
        {
            var file = @"Resources/Models/bucket.obj";

            var go = _loader.LoadGameObjectFromFile(file, Vector3.Zero, Vector3.Zero);
            go.Collision = new SphereCollision(1f);
            go.Tag = "player";

            go.AddComponent(new ColliderComponent(), typeof(ColliderComponent));
            go.AddComponent(new PlayerComponent(0), typeof(PlayerComponent));

            go.AddScript(new ColliderScript());
            var script = new PlayerScript(0.25f, -10, 10);
            go.AddScript(script);

            return go;
        }
    }
}
