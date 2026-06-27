using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    private ShotSpawn _settings;
    private Vector3 _movementDirection;

    public void Init(ShotSpawn shotSpawn)
    {
        _settings = shotSpawn;
        Destroy(gameObject, _settings.Lifetime);

        _movementDirection = transform.rotation * _settings.Direction;
    }

    void Update()
    {
        transform.Translate(_movementDirection * _settings.Speed * Time.deltaTime);
    }
}
