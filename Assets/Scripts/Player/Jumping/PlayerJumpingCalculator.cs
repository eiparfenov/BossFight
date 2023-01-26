using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Player.Jumping
{
    public class PlayerJumpingCalculator: IVelocityEffector
    {
        private const float DelayAfterJump = .1f;
        
        private readonly PlayerJumpingSettings _settings;
        private readonly GroundChecker _groundChecker;
        
        private bool _jumpButtonPressed;
        private float? _jumpRequestTime;
        private bool _possibleToJumpAfterDelay = true;

        public PlayerJumpingCalculator(PlayerJumpingSettings settings, GroundChecker groundChecker)
        {
            _settings = settings;
            _groundChecker = groundChecker;
        }

        public bool JumpButtonPressed
        {
            get => _jumpButtonPressed;
            set
            {
                _jumpButtonPressed = value;
                if (value)
                {
                    _jumpRequestTime = Time.time;
                    Debug.Log(_jumpRequestTime);
                }
            }
        }

        public Vector2 NewVelocity(Vector2 currentVelocity, float deltaTime)
        {
            var acceleration = 2 * _settings.Height / _settings.Duration / _settings.Duration * deltaTime;
            if (IsJumpPossible)
            {
                MakeDelayAfterJump();
                if (currentVelocity.y < 0)
                {
                    currentVelocity.y = 0;
                }
                currentVelocity.y += 2 * _settings.Height / _settings.Duration;
                Debug.Log("Jump");
                _jumpRequestTime = null;
            }

            if (currentVelocity.y < 0)
            {
                acceleration *= _settings.DownGravity;
            }

            if (currentVelocity.y > 0 && !JumpButtonPressed)
            {
                acceleration *= _settings.JumpCutoff;
            }

            currentVelocity.y -= acceleration;
            return currentVelocity;
        }

        private async void MakeDelayAfterJump()
        {
            if(!_possibleToJumpAfterDelay)
                return;
            Debug.Log("DelayStarted");
            _possibleToJumpAfterDelay = false;
            await UniTask.Delay((int) (DelayAfterJump * 1000f));
            await UniTask.WaitUntil(() => _groundChecker.Grounded == 0f);
            _possibleToJumpAfterDelay = true; 
        }

        private bool IsJumpPossible => _jumpRequestTime is not null &&
                                       Time.time - _jumpRequestTime.Value < _settings.JumpBuffer &&
                                       _possibleToJumpAfterDelay &&
                                       _groundChecker.Grounded < _settings.CoyoteTime;
    }
}