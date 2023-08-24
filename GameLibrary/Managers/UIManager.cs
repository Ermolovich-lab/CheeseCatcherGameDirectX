using GameEngineLibrary.Animation;
using GameLibrary.Components;
using GameLibrary.Scripts.Game;
using GameUILibrary.Containers;
using GameUILibrary.Elements;

namespace GameLibrary.Managers
{
    /// <summary>
    /// Менеджер управления пользовательским интерфейсом
    /// </summary>
    public class UIManager
    {
        private UIText _header;
        private UIText _timer;

        private PlayerComponent _playerComponent;
        private GameManager _gameManager;

        /// <summary>
        /// Коструктор класса
        /// </summary>
        public UIManager()
        {
            
        }

        public void SetPlayerComponent(PlayerComponent playerComponent)
        {
            _playerComponent = playerComponent;

            _playerComponent.OnComponentUpdate += (value) =>
            {
                _header.Text = value.ToString();
            };
        }

        public void SetGameManager(GameManager gameManager)
        {
            _gameManager = gameManager;

            _gameManager.OnTimerUpdate += (value) =>
            {
                _timer.Text = string.Format("{0:f1}s", value);
            };
        }

        public void InitializeHeader(UIText header)
        {
            _header = header;
        }

        public void InitializeTimer(UIText timer)
        {
            _timer = timer;
        }
    }
}
