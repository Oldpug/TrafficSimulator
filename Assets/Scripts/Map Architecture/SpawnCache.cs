using System.Collections.Generic;
using UnityEngine;

public class SpawnCache : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cars;

    private Queue<GameObject> cache;

    private void Awake()
    {
        cache = new Queue<GameObject>();
    }

    public void Clear()
    {
        cache.Clear();
    }

    public void SpawnCar(Spawner spawner)
    {
        var obj = cache.Count == 0 ? Instantiate(cars[Random.Range(0, cars.Length)]) : cache.Dequeue();

        obj.transform.position = spawner.transform.position;
        obj.transform.rotation = spawner.transform.rotation;

        var car = obj.GetComponent<Car>();
        car.Lane = spawner;
        car.Cache = this;

        car.Init();
        obj.SetActive(true);
    }

    public void DespawnCar(Car car)
    {
        var obj = car.gameObject;

        obj.SetActive(false);
        cache.Enqueue(obj);
    }
}