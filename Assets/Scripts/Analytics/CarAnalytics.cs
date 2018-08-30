using UnityEngine;

public class CarAnalytics : MonoBehaviour {

    [SerializeField]
    private float engine;

    private Car car;

    void Start()
    {
        car = GetComponent<Car>();
    }

    public float Velocity
    {
        get { return car.Velocity; }
    }

    public float CO2
    {
        get { return engine; }
    }


	
	void Update () {
		
	}
}
