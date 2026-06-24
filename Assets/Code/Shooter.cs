using System;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private ShotPattern shotPattern;
    [SerializeField] private ShotBehaviour shotType;

    private List<ShotBehaviour> _shotsFired;

    private float timer = 0f;

    private void Start()
    {
        _shotsFired = new List<ShotBehaviour>();

        foreach (ShotSpawn s in shotPattern.shotSpawns)
        {
            TaskScheduler.instance.Schedule(() => SpawnShot(s), s.rate);
        }
    }

    private void SpawnShot(ShotSpawn s)
    {
        ShotBehaviour newShot;
        newShot = Instantiate(shotType, transform);
        newShot.Init(s);
        _shotsFired.Add(newShot);
        TaskScheduler.instance.Schedule(() => SpawnShot(s), s.rate);
    }
}
