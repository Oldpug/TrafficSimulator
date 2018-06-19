using TrafficSimulator;
using UnityEngine;

public class TurningCar : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.75f;

    [SerializeField]
    private float tDelta = 0.0075f;

    private float t = 1f;

    private Lane lane;

    private Rigidbody rigidBody;

    private void MoveForward()
    {
        rigidBody.AddForce(transform.forward * speed);
    }

    private void Turn()
    {
        rigidBody.MovePosition(Bezier.Lerp(lane.Start.position, lane.Midpoint.position, lane.End.position, t));
        rigidBody.MoveRotation(Quaternion.Lerp(lane.Start.rotation, lane.End.rotation, t));

        t += tDelta;
    }

    private bool FinishedTurning()
    {
        return t >= 1f;
    }

    private void StopTurning()
    {
        rigidBody.velocity = Vector3.zero;
        MoveForward();
    }

    public void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (!FinishedTurning())
        {
            Turn();

            if (FinishedTurning())
                StopTurning();
        }
        else
            MoveForward();
    }

    public void OnTriggerEnter(Collider other)
    {
        var newLane = other.GetComponent<Lane>();

        if (newLane == null)
            return;

        lane = newLane;
        t = tDelta;
    }
}