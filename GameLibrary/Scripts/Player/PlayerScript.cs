using GameEngineLibrary.Collisions;
using GameEngineLibrary.Game;
using GameEngineLibrary.Scripts;
using GameLibrary.Components;
using SharpDX.XAudio2;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Scripts.Cat
{
    public class PlayerScript : Script
    {
        private PlayerComponent _playerComponent;

        private Vector3 _moveDirection;

        private float _borderLeft;
        private float _borderRight;

        private float _moveSpeed;

        private InputController _inputController;

        public PlayerScript(float moveSpeed, float borderLeft, float borderRight)
        {
            _moveSpeed = moveSpeed;
            _borderLeft = borderLeft;
            _borderRight = borderRight;

            _inputController = InputController.GetInstance();
        }

        public override void Init()
        {
            _playerComponent = GameObject.GetComponent<PlayerComponent>();
        }

        public override void Update(float delta)
        {
            var lastPosition = GameObject.Position;
            var newPositionX = _inputController.MouseRelativePositionX * _moveSpeed * delta;
            GameObject.MoveBy(new Vector3(newPositionX, lastPosition.Y, lastPosition.Z));

            if (GameObject.Position.X < _borderLeft || GameObject.Position.X > _borderRight)
            {
                GameObject.MoveTo(lastPosition);
            }
        }
    }
}
