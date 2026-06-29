using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatEventChannel", menuName = "Events/FloatEventChannel")]
public class FloatEventChannel : ScriptableObject
{
    public UnityAction<float> OnEventTriggered;

    public void RaiseEvent(float arg0)
    {
        OnEventTriggered?.Invoke(arg0);
    }
}
