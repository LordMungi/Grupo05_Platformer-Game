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
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.2f)
        {
            foreach (ShotSpawn s in shotPattern.shotSpawns)
            {
                ShotBehaviour newShot;
                newShot = Instantiate(shotType, transform);
                newShot.Init(s);
                _shotsFired.Add(newShot);
            }
            timer = 0f;
        }
    }
}
