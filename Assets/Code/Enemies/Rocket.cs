using UnityEngine;

public class Rocket : Enemy
{
    [SerializeField] private float LifeTime = 3;
    [SerializeField] private float Speed = 5;

    private bool _isActive = false;

    void Update()
    {
        if (_isActive)
            transform.Translate(Vector2.up * Speed * Time.deltaTime);
    }
    public void Stop()
    {
        _isActive = false;
    }

    
    private void Die()
    {
        gameObject.SetActive(false);
    }

    public override void Activate()
    {
        _isActive = true;
        ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(Die, LifeTime);
    }

    public override void Deactivate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }
}
