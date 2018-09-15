using System.Collections.Generic;
using UnityEngine;

public class SpawnCache : MonoBehaviour
{
    private static SpawnCache instance;

    private static Queue<GameObject> carCache;

    private static int carCount;

    [SerializeField]
    private GameObject[] carPrefabs;
    
    [SerializeField]
    private int maxCarCount;

    public static int MaxCarCount
    {
        get
        {
            return instance.maxCarCount;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            carCache = new Queue<GameObject>();
        }
        else
            Destroy(gameObject);
    }

    public static void Clear()
    {
        carCache.Clear();
    }

    public static void SpawnCar(Spawner spawner)
    {
        if (carCount >= MaxCarCount)
            return;

        ++carCount;

        var cars = instance.carPrefabs;
        var obj = carCache.Count == 0 ? Instantiate(cars[Random.Range(0, cars.Length)]) : carCache.Dequeue();

        obj.transform.position = spawner.transform.position;
        obj.transform.rotation = spawner.transform.rotation;

        var car = obj.GetComponent<Car>();
        car.Lane = spawner;

        car.Init();
        obj.SetActive(true);
    }

    public static void DespawnCar(Car car)
    {
        var obj = car.gameObject;

        obj.SetActive(false);
        carCache.Enqueue(obj);

        --carCount; 
    }
}