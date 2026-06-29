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

    [Header("Broadcast Events")]
    [SerializeField] private EventChannel PauseGameEvent;
    [SerializeField] private EventChannel UnpauseGameEvent;

    private bool _isPaused = false;

    private void Awake()
    {
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());
        UnpauseGame();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                PauseGame();
            else
                UnpauseGame();
        }
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

    public void PauseGame()
    {
        Debug.Log("A");
        Time.timeScale = 0;
        _isPaused = true;
        PauseGameEvent.RaiseEvent();
    }

    public void UnpauseGame()
    {
        Debug.Log("B");
        Time.timeScale = 1;
        _isPaused = false;
        UnpauseGameEvent.RaiseEvent();
    }

    public void BackToMenu()
    {
        ServiceProvider.Instance.GetService<CustomSceneManager>().LoadMainMenu();
    }
}
