using UnityEngine;

[CreateAssetMenu(fileName = "ShotPattern", menuName = "Shots/Pattern")]
public class ShotPattern : ScriptableObject
{
    [field: SerializeField] public ShotSpawn[] shotSpawns { get; private set; }
}
