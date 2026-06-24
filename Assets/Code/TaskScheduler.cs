using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskScheduler : MonoBehaviour
{
    public class ScheduledCall
    {
        public Action callback;
        public float remainingTime;

        public ScheduledCall(Action callback, float remainingTime) { this.callback = callback; this.remainingTime = remainingTime; }
    }

    private TaskScheduler() { }

    public static TaskScheduler instance { get; private set; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    List<ScheduledCall> _scheduledCalls;

    void Start()
    {
        _scheduledCalls = new List<ScheduledCall>();
    }

    void Update()
    {
        for (int i = _scheduledCalls.Count - 1; i >= 0; i--)
        {
            ScheduledCall call = _scheduledCalls[i];
            call.remainingTime -= Time.deltaTime;

            if (call.remainingTime <= 0f)
            {
                call.callback.Invoke();
                _scheduledCalls.RemoveAt(i);
            }
        }
    }

    public void Schedule(Action callback, float remainingTime)
    {
        _scheduledCalls.Add(new ScheduledCall(callback, remainingTime));
    }
}
