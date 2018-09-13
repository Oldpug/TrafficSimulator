using UnityEngine;

public class Spawner : Lane
{
    private static SpawnCache cache;

    [SerializeField]
    private Transform end;

    [SerializeField]
    private Lane next;

    [SerializeField]
    private float spawnInterval;

    private float timer;

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

    private void Awake()
    {
        if (cache == null)
            cache = FindObjectOfType<SpawnCache>();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= spawnInterval)
        {
            cache.SpawnCar(this);
            timer = 0;
        }
    }
}