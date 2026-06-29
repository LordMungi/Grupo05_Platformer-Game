using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    private ShotSpawn _settings;

    public void Init(ShotSpawn shotSpawn, GameObject parent)
    {
        _settings = shotSpawn;
        gameObject.layer = parent.layer;
        Destroy(gameObject, _settings.Lifetime);
    }

    void Update()
    {
        transform.Translate(_settings.Direction * _settings.Speed * Time.deltaTime);
    }
}
