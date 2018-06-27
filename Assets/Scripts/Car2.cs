using UnityEngine;

public class Car2 : MonoBehaviour
{
    [SerializeField]
    float speed = 0.01f;
    [SerializeField]
    private int steeringSpeed = 3;

    private int desiredRotation = 90;
    private int currentRotation = 0;
    private bool isTurning = false;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
            if(isTurning)
        {
            Turn();
        }
            MoveForward();
    }

    private void MoveForward()
    {
        body.AddForce(transform.forward * speed);
    }

    private void Turn()
    {
        Rotate(steeringSpeed);

        if (currentRotation == desiredRotation)
        {
            isTurning = false;
            currentRotation = 0;
        }
    }

    private void Rotate(int angle)
    {
        currentRotation += angle;
        transform.Rotate(new Vector3(0, angle, 0));
    }
    
    void OnTriggerEnter(Collider other)
    {
        isTurning = true;
        body.velocity = Vector3.zero;
        Debug.Log("CURBAAAAA");
    }
}
