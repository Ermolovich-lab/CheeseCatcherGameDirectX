using GameEngineLibrary.Game;
using SharpDX;
using SharpDX.Mathematics.Interop;
using GameUILibrary;
using GameUILibrary.Backgrounds;
using GameUILibrary.Containers;
using GameUILibrary.Drawing;
using GameUILibrary.Elements;

namespace GameLibrary.Scenes
{
    /// <summary>
    /// Класс сцены с окончанием игры
    /// </summary>
    public class EndGameScene : Scene
    {
        private string _ednGametext;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="text">Текст с сообщением о результах игры</param>
        public EndGameScene(string text)
        {
            _ednGametext = text;
        }

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
            context.NewBitmap("Background", loader.LoadBitmapFromFile(@"Resources\UI\menu.png"));
            context.NewNinePartsBitmap("Panel", loader.LoadBitmapFromFile(@"Resources\UI\panel.png"), 25, 103, 25, 103);
            context.NewNinePartsBitmap("ButtonPressed", loader.LoadBitmapFromFile(@"Resources\UI\buttonPressed.png"), 30, 98, 30, 98);
            context.NewNinePartsBitmap("ButtonRealesed", loader.LoadBitmapFromFile(@"Resources\UI\buttonRealesed.png"), 30, 98, 30, 98);
            context.NewSolidBrush("TextDark", new RawColor4(49.0f / 255.0f, 51.0f / 255.0f, 87.0f / 255.0f, 1.0f));
            context.NewSolidBrush("TextLight", new RawColor4(193.0f / 255.0f, 236.0f / 255.0f, 245.0f / 255.0f, 1.0f));
            context.NewTextFormat("Text60", fontFamilyName: "Arial Black", fontSize: 30.0f, textAlignment: SharpDX.DirectWrite.TextAlignment.Center,
                paragraphAlignment: SharpDX.DirectWrite.ParagraphAlignment.Center);
            context.NewTextFormat("Text35", fontSize: 35.0f, textAlignment: SharpDX.DirectWrite.TextAlignment.Center,
                paragraphAlignment: SharpDX.DirectWrite.ParagraphAlignment.Center);

            var ui = new UIMultiElementsContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight))
            {
                Background = new TextureBackground("Background")
            };

            var mainContainer = new UISequentialContainer(Vector2.Zero, new Vector2(screenWidth, screenHeight))
            {
                MainAxis = UISequentialContainer.Alignment.Center,
                CrossAxis = UISequentialContainer.Alignment.Center,
            };

            var container = new UISequentialContainer(Vector2.Zero, new Vector2(500.0f, 250.0f))
            {
                MainAxis = UISequentialContainer.Alignment.Center,
                CrossAxis = UISequentialContainer.Alignment.Center,
                Background = new NinePartsTextureBackground("Panel")
            };

            var text = new UIText(_ednGametext, new Vector2(400.0f, 100.0f), "Text60", "TextDark");
            container.Add(text);

            text = new UIText("Выход", new Vector2(250.0f, 70.0f), "Text35", "TextDark");
            var button = new UIButton(text)
            {
                ReleasedBackground = new NinePartsTextureBackground("ButtonRealesed"),
                PressedBackground = new NinePartsTextureBackground("ButtonPressed")
            };

            button.OnClicked += () => Game.CloseProgramm();
            container.Add(new UIMarginContainer(button, 0.0f, 20.0f));

            mainContainer.Add(container);

            ui.Add(mainContainer);
            ui.OnResized += () => mainContainer.Size = ui.Size;

            return ui;
        }
    }
}
