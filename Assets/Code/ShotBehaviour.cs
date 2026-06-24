using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    ShotSpawn _settings;

    public void Init(ShotSpawn shotSpawn)
    {
        _settings = shotSpawn;
    }

    void Update()
    {
        transform.Translate(_settings.direction * _settings.speed * Time.deltaTime);
    }
}
