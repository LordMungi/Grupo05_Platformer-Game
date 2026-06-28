using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Shooter[] Shooters;

    private bool _isShooting = false;

    void Update()
    {
        
    }

    public void StartShooting()
    {
        if (!_isShooting)
        {
            foreach (Shooter s in Shooters)
            {
                s.StartShooting();
            }
        }
        _isShooting = true;
    }

    public void StopShooting()
    {
        foreach (Shooter s in Shooters)
        {
            s.StopShooting();
        }
        _isShooting = false;
    }

    public void ToggleShooting()
    {
        if (_isShooting)
            StopShooting();
        else
            StartShooting();
    }
}
