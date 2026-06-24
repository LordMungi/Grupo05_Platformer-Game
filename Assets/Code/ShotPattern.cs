using UnityEngine;

[CreateAssetMenu(fileName = "ShotPattern", menuName = "Scriptable Objects/ShotPattern")]
public class ShotPattern : ScriptableObject
{
    [field: SerializeField] public ShotSpawn[] shotSpawns { get; private set; }
}
