using UnityEngine;

public class FollowDampened : MonoBehaviour
{
    [SerializeField] Transform ObjectToFollow;
    [SerializeField] float Speed;

    void Update()
    {
        Vector2 lerp = Vector2.Lerp(transform.position, ObjectToFollow.position, Speed * Time.deltaTime);
        transform.position = new Vector3(lerp.x, lerp.y, transform.position.z);
    }
}
