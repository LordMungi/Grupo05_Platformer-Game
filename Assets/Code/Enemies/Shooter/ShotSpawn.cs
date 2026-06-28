using UnityEngine;

[CreateAssetMenu(fileName = "ShotSpawn", menuName = "Shots/Spawn")]
public class ShotSpawn : ScriptableObject
{
    [field: SerializeField] public Vector2 Direction { get; private set; }
    [field: SerializeField] public float Speed { get; private set; }
    [field: SerializeField] public float Rate { get; private set; }
    [field: SerializeField] public float Lifetime { get; private set; } = 2f;
}
