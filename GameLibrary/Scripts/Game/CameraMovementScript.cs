using GameEngineLibrary.Scripts;
using SharpDX;
using SharpDX.DirectInput;
using System.Collections.Generic;

namespace GameLibrary.Scripts.Game
{
    /// <summary>
    /// Класс поведения передвижения камеры
    /// </summary>
    public class CameraMovementScript : KeyboardListenerScript
    {
        private Vector3 _moveDirection;

        private float _borderLeft;
        private float _borderRight;
        private float _borderTop;
        private float _borderDown;

        private float _moveSpeed;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="moveSpeed">Скорость передвижения</param>
        /// <param name="borderLeft">Левая граница</param>
        /// <param name="borderRight">Правая граница</param>
        /// <param name="borderTop">Перхняя граница</param>
        /// <param name="borderDown">Нижняя граница</param>
        public CameraMovementScript(float moveSpeed, float borderLeft, float borderRight, float borderTop, float borderDown)
        {
            _moveSpeed = moveSpeed;
            _borderLeft = borderLeft;
            _borderRight = borderRight;
            _borderTop = borderTop;
            _borderDown = borderDown;

            Actions.Add(Key.W, delta => _moveDirection += Vector3.UnitZ);
            Actions.Add(Key.S, delta => _moveDirection -= Vector3.UnitZ);
            Actions.Add(Key.A, delta => _moveDirection -= Vector3.UnitX);
            Actions.Add(Key.D, delta => _moveDirection += Vector3.UnitX);
        }

        /// <summary>
        /// Обновление перед нажатием клавиш
        /// </summary>
        /// <param name="delta">Время между кадрами</param>
        protected override void BeforeKeyProcess(float delta)
        {
            _moveDirection = Vector3.Zero;
        }

        /// <summary>
        /// Обновление после нажатий клавиш
        /// </summary>
        /// <param name="delta">Время между кадрами</param>
        protected override void AfterKeyProcess(float delta)
        {
            var lastPosition = GameObject.Position;
            _moveDirection.Normalize();
            GameObject.MoveBy(_moveDirection * _moveSpeed * delta);

            if (GameObject.Position.X < _borderLeft || GameObject.Position.X > _borderRight ||
                GameObject.Position.Z < _borderDown || GameObject.Position.Z > _borderTop)
            {
                GameObject.MoveTo(lastPosition);
            }
        }
    }
}
