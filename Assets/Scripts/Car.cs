using UnityEngine;

public class Car : MonoBehaviour 
{
    float speed = 0.005f;

    private void Start()
    {
        speed = Random.Range(0.004f, 1);
    }
    // Update is called once per frame
    void Update ()
    {
        Vector3 pos = transform.position;
        pos.x += speed;
        transform.position = pos;
	}
}
