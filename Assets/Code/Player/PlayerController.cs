using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D Body;

    [Header("Values")]
    [Header("Horizontal Movement")]
    [SerializeField] private float Acceleration;
    [SerializeField] private float MaxSpeed;

    [Header("Jump")]
    [SerializeField] private float GroundedCheckLength = 0.1f;
    [SerializeField] private float JumpForce;
    [SerializeField] private float JumpBufferLength = 0.1f;

    [Header("Fly")]
    [SerializeField] private float JetpackForce = 1f;
    [SerializeField] private float FlyAfterJumpDelay = 0.1f;
    [SerializeField] private float MaxFuel = 10f;
    [SerializeField] private float FuelConsumption = 0.5f;
    [SerializeField] private float RefillRate = 0.5f;
    [SerializeField] private float RefillDelay = 0.2f;

    [Header("Broadcast Events")]


    private float _inputDelta;

    private bool _isJump;
    private bool _isGrounded;
    
    private bool _canFly;
    private bool _isFly;
    public float _currentFuel;

    private float _refillTimer;

    private int _state;

    private LayerMask _groundLayer;

    void Start()
    {
        _groundLayer = LayerMask.GetMask("Ground");
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());

        _currentFuel = MaxFuel;
    }

    private void FixedUpdate()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, GroundedCheckLength,  _groundLayer);

        Body.AddForce(new Vector2(_inputDelta * Acceleration * Time.fixedDeltaTime, 0));
        Body.linearVelocity = new Vector2(Mathf.Clamp(Body.linearVelocity.x, -MaxSpeed, MaxSpeed), Body.linearVelocity.y);

        if (_inputDelta == 0)
            Body.linearVelocity = new Vector2(0, Body.linearVelocity.y);

        if (_isGrounded)
        {
            if (_isJump)
            {
                Body.linearVelocity = new Vector2(Body.linearVelocity.x, 0);
                Body.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                _isJump = false;
            }
        }
        else if (_isFly)
        {
            if (_currentFuel > 0)
            {
                Body.AddForce(new Vector2(0, JetpackForce * Time.fixedDeltaTime));
            }
        }
        
    }

    private void Update()
    {
        _refillTimer += Time.deltaTime;

        _inputDelta = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGrounded)
            {
                _isJump = true; 
                _canFly = false;
                ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(DequeueJump, JumpBufferLength);
                ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(AllowFly, FlyAfterJumpDelay);
            }
            else if (_canFly)
            {
                _isFly = true;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            _isFly = false;
        }


        if (_isFly)
        {
            _currentFuel = Mathf.Max(0, _currentFuel - FuelConsumption * Time.deltaTime);
            _refillTimer = 0;
        }
        else if (_refillTimer >= RefillDelay)
        {
            _currentFuel = Mathf.Min(MaxFuel, _currentFuel + RefillRate * Time.fixedDeltaTime);
        }
    }

    private void DequeueJump()
    {
        _isJump = false;
    }

    private void AllowFly()
    {
        _canFly = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - GroundedCheckLength));
    }
}
