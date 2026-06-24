using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class ShotSpawn : ScriptableObject
{
    [field: SerializeField] public Vector2 direction { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float rate { get; private set; }
}
