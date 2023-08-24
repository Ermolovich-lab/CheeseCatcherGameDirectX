using GameEngineLibrary.Collisions;
using GameEngineLibrary.Components;
using GameEngineLibrary.Game;
using GameEngineLibrary.Graphics;
using GameEngineLibrary.Scripts;
using GameEngineLibrary.Utils;
using GameLibrary.Components;
using GameLibrary.Managers;
using GameLibrary.Scenes;
using GameLibrary.Scripts.Cat;
using GameLibrary.Scripts.Foods;
using GameLibrary.Scripts.Game;
using GameLibrary.Scripts.Utils;
using SharpDX;
using SoundLibrary;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace GameLibrary.Factories
{
    /// <summary>
    /// Фабрика для создания игровых объектов зданий
    /// </summary>
    public class FoodFactory
    {
        private Loader _loader;
        private SharpAudioDevice _audioDevice;

        private UIManager _uiManager;

        private PlayerComponent _catComponent;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="loader">Класс для загрузки объектов</param>
        /// <param name="audioDevice">Аудио устройство SharpDX</param>
        /// <param name="resourcesManager">Менеджер управления ресурсами</param>
        /// <param name="uiManager">Менеджер управление интерфейсом</param>
        /// <param name="unitsController">Контроллер персонажей игрока</param>
        /// <param name="aiUnitsController">Контроллер персонажей под управлением ИИ</param>
        public FoodFactory(Loader loader, SharpAudioDevice audioDevice,
            UIManager uiManager)
        {
            _audioDevice = audioDevice;
            _loader = loader;
            _uiManager = uiManager;
        }

        private CopyableGameObject CreateFood(Game3DObject go, int point)
        {
            go.Tag = "food";
            go.Collision = new SphereCollision(1f);
            var components = new List<Func<ObjectComponent>>
            {
                () => new ColliderComponent(),
                () => new ReloadComponent(5f),
                () => new FoodComponent(point)
            };
            var scripts = new List<Func<Script>>
            {
                () => new ColliderScript(),
                () => new ReloadScript(),
                () =>
                {
                    var voice = new SharpAudioVoice(_audioDevice, @"Resources/Sounds/Slovil.wav");
                    var script = new FoodScript(voice);

                    return script;
                }
            };
            var result = new CopyableGameObject(go, scripts, components);
            return result;
        }

        public CopyableGameObject CreateCheese()
        {
            var file = @"Resources/Models/cheese.fbx";
            var go = _loader.LoadGameObjectFromFile(file, Vector3.Zero, Vector3.Zero);

            return CreateFood(go, 10);
        }

        public CopyableGameObject CreateTomato()
        {
            var file = @"Resources/Models/tomato.obj";
            var go = _loader.LoadGameObjectFromFile(file, Vector3.Zero, Vector3.Zero);

            return CreateFood(go, -5);
        }

        public CopyableGameObject CreateCucumber()
        {
            var file = @"Resources/Models/cucumber.obj";
            var go = _loader.LoadGameObjectFromFile(file, Vector3.Zero, Vector3.Zero);

            return CreateFood(go, -10);
        }

        public Game3DObject CreateRoom(Vector3 position, Vector3 rotation)
        {
            string file = @"Resources/Models/kitchen.obj";
            var go = _loader.LoadGameObjectFromFile(file, position, rotation);

            return go;
        }
    }
}
