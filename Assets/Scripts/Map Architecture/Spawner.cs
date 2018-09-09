using UnityEngine;

public class Spawner : Lane {
  [SerializeField]
  private Transform end;

  [SerializeField]
  private Lane next;

  [SerializeField]
  private float spawnInterval;

  [SerializeField]
  private GameObject[] cars;

  private float timer;

  public override Transform End {
    get {
      return end;
    }
  }

  public override Lane Next {
    get {
      return next;
    }

    set {
      next = value;
    }
  }

  private void Awake() {
    if (cars == null)
      cars = new GameObject[0];

    if (end == null)
      end = transform;

    if (spawnInterval == 0)
      spawnInterval = 1f;
  }

  private void FixedUpdate() {
    timer += Time.fixedDeltaTime;

    if (timer >= spawnInterval) {
      var car = Instantiate(cars[Random.Range(0, cars.Length)]);

      car.transform.position = transform.position;
      car.transform.rotation = transform.rotation;

      car.GetComponent<Car>().lane = this;

      timer = 0;
    }
  }
}