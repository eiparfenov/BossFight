using System;
using Player.Jumping;
using Player.Movement;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerMovementSettings movementSettings;
        [SerializeField] private PlayerJumpingSettings jumpingSettings;
        
        private Rigidbody2D _rigidbody2D;
        private PlayerMovementCalculator _movementCalculator;
        private PlayerJumpingCalculator _jumpingCalculator;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _movementCalculator = new PlayerMovementCalculator(movementSettings);
            _jumpingCalculator = new PlayerJumpingCalculator(jumpingSettings, GetComponentInChildren<GroundChecker>());
        }

        private void FixedUpdate()
        {
            var currentVelocity = _rigidbody2D.velocity;
            foreach (var velocityEffector in new IVelocityEffector[] {_movementCalculator, _jumpingCalculator})
            {
                currentVelocity = velocityEffector.NewVelocity(currentVelocity, Time.fixedDeltaTime);
            }

            _rigidbody2D.velocity = currentVelocity;
        }

        private void Update()
        {
            _movementCalculator.LeftButtonPressed = Input.GetKey(KeyCode.A);
            _movementCalculator.RightButtonPressed = Input.GetKey(KeyCode.D);
            _jumpingCalculator.JumpButtonPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space);
        }
    }
}