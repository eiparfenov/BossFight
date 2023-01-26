using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementCalculator: IVelocityEffector
    {
        private enum MoveDir
        {
            Left, Right, Stay
        }
        private readonly PlayerMovementSettings _settings;
        public bool LeftButtonPressed { get; set; }
        public bool RightButtonPressed { get; set; }

        public PlayerMovementCalculator(PlayerMovementSettings settings)
        {
            _settings = settings;
        }

        public Vector2 NewVelocity(Vector2 currentVelocity, float deltaTime)
        {
            var acceleration = 0f;
            
            var currentMoveDir = CurrentMoveDir(currentVelocity);
            var inputMoveDir = InputMoveDir(LeftButtonPressed, RightButtonPressed);

            if (inputMoveDir != MoveDir.Stay)
            {
                if (currentMoveDir == MoveDir.Stay || currentMoveDir == inputMoveDir)
                {
                    acceleration = _settings.MaxSpeed / _settings.AccelerationTime;
                }
                else
                {
                    acceleration = _settings.MaxSpeed / _settings.DecelerationTimeOnTurn;
                }

                if (inputMoveDir == MoveDir.Left)
                {
                    acceleration = -acceleration;
                }
            }
            else
            {
                if (currentMoveDir != MoveDir.Stay)
                {
                    acceleration = _settings.MaxSpeed / _settings.DecelerationTimeOnStop;
                }
                
                if (currentMoveDir == MoveDir.Right)
                {
                    acceleration = -acceleration;
                }
            }

            var newVelocity = currentVelocity + Vector2.right * acceleration * deltaTime;
            newVelocity.x = Mathf.Clamp(newVelocity.x, -_settings.MaxSpeed, _settings.MaxSpeed);

            if (inputMoveDir == MoveDir.Stay && newVelocity.x * currentVelocity.x < 0f)
            {
                newVelocity.x = 0f;
            }
            return newVelocity;
        }

        private MoveDir CurrentMoveDir(Vector2 currentVelocity)
        {
            if (currentVelocity.x < 0)
                return MoveDir.Left;
            if (currentVelocity.x == 0f)
                return MoveDir.Stay;
            return MoveDir.Right;
        }

        private MoveDir InputMoveDir(bool leftButtonPressed, bool rightButtonPressed)
        {
            if (leftButtonPressed)
            {
                if (rightButtonPressed)
                { 
                    return MoveDir.Stay;
                }
                else
                {
                    return MoveDir.Left;
                }
            }
            else 
                if (rightButtonPressed)
                {
                    return MoveDir.Right;
                }
                else
                {
                    return MoveDir.Stay;
                }
        }
    }
}