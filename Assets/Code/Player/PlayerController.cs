using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D Body;

    [Header("Values")]
    [SerializeField] private float Acceleration;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float GroundedCheckLength = 0.1f;

    private float _inputDelta;
    private bool _isJump;
    public bool _isGrounded;
    private LayerMask _groundLayer;

    void Start()
    {
        _groundLayer = LayerMask.GetMask("Ground");
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundedCheckLength,  _groundLayer);

        Body.AddForce(new Vector2(_inputDelta * Acceleration * Time.fixedDeltaTime, 0));
        Body.linearVelocity = new Vector2(Mathf.Clamp(Body.linearVelocity.x, -MaxSpeed, MaxSpeed), Body.linearVelocity.y);

        if (_inputDelta == 0)
            Body.linearVelocity = new Vector2(0, Body.linearVelocity.y);

        if (_isJump && _isGrounded)
        {
            Debug.Log("A");
            Body.linearVelocity = new Vector2(Body.linearVelocity.x, 0);
            Body.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            _isJump = false;
        }
    }

    private void Update()
    {
        _inputDelta = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            _isJump = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundedCheckLength));
    }
}
