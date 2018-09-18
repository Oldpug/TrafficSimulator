using System.Collections.Generic;
using UnityEngine;

public class SpawnCache : MonoBehaviour
{
    private static SpawnCache instance;

    private static ConcurrentQueue<GameObject> carCache;

    private static int carCount;

    [SerializeField]
    private GameObject[] carPrefabs;
    
    [SerializeField]
    private int maxCarCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            carCache = new ConcurrentQueue<GameObject>();
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
        if (carCount >= instance.maxCarCount)
            return;

        ++carCount;

        var cars = instance.carPrefabs;
        var obj = carCache.IsEmpty ? Instantiate(cars[Random.Range(0, cars.Length)]) : carCache.Dequeue();

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