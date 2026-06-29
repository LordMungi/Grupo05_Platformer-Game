using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Listener Events")]
    [SerializeField] private IntEventChannel PlayerStateChangeEvent;

    private Animator _animationController;

    private void Start()
    {
        _animationController = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerStateChangeEvent.OnEventTriggered += ChangeState;
    }

    private void OnDisable()
    {
        PlayerStateChangeEvent.OnEventTriggered -= ChangeState;
    }

    private void ChangeState(int arg)
    {
        _animationController.SetInteger("State", arg);
    }
}
