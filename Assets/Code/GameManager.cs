using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private PlayerController Player;
    [SerializeField] private Transform[] Checkpoints;

    [Header("Values")]
    [SerializeField] private float PlayerReviveDelay = 1f;

    [Header("Listener Events")]
    [SerializeField] private EventChannel PlayerDeathEvent;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());
    }

    private void OnEnable()
    {
        PlayerDeathEvent.OnEventTriggered += OnPlayerDeath;
    }
    private void OnDisable()
    {
        PlayerDeathEvent.OnEventTriggered -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(RevivePlayer, PlayerReviveDelay);
    }

    private void RevivePlayer()
    {
        Player.Spawn(Checkpoints[0].position);
    }
}
