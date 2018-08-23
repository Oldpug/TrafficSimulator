using UnityEngine;

public class Car : MonoBehaviour
{
    private const float EPSILON = 0.01f;

    [SerializeField]
    private float maxSpeed = 2f;

    [SerializeField]
    private float brakingSpeed = 2f;

    [SerializeField]
    private float viewDistance = 1.5f;

    [SerializeField]
    private Lane lane;

    private Rigidbody body;

    private Driver driver;

    private float speed;

    private bool isTurning;

    private float laneLength;

    private Vector3 laneBeginFwd;

    private Vector3 laneBeginPos;

    private Vector3 lastFramePos;

    private void Start()
    {
        lastFramePos = transform.position;
    }

    public float Velocity { get; private set; }

    public bool IsFacingObstacle
    {
        get
        {
            RaycastHit hit;
            bool isFacingObstacle = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, ~0, QueryTriggerInteraction.Collide);

            if (isFacingObstacle)
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            else
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

            return isFacingObstacle;
        }
    }

    private void RecordMovement()
    {
        float x = Vector3.Distance(lastFramePos, transform.position);
        Velocity =  x > EPSILON ? x : 0;
        lastFramePos = transform.position;
    }

    private void InitLane()
    {
        if (lane == null)
            return;

        if (lane.End == null)
        {
            lane = null;
            return;
        }

        laneBeginPos = transform.position;
        laneBeginFwd = transform.forward;

        laneLength = Vector3.Distance(transform.position, lane.End.position);

        isTurning = transform.forward != lane.End.forward;
    }

    private void AdvanceLane()
    {
        transform.position = lane.End.position;
        transform.rotation = lane.End.rotation;

        lane = lane.Next ?? driver.GetDirection();
        InitLane();
    }

    private void Move()
    {
        if (lane == null)
            return;

        var currentDistance = Vector3.Distance(laneBeginPos, transform.position);

        if (isTurning)
        {
            var dir = Vector3.Lerp(laneBeginFwd, lane.End.forward, currentDistance / laneLength);
            body.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
        }

        body.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

        if (Mathf.Abs(laneLength - currentDistance) < 0.1f)
            AdvanceLane();
    }

    private void Brake()
    {
        speed = Mathf.Max(speed - brakingSpeed * Time.fixedDeltaTime, 0);
    }

    private void Accelerate()
    {
        speed = Mathf.Min(speed + brakingSpeed * Time.fixedDeltaTime, maxSpeed);
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        driver = GetComponent<Driver>();

        speed = maxSpeed;
        InitLane();
    }

    private void FixedUpdate()
    {
        RecordMovement();

        if (IsFacingObstacle)
            Brake();
        else
            Accelerate();

        Move();
    }
}