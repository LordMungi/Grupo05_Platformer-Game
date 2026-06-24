using UnityEngine;

[CreateAssetMenu(fileName = "ShotSpawn", menuName = "Shots/Spawn")]
public class ShotSpawn : ScriptableObject
{
    [field: SerializeField] public Vector2 direction { get; private set; }
    [field: SerializeField] public float speed { get; private set; }
    [field: SerializeField] public float rate { get; private set; }
}
