using UnityEngine;

public class Car : MonoBehaviour {
  [SerializeField]
  private float speed = 0.75f;

  [SerializeField]
  private float tDelta = 0.0075f;

  [SerializeField]
  private float viewDistance = 1f;

  [SerializeField]
  private float brakingSpeed = 0.1f;

  private float t = 1f;

  private Lane currentLane;

  private Vector3 turnBeginPos;

  private Quaternion turnBeginRot;

  private Vector3 turnMidpoint;

  private Rigidbody rigidBody;

  private bool IsTurning {
    get {
      return t < 1f;
    }
  }

  private bool IsFacingObstacle {
    get {
      RaycastHit hit;
      bool isFacingObstacle = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, ~0, QueryTriggerInteraction.Ignore);

      if (isFacingObstacle)
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
      else
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);

      return isFacingObstacle;
    }
  }

  private void Awake() {
    rigidBody = GetComponent<Rigidbody>();
  }

  private void MoveForward() {
    rigidBody.AddForce(transform.forward * speed);
  }

  private void Brake() {
    rigidBody.drag += brakingSpeed;
  }

  private void Turn() {
    rigidBody.MovePosition(Bezier.Lerp(turnBeginPos, turnMidpoint, currentLane.End.position, t));
    rigidBody.MoveRotation(Quaternion.Lerp(turnBeginRot, currentLane.End.rotation, t));

    t += tDelta;
  }

  private void StopTurning() {
    rigidBody.velocity = rigidBody.transform.forward;
    MoveForward();
  }

  private void OnTriggerEnter(Collider collider) {
    Lane newLane = collider.GetComponent<Lane>();

    if (newLane == null)
      return;

    turnBeginPos = transform.position;
    turnBeginRot = transform.rotation;

    currentLane = newLane;
    turnMidpoint = Bezier.Midpoint(transform, newLane.End);

    t = tDelta;
  }

  private void OnTriggerStay(Collider collider) {
    if (collider.tag == Intersection.StopperTag)
      Brake();
  }

  private void FixedUpdate() {
    if (IsFacingObstacle)
      Brake();
    else if (IsTurning) {
      Turn();

      if (!IsTurning)
        StopTurning();
    } else
      MoveForward();
  }
}