using UnityEngine;

public class Car : MonoBehaviour {
  [SerializeField]
  private float speedLimit;

  [SerializeField]
  private float brakingSpeed;

  [SerializeField]
  private float viewDistance;

  [SerializeField]
  private Lane lane;

  [SerializeField]
  private float currentSpeed;

  private float laneProgress;

  private Vector3 laneBeginPos;

  private Quaternion laneBeginRot;

  private bool isTurning;

  private Vector3 laneMidpoint;

  public float SpeedLimit {
    get {
      return speedLimit;
    }
  }

  public float CurrentSpeed {
    get {
      return currentSpeed;
    }
  }

  public float BrakingSpeed {
    get {
      return brakingSpeed;
    }
  }

  public Lane Lane {
    get {
      return lane;
    }
  }

  public bool IsFacingObstacle {
    get {
      RaycastHit hit;
      bool isFacingObstacle = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, ~0, QueryTriggerInteraction.Collide);

      if (isFacingObstacle)
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      else
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

      return isFacingObstacle;
    }
  }

  private void InitLane() {
    if (lane == null)
      return;

    if (lane.End == null) {
      lane = null;
      return;
    }

    laneBeginPos = transform.position;
    laneBeginRot = transform.rotation;

    if (transform.forward == lane.End.forward)
      isTurning = false;
    else {
      isTurning = true;
      laneMidpoint = Bezier.Midpoint(transform, lane.End);
    }

    laneProgress = currentSpeed * Time.deltaTime;
  }

  private void AdvanceLane() {
    transform.position = lane.End.position;
    transform.rotation = lane.End.rotation;

    lane = lane.Next;
    InitLane();
  }

  private void Move() {
    if (lane == null)
      return;

    if (isTurning) {
      transform.position = Bezier.Lerp(laneBeginPos, laneMidpoint, lane.End.position, laneProgress);
      transform.rotation = Quaternion.Lerp(laneBeginRot, lane.End.rotation, laneProgress);
    } else
      transform.position = Vector3.Lerp(laneBeginPos, lane.End.position, laneProgress);

    laneProgress += currentSpeed * Time.deltaTime;

    if (laneProgress >= 1f)
      AdvanceLane();
  }

  private void Brake() {
    currentSpeed = Mathf.Max(currentSpeed - brakingSpeed * Time.deltaTime, 0);
  }

  private void Accelerate() {
    currentSpeed = Mathf.Min(currentSpeed + brakingSpeed * Time.deltaTime, speedLimit);
  }

  private void Awake() {
    InitLane();
  }

  private void FixedUpdate() {
    if (IsFacingObstacle)
      Brake();
    else
      Accelerate();

    Move();
  }
}