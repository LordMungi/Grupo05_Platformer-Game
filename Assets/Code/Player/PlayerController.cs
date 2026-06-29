using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D Body;

    [Header("Values")]
    [SerializeField] private float Acceleration;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float JumpForce;

    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float inputDelta = Input.GetAxisRaw("Horizontal");

        Body.AddForce(new Vector2(inputDelta * Acceleration * Time.deltaTime, 0));
        Body.linearVelocity = Vector2.ClampMagnitude(Body.linearVelocity, MaxSpeed);

        if (inputDelta == 0)
            Body.linearVelocity = new Vector2(0, Body.linearVelocity.y);
    }
}
