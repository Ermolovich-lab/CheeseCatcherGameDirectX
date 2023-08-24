using GameEngineLibrary.Collisions;
using GameEngineLibrary.Game;
using GameEngineLibrary.Graphics;
using GameLibrary.Factories;
using GameLibrary.Managers;
using GameLibrary.Scripts.Game;
using SharpDX;
using SharpDX.Mathematics.Interop;
using SoundLibrary;
using GameUILibrary;
using GameUILibrary.Backgrounds;
using GameUILibrary.Containers;
using GameUILibrary.Drawing;
using GameUILibrary.Elements;
using System;
using GameLibrary.Components;
using System.Collections.Generic;
using GameEngineLibrary.Utils;
using GameLibrary.Scripts.Utils;

namespace GameLibrary.Scenes
{
    /// <summary>
    /// Класс сцены с игровым уровнем
    /// </summary>
    public class LevelScene : Scene
    {
        private UIManager _uiManager;

        private PlayerFactory _playerFactory;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public LevelScene()
        {
            _uiManager = new UIManager();
        }

        /// <summary>
        /// Инициализация игровых объектов
        /// </summary>
        /// <param name="loader">Класс для загруки файло</param>
        /// <param name="audioDevice">Аудио устройство SharpDX</param>
        protected override void InitializeObjects(Loader loader, SharpAudioDevice audioDevice)
        {
            _playerFactory = new PlayerFactory(loader, audioDevice, _uiManager);

            var player = _playerFactory.CreatePlayer();
            var playerComponent = player.GetComponent<PlayerComponent>();
            _uiManager.SetPlayerComponent(playerComponent);
            AddGameObject(player);

            List<CopyableGameObject> foods = new List<CopyableGameObject>();

            var foodFactory = new FoodFactory(loader, audioDevice, _uiManager);
            foods.Add(foodFactory.CreateCheese());
            foods.Add(foodFactory.CreateCheese());
            foods.Add(foodFactory.CreateTomato());
            foods.Add(foodFactory.CreateCucumber());

            var voice = new SharpAudioVoice(audioDevice, @"Resources/Sounds/Spawn.wav");

            var go = new Game3DObject(new Vector3(-11f, 20f, 0f), Vector3.Zero);
            var foodLauncher = new FoodLauncherScript(foods, new Vector3(1, 1, 0), 2, 0, voice);
            go.AddScript(foodLauncher);
            AddGameObject(go);

            go = new Game3DObject(new Vector3(11f, 20f, 0f), Vector3.Zero);
            foodLauncher = new FoodLauncherScript(foods, new Vector3(-1, 1, 0), 2, 3f, voice);
            go.AddScript(foodLauncher);
            AddGameObject(go);

            go = new Game3DObject(new Vector3(11f, 2f, 0f), Vector3.Zero);
            var gameManager = new GameManager(playerComponent, 30f);
            go.AddScript(gameManager);
            AddGameObject(go);

            _uiManager.SetGameManager(gameManager);

            go = new Game3DObject(new Vector3(0f, -5f, 0f), Vector3.Zero);
            go.Collision = new BoxCollision(40f, 5);
            go.AddComponent(new ColliderComponent(), typeof(ColliderComponent));
            go.AddScript(new ColliderScript());
            go.AddScript(new TrashScript());
            go.Tag = "trash";
            AddGameObject(go);

            go = foodFactory.CreateRoom(new Vector3(20, -15, 60), new Vector3(0, 0, 90 * (float)Math.PI / 180));
            AddGameObject(go);
        }

        /// <summary>
        /// Создать игрового объекта камеры
        /// </summary>
        /// <returns></returns>
        protected override Camera CreateCamera()
        {
            var camera = new Camera(new Vector3(0f, 20, -35f), rotY: (float)(10 * Math.PI / 180));
            camera.AddScript(new CameraMovementScript(15.0f, -36.0f, 36.0f, 36.0f, -36.0f));
            AddGameObject(camera);
            return camera;
        }

        #region UI

