using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    float speed = 0.01f;
    [SerializeField]
    float detectionRayLength = 1;
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
        if (isTurning)
        {
            Turn();
        }
        else if (IsFacingObstacle())
        {
            body.velocity = Vector3.zero;
            isTurning = true;
        }
        else
        {
            MoveForward();
        }
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

    private bool IsFacingObstacle()
    {
        // Detect collisions with anything
        int layerMask = ~0;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, detectionRayLength, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        return false;
    }
}
