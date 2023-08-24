using GameEngineLibrary.Scripts;
using GameLibrary.Components;

namespace GameLibrary.Scripts.Utils
{
    /// <summary>
    /// Класс обновления компонента коллайдера
    /// </summary>
    public class ColliderScript : Script
    {
        private ColliderComponent _colliderComponent;

        /// <summary>
        /// Инициализация класса
        /// </summary>
        public override void Init()
        {
            _colliderComponent = GameObject.GetComponent<ColliderComponent>();
            _colliderComponent.AddCollisionOnScene();
        }

        /// <summary>
        /// Обновление поведения
        /// </summary>
        /// <param name="delta">Время между кадрами</param>
        public override void Update(float delta) { }
    }
}
