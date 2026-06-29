using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "TransformEventChannel", menuName = "Events/TransformEventChannel")]
public class TransformEventChannel : ScriptableObject
{
    public UnityAction<Transform> OnEventTriggered;

    public void RaiseEvent(Transform arg0)
    {
        OnEventTriggered?.Invoke(arg0);
    }
}
