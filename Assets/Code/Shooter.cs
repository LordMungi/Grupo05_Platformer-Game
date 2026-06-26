using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShotPattern ShotPattern;
    [SerializeField] private ShotBehaviour ShotType;

    private List<ShotBehaviour> _shotsFired;

    private bool _isShooting = false;

    private void Start()
    {
        _shotsFired = new List<ShotBehaviour>();
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());
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
        newShot = Instantiate(ShotType, transform);
        newShot.Init(s);
        _shotsFired.Add(newShot);

        if (_isShooting)
            ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(() => SpawnShot(s), s.Rate);
    }
}
