using UnityEngine;

public class Car : MonoBehaviour {
  [SerializeField]
  private float speedLimit = 0.006f;

  [SerializeField]
  private float brakingSpeed = 0.0004f;

  [SerializeField]
  private float viewDistance = 1f;

  [SerializeField]
  private Lane lane;

  private float currentSpeed;

  private float laneProgress;

  private Vector3 laneBeginPos;

  private Quaternion laneBeginRot;

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

    laneMidpoint = Bezier.Midpoint(transform, lane.End);
    laneProgress = currentSpeed;
  }

  private void AdvanceLane() {
    lane = lane.Next;
    InitLane();
  }

  private void Move() {
    if (lane == null)
      return;

    transform.position = Bezier.Lerp(laneBeginPos, laneMidpoint, lane.End.position, laneProgress);
    transform.rotation = Quaternion.Lerp(laneBeginRot, lane.End.rotation, laneProgress);

    laneProgress += currentSpeed * Time.timeScale;

    if (laneProgress >= 1f) {
      transform.position = lane.End.position;
      transform.rotation = lane.End.rotation;
      AdvanceLane();
    }
  }

  private void Brake() {
    currentSpeed = Mathf.Max(currentSpeed - brakingSpeed * Time.timeScale, 0);
  }

  private void Accelerate() {
    currentSpeed = Mathf.Min(currentSpeed + brakingSpeed * Time.timeScale, speedLimit);
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