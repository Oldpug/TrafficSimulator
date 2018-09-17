﻿using UnityEngine;

public class Spawner : Lane
{
    [SerializeField]
    private Transform end;

    [SerializeField]
    private Lane next;

    [SerializeField]
    public float SpawnInterval;

    private float timer;

    private int carsNearby;

    public override Transform End
    {
        get
        {
            return end;
        }
    }

    public override Lane Next
    {
        get
        {
            return next;
        }

        set
        {
            next = value;
        }
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= SpawnInterval && carsNearby == 0)
        {
            SpawnCache.SpawnCar(this);
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ++carsNearby;
    }

    private void OnTriggerExit(Collider other)
    {
        --carsNearby;
    }
}