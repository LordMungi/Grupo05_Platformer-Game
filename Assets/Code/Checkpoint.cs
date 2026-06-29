using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [Header("Broadcast Events")]
    [SerializeField] private TransformEventChannel CheckpointReachedEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckpointReachedEvent.RaiseEvent(transform);
    }
}
