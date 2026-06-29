using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator PlayerAnimationController;


    [Header("Listener Events")]
    [SerializeField] private IntEventChannel PlayerStateChangeEvent;
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
        PlayerAnimationController.SetInteger("State", arg);
    }
}
