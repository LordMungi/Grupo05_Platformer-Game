using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    ShotSpawn _settings;

    public void Init(ShotSpawn shotSpawn)
    {
        _settings = shotSpawn;
        Destroy(gameObject, _settings.Lifetime);
    }

    void Update()
    {
        transform.Translate(_settings.Direction * _settings.Speed * Time.deltaTime);
    }
}
