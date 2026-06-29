using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShotPattern ShotPattern;
    [SerializeField] private ShotBehaviour ShotType;
    [SerializeField] private GameObject BulletParent;

    private List<ShotBehaviour> _shotsFired;

    private bool _isShooting = false;

    private void Start()
    {
        _shotsFired = new List<ShotBehaviour>();
    }

    public void StartShooting()
    {
        if (!_isShooting)
        {
            foreach (ShotSpawn s in ShotPattern.shotSpawns)
            {
                ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(() => SpawnShot(s), s.Rate);
            }
        }
        _isShooting = true;
    }

    public void StopShooting()
    {
        _isShooting = false;
    }

    public void ToggleShooting()
    {
        if (_isShooting)
            StopShooting();
        else
            StartShooting();
    }

    private void SpawnShot(ShotSpawn s)
    {
        ShotBehaviour newShot;
        newShot = Instantiate(ShotType, transform.position, transform.rotation, BulletParent.transform);
        newShot.Init(s, BulletParent);
        _shotsFired.Add(newShot);

        if (_isShooting)
            ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(() => SpawnShot(s), s.Rate);
    }
}
