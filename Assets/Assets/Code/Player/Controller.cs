using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference jump;
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float jumpForce;
        private bool _isMoving;
        private Rigidbody2D _rb;
        private float _movement;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();

            if (move)
            {
                move.action.started += Move;
                move.action.canceled += Stop;
            }

            if (jump)
            {
                jump.action.started += Jump;
            }
        }

        private void Jump(InputAction.CallbackContext _) => _rb?.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

        private void Stop(InputAction.CallbackContext _) => _isMoving = false;

        private void Move(InputAction.CallbackContext obj)
        {
            _isMoving = true;
            _movement = obj.ReadValue<float>();
        }

        private void FixedUpdate()
        {
            if (!_rb || !_isMoving || MathF.Abs(_rb.linearVelocity.magnitude) > maxSpeed) return;
            _rb.AddForce(new Vector2(_movement * acceleration * Time.fixedDeltaTime, 0));
        }

        private void OnDestroy()
        {
            if (move)
            {
                move.action.started -= Move;
                move.action.canceled -= Stop;
            }

            if (jump)
            {
                jump.action.started -= Jump;
            }
        }
    }
}