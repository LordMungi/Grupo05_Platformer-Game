using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Code.Player
{
    public delegate void LongJumpStarted();

    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        public event LongJumpStarted OnLongJumpStarted;

        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference jump;
        [SerializeField] private float acceleration;
        [SerializeField] private float maxSpeed;
        [SerializeField] private float jumpForce;

        [SerializeField, Tooltip("Force  to counter gravity for long jump"), Range(0, 9)]
        private float counterGravityForce;

        private bool _isMoving;
        private bool _isLongJumping;
        private bool _isGrounded;
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

            if (!jump) return;

            jump.action.started += Jump;
            jump.action.performed += LongJump;
            jump.action.canceled += CancelLongJump;
        }

        #region Custom Callbacks
        private void CancelLongJump(InputAction.CallbackContext _) => _isLongJumping = false;

        private void LongJump(InputAction.CallbackContext _)
        {
            _isLongJumping = true;
            OnLongJumpStarted?.Invoke();
        }

        private void Jump(InputAction.CallbackContext _)
        {
            if (!_isGrounded) return;
            
            _rb?.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        private void Stop(InputAction.CallbackContext _) => _isMoving = false;

        private void Move(InputAction.CallbackContext obj)
        {
            _isMoving = true;
            _movement = obj.ReadValue<float>();
        }

        #endregion
        
        private void FixedUpdate()
        {
            if (!_rb) return;

            if (_isMoving && MathF.Abs(_rb.linearVelocity.magnitude) < maxSpeed)
            {
                _rb.AddForce(new Vector2(_movement * acceleration * Time.fixedDeltaTime, 0), ForceMode2D.Force);
            }

            if (_isLongJumping)
            {
                _rb.AddForce(new Vector2(0, counterGravityForce * Time.fixedDeltaTime), ForceMode2D.Force);
            }
        }

        private void OnDestroy()
        {
            if (move)
            {
                move.action.started -= Move;
                move.action.canceled -= Stop;
            }

            if (!jump) return;

            jump.action.started -= Jump;
            jump.action.performed -= LongJump;
            jump.action.canceled -= CancelLongJump;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Walkable")) _isGrounded = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Walkable")) _isGrounded = false;
        }
    }
}