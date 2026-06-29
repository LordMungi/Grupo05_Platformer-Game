using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D Body;

    [Header("Values")]
    [SerializeField] private float Acceleration;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce;

    float _inputDelta;
    bool _isJump;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {

        Body.AddForce(new Vector2(_inputDelta * Acceleration * Time.fixedDeltaTime, 0));
        Body.linearVelocity = new Vector2(Mathf.Clamp(Body.linearVelocity.x, -MaxSpeed, MaxSpeed), Body.linearVelocity.y);

        if (_inputDelta == 0)
            Body.linearVelocity = new Vector2(0, Body.linearVelocity.y);

        if (_isJump)
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
}