        /// <summary>
        /// Инициализация пользовательского интерфейса
        /// </summary>
        /// <param name="loader">Класс для загруки файлов</param>
        /// <param name="context">Контекст отрисовки</param>
        /// <param name="screenWidth">Ширина экрана</param>
        /// <param name="screenHeight">Высота экрана</param>
        /// <returns>Пользовательский интерфейс</returns>
        protected override UIElement InitializeUI(Loader loader, DrawingContext context, int screenWidth, int screenHeight)
        {
            context.NewNinePartsBitmap("Panel", loader.LoadBitmapFromFile(@"Resources\UI\panel.png"), 25, 103, 25, 103);
            context.NewNinePartsBitmap("ButtonPressed", loader.LoadBitmapFromFile(@"Resources\UI\buttonPressed.png"), 0, 0, 0, 0);
            context.NewNinePartsBitmap("ButtonRealesed", loader.LoadBitmapFromFile(@"Resources\UI\buttonRealesed.png"), 0, 0, 0, 0);
            context.NewBitmap("Points", loader.LoadBitmapFromFile(@"Resources\UI\pointsIcon.png"));

            context.NewSolidBrush("TextRed", new RawColor4(183.0f / 255.0f, 24.0f / 255.0f, 51.0f / 255.0f, 1.0f));
            context.NewSolidBrush("TextLight", new RawColor4(193.0f / 255.0f, 236.0f / 255.0f, 245.0f / 255.0f, 1.0f));
            context.NewSolidBrush("TextDark", new RawColor4(49.0f / 255.0f, 51.0f / 255.0f, 87.0f / 255.0f, 1.0f));
            context.NewTextFormat("Text50", fontSize: 50.0f, textAlignment: SharpDX.DirectWrite.TextAlignment.Center,
                paragraphAlignment: SharpDX.DirectWrite.ParagraphAlignment.Center);
            context.NewTextFormat("Text15", fontSize: 15.0f, textAlignment: SharpDX.DirectWrite.TextAlignment.Center,
                paragraphAlignment: SharpDX.DirectWrite.ParagraphAlignment.Center);

            var ui = new UIMultiElementsContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight));

            var headerPanel = CreateHeaderPanel();
            var timerText = CreateTimerPanel();

            ui.Add(headerPanel);
            ui.Add(timerText);
            ui.OnResized += () => headerPanel.Size = ui.Size;
            ui.OnResized += () => timerText.Size = ui.Size;

            return ui;
        }

        /// <summary>
        /// Создать элемент интерфейса, который отображает информацию об очках
        /// </summary>
        /// <returns>Элемент интерфейса</returns>
        public UISequentialContainer CreateHeaderPanel()
        {
            var header = new UISequentialContainer(Vector2.Zero, Vector2.Zero)
            {
                MainAxis = UISequentialContainer.Alignment.Start,
                CrossAxis = UISequentialContainer.Alignment.Center
            };

            var panel = new UISequentialContainer(Vector2.Zero, new Vector2(200.0f, 80.0f), false)
            {
                MainAxis = UISequentialContainer.Alignment.Center,
                CrossAxis = UISequentialContainer.Alignment.Center,
                Background = new NinePartsTextureBackground("Panel")
            };

            var text = new UIText("0", new Vector2(100.0f, 70.0f), "Text35", "TextLight");
            var icon = new UIPanel(Vector2.Zero, new Vector2(50.0f, 50.0f))
            {
                Background = new TextureBackground("Points")
            };
            panel.Add(text);
            panel.Add(icon);

            _uiManager.InitializeHeader(text);
            header.Add(new UIMarginContainer(panel, 10.0f));

            return header;
        }

        public UISequentialContainer CreateTimerPanel()
        {
            var header = new UISequentialContainer(Vector2.Zero, Vector2.Zero)
            {
                MainAxis = UISequentialContainer.Alignment.Start,
                CrossAxis = UISequentialContainer.Alignment.Start
            };

            var panel = new UISequentialContainer(Vector2.Zero, new Vector2(200.0f, 80.0f), false)
            {
                MainAxis = UISequentialContainer.Alignment.Center,
                CrossAxis = UISequentialContainer.Alignment.Center,
                Background = new NinePartsTextureBackground("Panel")
            };

            var text = new UIText("0", new Vector2(100.0f, 70.0f), "Text35", "TextLight");

            _uiManager.InitializeTimer(text);
            header.Add(new UIMarginContainer(text, 10.0f));

            return header;
        }
        #endregion
    }
}
