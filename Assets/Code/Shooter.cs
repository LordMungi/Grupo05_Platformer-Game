using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShotPattern ShotPattern;
    [SerializeField] private ShotBehaviour ShotType;

    private List<ShotBehaviour> _shotsFired;

    private float timer = 0f;

    private void Start()
    {
        _shotsFired = new List<ShotBehaviour>();
        ServiceProvider.Instance.AddService<TaskScheduler>(new GameObject("TaskScheduler").AddComponent<TaskScheduler>());

        foreach (ShotSpawn s in ShotPattern.shotSpawns)
        {
            ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(() => SpawnShot(s), s.Rate);
        }
    }

    private void SpawnShot(ShotSpawn s)
    {
        ShotBehaviour newShot;
        newShot = Instantiate(ShotType, transform);
        newShot.Init(s);
        _shotsFired.Add(newShot);
        ServiceProvider.Instance.GetService<TaskScheduler>().Schedule(() => SpawnShot(s), s.Rate);
    }
}
