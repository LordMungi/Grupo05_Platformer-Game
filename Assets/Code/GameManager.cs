using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerController Player;
    [SerializeField] private Transform Checkpoint;

    [Header("Values")]
    [SerializeField] private float PlayerReviveDelay = 1f;

    [Header("Listener Events")]
    [SerializeField] private EventChannel PlayerDeathEvent;
    [SerializeField] private TransformEventChannel CheckpointReachedEvent;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());
    }

    private void OnEnable()
    {
        PlayerDeathEvent.OnEventTriggered += OnPlayerDeath;
        CheckpointReachedEvent.OnEventTriggered += SetCheckpoint;
    }
    private void OnDisable()
    {
        PlayerDeathEvent.OnEventTriggered -= OnPlayerDeath;
        CheckpointReachedEvent.OnEventTriggered -= SetCheckpoint;
    }

    private void OnPlayerDeath()
    {
        ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(RevivePlayer, PlayerReviveDelay);
    }

    private void RevivePlayer()
    {
        Player.Spawn(Checkpoint.position);
    }

    private void SetCheckpoint(Transform c)
    {
        Checkpoint = c;
    }
}
