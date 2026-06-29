using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private EventChannel WinGameEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WinGameEvent.RaiseEvent();
    }
}
