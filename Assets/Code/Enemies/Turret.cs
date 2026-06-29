using UnityEngine;

public class Turret : Enemy
{
    [SerializeField] private Shooter[] Shooters;

    private bool _isShooting = false;

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

    public override void Activate()
    {
        StartShooting();
    }

    public override void Deactivate()
    {
        StopShooting();
    }
}
