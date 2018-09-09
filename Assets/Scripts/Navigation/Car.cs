using UnityEngine;

public class Car : MonoBehaviour {
  [SerializeField]
  private readonly float maxSpeed = 2f;

  [SerializeField]
  private readonly float brakingSpeed = 2f;

  [SerializeField]
  private readonly float viewDistance = 1.5f;

  [SerializeField]
  private readonly float laneCorrectionDistance = 0.01f;

  [SerializeField]
  private IntersectionLane[] path;

  [SerializeField]
  public Lane lane;

  private Rigidbody body;

  private int currentPathIdx;

  private float speed;

  private bool isTurning;

  private float laneLength;

  private Vector3 laneBeginFwd;

  private Vector3 laneBeginPos;

  private Vector3 lastFramePos;

  public float Velocity { get; private set; }

  public bool IsFacingObstacle {
    get {
      RaycastHit hit;
      var isFacingObstacle = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, ~0, QueryTriggerInteraction.Collide);

      if (isFacingObstacle)
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      else
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

      return isFacingObstacle;
    }
  }

  private void RecordMovement() {
    var x = Vector3.Distance(lastFramePos, transform.position);
    Velocity = x > laneCorrectionDistance ? x : 0;
    lastFramePos = transform.position;
  }

  private void InitLane() {
    if (lane == null)
      return;

    if (lane.End == null) {
      lane = null;
      return;
    }

    laneBeginPos = transform.position;
    laneBeginFwd = transform.forward;

    laneLength = Vector3.Distance(transform.position, lane.End.position);

    isTurning = transform.forward != lane.End.forward;
  }

  private void AdvanceLane() {
    transform.position = lane.End.position;
    transform.rotation = lane.End.rotation;

    var next = lane.Next;

    if (next is IntersectionLane) {
      var intersection = next as IntersectionLane;

      if (++currentPathIdx >= path.Length)
        lane = null;
      else
        lane = intersection.GetIntersectionExit(path[currentPathIdx], transform);
    } else
      lane = next;

    InitLane();
  }

  private void Move() {
    if (lane == null)
      return;

    var currentDistance = Vector3.Distance(laneBeginPos, transform.position);

    if (isTurning) {
      var dir = Vector3.Lerp(laneBeginFwd, lane.End.forward, currentDistance / laneLength);
      body.MoveRotation(Quaternion.LookRotation(dir, Vector3.up));
    }

    body.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);

    if (Mathf.Abs(laneLength - currentDistance) < 0.1f)
      AdvanceLane();
  }

  private void Brake() {
    speed = Mathf.Max(speed - brakingSpeed * Time.fixedDeltaTime, 0);
  }

  private void Accelerate() {
    speed = Mathf.Min(speed + brakingSpeed * Time.fixedDeltaTime, maxSpeed);
  }

  private void Awake() {
    body = GetComponent<Rigidbody>();

    if (path == null)
      path = new IntersectionLane[0];

    lastFramePos = transform.position;
    speed = maxSpeed;
    InitLane();
  }

  private void FixedUpdate() {
    RecordMovement();

    if (IsFacingObstacle)
      Brake();
    else
      Accelerate();

    Move();
  }
}